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
            int boxLength = (int)Math.Sqrt(boardSize);

            for (int row = 0; row < boardSize; row++)
            {
                changeFlag |= ApplyHiddenPairsInRow(board, row);
            }

            for (int col = 0; col < boardSize; col++)
            {
                changeFlag |= ApplyHiddenPairsInColumn(board, col);
            }

            for (int boxRow = 0; boxRow < boardSize; boxRow += boxLength)
            {
                for (int boxCol = 0; boxCol < boardSize; boxCol += boxLength)
                {
                    changeFlag |= ApplyHiddenPairsInBlock(board, boxRow, boxCol);
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
        public static bool ApplyHiddenPairsInRow(SudokuBoard board, int row)
        {
            bool changeFlag = false, foundFlag = false;
            int boardSize = board.BoardSize, valueIndex, firstValueIndex, secondValueIndex;
            List<BoardCell> emptyCells = board.GetEmptyCellsInRow(row);

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell>[] countsCells = new List<BoardCell>[boardSize];
            for (valueIndex = 0; valueIndex < countsCells.Length; valueIndex++)
            {
                countsCells[valueIndex] = new List<BoardCell>();
            }

            // set with all the values that have not been placed yet in the row
            HashSet<int> unusedValues = new HashSet<int>();
            unusedValues.UnionWith(Enumerable.Range(1, boardSize));

            // removing the placed values in the row from the row possibilities set
            for (int col = 0; col < boardSize; col++)
            {
                var cell = board.GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    unusedValues.Remove(cell.GetValue());
                }
            }

            // adding for each unplaced value the cells that includes the value in their possibilities
            foreach (int value in unusedValues)
            {
                foreach (BoardCell cell in emptyCells)
                {
                    if (cell.GetPossibilities().Contains(value))
                    {
                        countsCells[value - 1].Add(cell);
                    }
                }

            }

            // searching for 2 values that has to be in the same 2 cells
            for (firstValueIndex = 0; firstValueIndex < boardSize; firstValueIndex++)
            {
                foundFlag = false;
                if (countsCells[firstValueIndex].Count > 0) // only unplaced values
                {
                    int pairValue = -1;
                    for (secondValueIndex = firstValueIndex + 1; secondValueIndex < boardSize; secondValueIndex++)
                    {
                        if (countsCells[firstValueIndex].SequenceEqual(countsCells[secondValueIndex])
                            && countsCells[firstValueIndex].Count == 2) // 2 values with the same 2 cells
                        {
                            if (foundFlag == false) // didnt fount yet
                            {
                                foundFlag = true; // found    
                                pairValue = secondValueIndex + 1;
                            }

                            else
                                throw new UnsolvableBoardException(); // more than 2 values has to be in exactly 2 cells
                        }
                    }

                    // if a hidden pair has been found, remove all the other possibilites from these cells
                    if (foundFlag == true)
                    {
                        foreach (int value in countsCells[firstValueIndex].First().GetPossibilities())
                        {
                            if (value != firstValueIndex + 1 && value != pairValue)
                            {
                                board.RemoveCellPossibility(countsCells[firstValueIndex].First().Row,
                                    countsCells[firstValueIndex].First().Col, value);
                                board.RemoveCellPossibility(countsCells[firstValueIndex].Last().Row,
                                    countsCells[firstValueIndex].Last().Col, value);
                                changeFlag = true;

                            }
                        }
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
        public static bool ApplyHiddenPairsInColumn(SudokuBoard board, int col)
        {
            bool changeFlag = false, foundFlag = false;
            int boardSize = board.BoardSize, valueIndex, firstValueIndex, secondValueIndex;
            List<BoardCell> emptyCells = board.GetEmptyCellsInColumn(col);

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell>[] countsCells = new List<BoardCell>[boardSize];
            for (valueIndex = 0; valueIndex < countsCells.Length; valueIndex++)
            {
                countsCells[valueIndex] = new List<BoardCell>();
            }

            // set with all the values that have not been placed yet in the column
            HashSet<int> unusedValues = new HashSet<int>();
            unusedValues.UnionWith(Enumerable.Range(1, boardSize));

            // removing the placed values in the column from the column possibilities set
            for (int row = 0; row < boardSize; row++)
            {
                var cell = board.GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    unusedValues.Remove(cell.GetValue());
                }
            }

            // adding for each unplaced value the cells that includes the value in their possibilities
            foreach (int value in unusedValues)
            {
                foreach (BoardCell cell in emptyCells)
                {
                    if (cell.GetPossibilities().Contains(value))
                    {
                        countsCells[value - 1].Add(cell);
                    }
                }
            }

            // searching for 2 values that has to be in the same 2 cells
            for (firstValueIndex = 0; firstValueIndex < boardSize; firstValueIndex++)
            {
                foundFlag = false;
                if (countsCells[firstValueIndex].Count > 0) // only unplaced values
                {
                    int pairValue = -1;
                    for (secondValueIndex = firstValueIndex + 1; secondValueIndex < boardSize; secondValueIndex++)
                    {
                        if (countsCells[firstValueIndex].SequenceEqual(countsCells[secondValueIndex])
                            && countsCells[firstValueIndex].Count == 2) // 2 cells have the same 2 possibilities
                        {
                            if (foundFlag == false) // hadn't found yet
                            {
                                foundFlag = true; // found    
                                pairValue = secondValueIndex + 1;
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
                        foreach (int value in countsCells[firstValueIndex].First().GetPossibilities())
                        {
                            if (value != firstValueIndex + 1 && value != pairValue)
                            {
                                board.RemoveCellPossibility(countsCells[firstValueIndex].First().Row,
                                    countsCells[firstValueIndex].First().Col, value);
                                board.RemoveCellPossibility(countsCells[firstValueIndex].Last().Row,
                                    countsCells[firstValueIndex].Last().Col, value);
                                changeFlag = true;
                            }
                        }
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
        public static bool ApplyHiddenPairsInBlock(SudokuBoard board, int blockStartRow, int blockStartCol)
        {
            bool changeFlag = false, foundFlag = false;
            int boardSize = board.BoardSize, valueIndex, firstValueIndex, secondValueIndex;
            int boxLength = (int)Math.Sqrt(boardSize);

            // Array of board cells lists. each array cell index(+1) represents a possible final value.
            // each array cells includes a list of the possible board cells with that value.
            List<BoardCell> emptyCells = board.GetEmptyCellsInBlock(blockStartRow, blockStartCol);
            List<BoardCell>[] countsCells = new List<BoardCell>[boardSize];

            // set with all the values that have not been placed yet in the block
            for (valueIndex = 0; valueIndex < countsCells.Length; valueIndex++)
            {
                countsCells[valueIndex] = new List<BoardCell>();
            }

            HashSet<int> unusedValues = new HashSet<int>();
            unusedValues.UnionWith(Enumerable.Range(1, boardSize));

            // removing the placed values in the block from the block possibilities set
            for (int row = blockStartRow; row < blockStartRow + boxLength; row++)
            {
                for (int col = blockStartCol; col < blockStartCol + boxLength; col++)
                {
                    var cell = board.GetCell(row, col);
                    if (!cell.IsEmpty())
                    {
                        unusedValues.Remove(cell.GetValue());
                    }
                }
            }

            // adding for each unplaced value the cells that includes the value in their possibilities
            foreach (int value in unusedValues)
            {
                foreach (BoardCell cell in emptyCells)
                {
                    if (cell.GetPossibilities().Contains(value))
                    {
                        countsCells[value - 1].Add(cell);
                    }
                }
            }

            // searching for 2 values that has to be in the same 2 cells
            for (firstValueIndex = 0; firstValueIndex < boardSize; firstValueIndex++)
            {
                foundFlag = false;
                if (countsCells[firstValueIndex].Count > 0) // only unplaced values
                {
                    int pairValue = -1;
                    for (secondValueIndex = firstValueIndex + 1; secondValueIndex < boardSize; secondValueIndex++)
                    {
                        if (countsCells[firstValueIndex].SequenceEqual(countsCells[secondValueIndex])
                            && countsCells[firstValueIndex].Count == 2) // 2 cells have the same 2 possibilities
                        {
                            if (foundFlag == false) // hadn't found yet
                            {
                                foundFlag = true; // found    
                                pairValue = secondValueIndex + 1;
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
                        foreach (int value in countsCells[firstValueIndex].First().GetPossibilities())
                        {
                            if (value != firstValueIndex + 1 && value != pairValue)
                            {
                                board.RemoveCellPossibility(countsCells[firstValueIndex].First().Row, countsCells[firstValueIndex].First().Col, value);
                                board.RemoveCellPossibility(countsCells[firstValueIndex].Last().Row, countsCells[firstValueIndex].Last().Col, value);
                                changeFlag = true;
                            }
                        }
                    }
                }
            }

            return changeFlag;
        }
        
    }
}
