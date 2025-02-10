using OmegaSudoku.Exceptions;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Heuristics
{
    public class HiddenPairsHeuristic : IHeuristic
    {

        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to the whole board.
        /// A hidden pair are two empty cells in the same row/column/block that both includes the same two value possibilities that could not appear nowhere else.
        /// The function removes all the other values from these 2 cells to reduce possibilities.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>

        public bool ApplyHeuristic(SudokuBoard board)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;

            for (int row = 0; row < boardSize; row++)
            {
                changeFlag |= ApplyHiddenPairsInRow(board, row);
            }

            for (int col = 0; col < boardSize; col++)
            {
                changeFlag |= ApplyHiddenPairsInColumn(board, col);
            }

            for (int blockRow = 0; blockRow < boardSize; blockRow += board.BlockSize)
            {
                for (int blockCol = 0; blockCol < boardSize; blockCol += board.BlockSize)
                {
                    changeFlag |= ApplyHiddenPairsInBlock(board, blockRow, blockCol);
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to a given row on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="row"> The Sudoku board row to apply the hidden pair heuristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        /// <exception cref="UnsolvableBoardException">Thrown when there are more than 2 values that have to be in exactly 2 cells, which means that the board is unsolvable.</exception>
        public static bool ApplyHiddenPairsInRow(SudokuBoard board, int row)
        {
            bool changeFlag = false, foundFlag = false;
            int boardSize = board.BoardSize, valueIndex, firstValueIndex, secondValueIndex, firstValue, secondValue;
            List<BoardCell> emptyCells = board.GetEmptyCellsInRow(row);

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell>[] cellsListsArray = GetInitializedCellsListsArray(boardSize);


            // set with all the values that have not been placed yet in the row
            HashSet<int> unusedValues = board.GetRowUnusedValuesSet(row);

            // adding for each unplaced value the cells that includes the value in their possibilities
            AddPossibleCellsToArray(cellsListsArray, unusedValues, emptyCells);


            // searching for 2 values that has to be in the same 2 cells
            for (firstValueIndex = 0; firstValueIndex < boardSize; firstValueIndex++)
            {
                foundFlag = false;
                if (cellsListsArray[firstValueIndex].Count > 0) // only unplaced values
                {
                    firstValue = firstValueIndex + 1;
                    secondValue = -1;
                    for (secondValueIndex = firstValueIndex + 1; secondValueIndex < boardSize; secondValueIndex++)
                    {
                        if (cellsListsArray[firstValueIndex].SequenceEqual(cellsListsArray[secondValueIndex])
                            && cellsListsArray[firstValueIndex].Count == 2) // 2 values with the same 2 cells
                        {
                            if (foundFlag == false) // didnt fount yet
                            {
                                foundFlag = true; // found    
                                secondValue = secondValueIndex + 1;
                            }

                            else
                                throw new UnsolvableBoardException(); // more than 2 values has to be in exactly 2 cells
                        }
                    }

                    // if a hidden pair has been found, remove all the other possibilites from these cells
                    if (foundFlag == true)
                    {
                        changeFlag = RemoveHiddenPairPossibilities(board, cellsListsArray, firstValueIndex, firstValueIndex + 1, secondValue);
                    }
                }
            }
            return changeFlag;
        }


        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to a given column on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="col"> The Sudoku board column to apply the hidden pair heuristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        /// <exception cref="UnsolvableBoardException">Thrown when there are more than 2 values that have to be in exactly 2 cells, which means that the board is unsolvable.</exception>
        public static bool ApplyHiddenPairsInColumn(SudokuBoard board, int col)
        {
            bool changeFlag = false, foundFlag = false;
            int boardSize = board.BoardSize, valueIndex, firstValueIndex, secondValueIndex, firstValue, secondValue;
            List<BoardCell> emptyCells = board.GetEmptyCellsInColumn(col);

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell>[] cellsListsArray = GetInitializedCellsListsArray(boardSize);

            // set with all the values that have not been placed yet in the column
            HashSet<int> unusedValues = board.GetColumnUnusedValuesSet(col);


            // adding for each unplaced value the cells that includes the value in their possibilities
            AddPossibleCellsToArray(cellsListsArray, unusedValues, emptyCells);

            // searching for 2 values that has to be in the same 2 cells
            for (firstValueIndex = 0; firstValueIndex < boardSize; firstValueIndex++)
            {
                foundFlag = false; // flag reset
                if (cellsListsArray[firstValueIndex].Count > 0) // only unplaced values
                {
                    firstValue = firstValueIndex + 1;
                    secondValue = -1;
                    for (secondValueIndex = firstValueIndex + 1; secondValueIndex < boardSize; secondValueIndex++)
                    {
                        if (cellsListsArray[firstValueIndex].SequenceEqual(cellsListsArray[secondValueIndex])
                            && cellsListsArray[firstValueIndex].Count == 2) // 2 cells have the same 2 possibilities
                        {
                            if (foundFlag == false) // hadn't found yet
                            {
                                foundFlag = true; // found    
                                secondValue = secondValueIndex + 1;
                            }
                            else
                            {
                                throw new UnsolvableBoardException(); // more than 2 values has to be in exactly 2 cells
                            }
                        }
                    }

                    // if a hidden pair has been found, remove all the other possibilites from these cells
                    if (foundFlag == true)
                    {
                        changeFlag = RemoveHiddenPairPossibilities(board, cellsListsArray, firstValueIndex, firstValue, secondValue);
                    }
                }
            }

            return changeFlag;
        }

        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to a given block on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="blockStartRow"> The Sudoku board start row of the block to apply the hidden pair heuristic. </param>
        /// <param name="blockStartCol"> The Sudoku board start column of the block to apply the hidden pair huristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        /// <exception cref="UnsolvableBoardException">Thrown when there are more than 2 values that have to be in exactly 2 cells, which means that the board is unsolvable.</exception>
        public static bool ApplyHiddenPairsInBlock(SudokuBoard board, int blockStartRow, int blockStartCol)
        {
            bool changeFlag = false, foundFlag = false;
            int boardSize = board.BoardSize, valueIndex, firstValueIndex, secondValueIndex, firstValue, secondValue;

            List<BoardCell> emptyCells = board.GetEmptyCellsInBlock(blockStartRow, blockStartCol);

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell>[] cellsListsArray = GetInitializedCellsListsArray(boardSize);

            // set with all the values that have not been placed yet in the block
            HashSet<int> unusedValues = board.GetBlockUnusedValuesSet(blockStartRow, blockStartCol);

            // adding for each unplaced value the cells that includes the value in their possibilities
            AddPossibleCellsToArray(cellsListsArray, unusedValues, emptyCells);

            // searching for 2 values that has to be in the same 2 cells
            for (firstValueIndex = 0; firstValueIndex < boardSize; firstValueIndex++)
            {
                foundFlag = false;
                if (cellsListsArray[firstValueIndex].Count > 0) // only unplaced values
                {
                    firstValue = firstValueIndex + 1;
                    secondValue = -1;
                    for (secondValueIndex = firstValueIndex + 1; secondValueIndex < boardSize; secondValueIndex++)
                    {
                        if (cellsListsArray[firstValueIndex].SequenceEqual(cellsListsArray[secondValueIndex])
                            && cellsListsArray[firstValueIndex].Count == 2) // 2 cells have the same 2 possibilities
                        {
                            if (foundFlag == false) // hadn't found yet
                            {
                                foundFlag = true; // found    
                                secondValue = secondValueIndex + 1;
                            }
                            else
                            {
                                throw new UnsolvableBoardException(); // more than 2 values has to be in exactly 2 cells
                            }
                        }
                    }

                    // if a hidden pair has been found, remove all the other possibilites from these cells
                    if (foundFlag == true)
                    {
                        changeFlag = RemoveHiddenPairPossibilities(board, cellsListsArray, firstValueIndex, firstValue, secondValue);
                    }
                }
            }

            return changeFlag;
        }


        /// <summary>
        /// Removes from a given hidden pair all of their other possibilities
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="cellsListsArray"> Array of cells lists. Each array index (+1) represents a possible value. Each value has a list with all the cells that includes him. </param>
        /// <param name="firstValueIndex"> First pair value index in the array. </param>
        /// <param name="firstValue"> First pair value. </param>
        /// <param name="secondValue"> Second pair value. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool RemoveHiddenPairPossibilities(SudokuBoard board, List<BoardCell>[] cellsListsArray, int firstValueIndex, int firstValue, int secondValue)
        {
            bool changeFlag = false;
            foreach (int value in cellsListsArray[firstValueIndex].First().GetPossibilities())
            {
                if (value != firstValue && value != secondValue)
                {
                    board.RemoveCellPossibility(cellsListsArray[firstValueIndex].First().Row,
                        cellsListsArray[firstValueIndex].First().Col, value);
                    board.RemoveCellPossibility(cellsListsArray[firstValueIndex].Last().Row,
                        cellsListsArray[firstValueIndex].Last().Col, value);

                    changeFlag = true;
                }
            }

            return changeFlag;
        }

        /// <summary>
        /// returns an initialized array of cells lists. Each array index (+1) represents a possible value.
        /// </summary>
        /// <param name="arraySize"> The array size </param>
        /// <returns> returns an array with empty lists of cells.</returns>
        private static List<BoardCell>[] GetInitializedCellsListsArray(int arraySize)
        {
            int valueIndex;

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell>[] cellsListsArray = new List<BoardCell>[arraySize];

            for (valueIndex = 0; valueIndex < arraySize; valueIndex++)
            {
                cellsListsArray[valueIndex] = new List<BoardCell>();
            }
            return cellsListsArray;
        }

        /// <summary>
        /// Adds for each index in a given array all the cells that includes the value (index+1 ) in their final value possibilities.
        /// </summary>
        /// <param name="cellsListsArray"> Array of cells lists. Each array index (+1) represents a possible value. Each value has a list with all the cells that includes him </param>
        /// <param name="unusedValues"> Set of all the unused values in a sudoku board's row/column/block </param>
        /// <param name="emptyCells"> List of all the empty cells in a sudoku board's row/column/block. </param>
        private static void AddPossibleCellsToArray(List<BoardCell>[] cellsListsArray, HashSet<int> unusedValues, List<BoardCell> emptyCells)
        {
            foreach (int value in unusedValues)
            {
                foreach (BoardCell cell in emptyCells)
                {
                    if (cell.GetPossibilities().Contains(value))
                    {
                        cellsListsArray[value - 1].Add(cell);
                    }
                }
            }
        }
    }

}
