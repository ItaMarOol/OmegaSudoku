using OmegaSudoku.Exceptions;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic
{
    public static class SudokuHeuristics
    {

        /// <summary>
        /// Applies the naked pairs sudoku heuristic to the whole board.
        /// A naked pair are two empty cells in the same row/column/block that both has the same two value possibilities.
        /// The function removes these values from the other cells in the pair's row, column, block to reduce possibilities.
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// </summary>
        /// <returns></returns>
        public static void ApplyNakedPairs(SudokuBoard board)
        {
            int boardSize = board.BoardSize;
            int boxLength = (int)Math.Sqrt(boardSize);

            for (int row = 0; row < boardSize; row++)
            {
                ApplyNakedPairsInRow(board, row);
            }

            for (int col = 0; col < boardSize; col++)
            {
                ApplyNakedPairsInColumn(board, col);
            }

            for (int boxRow = 0; boxRow < boardSize; boxRow += boxLength)
            {
                for (int boxCol = 0; boxCol < boardSize; boxCol += boxLength)
                {
                    ApplyNakedPairsInBlock(board, boxRow, boxCol);
                }
            }
        }

        /// <summary>
        /// Applies the naked pairs sudoku huristic to a given row on the board.
        /// </summary>
        /// <param name="row"> The Sudoku board row to apply the naked pair huristic. </param>
        /// <returns></returns>
        private static void ApplyNakedPairsInRow(SudokuBoard board, int row)
        {
            int boardSize = board.BoardSize;
            List<BoardCell> emptyCells = board.GetEmptyCellsInRow(row);

            // looking for two empty cells with the same possibilities pair
            for (int i = 0; i < emptyCells.Count; i++)
            {
                for (int j = i + 1; j < emptyCells.Count; j++)
                {
                    var cell1 = emptyCells[i];
                    var cell2 = emptyCells[j];

                    var possibilities1 = cell1.GetPossibilities();
                    var possibilities2 = cell2.GetPossibilities();

                    if (possibilities1.SetEquals(possibilities2) && possibilities1.Count == 2)
                    {
                        // removing the possibilities pair from the empty cell in the given row
                        for (int col = 0; col < boardSize; col++)
                        {
                            if (col != cell1.Col && col != cell2.Col)
                            {
                                board.RemoveCellPossibility(row, col, possibilities1.First());
                                board.RemoveCellPossibility(row, col, possibilities1.Last());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies the naked pairs sudoku huristic to a given column on the board.
        /// </summary>
        /// <param name="col"> The Sudoku board column to apply the naked pair huristic. </param>
        /// <returns></returns>
        private static void ApplyNakedPairsInColumn(SudokuBoard board, int col)
        {
            int boardSize = board.BoardSize;
            List<BoardCell> emptyCells = board.GetEmptyCellsInColumn(col);

            // looking for two empty cells with the same possibilities pair
            for (int i = 0; i < emptyCells.Count; i++)
            {
                for (int j = i + 1; j < emptyCells.Count; j++)
                {
                    var cell1 = emptyCells[i];
                    var cell2 = emptyCells[j];

                    var possibilities1 = cell1.GetPossibilities();
                    var possibilities2 = cell2.GetPossibilities();

                    if (possibilities1.SetEquals(possibilities2) && possibilities1.Count == 2)
                    {
                        // removing the possibilities pair from the empty cell in the given column
                        for (int row = 0; row < boardSize; row++)
                        {
                            if (row != cell1.Row && row != cell2.Row)
                            {
                                board.RemoveCellPossibility(row, col, possibilities1.First());
                                board.RemoveCellPossibility(row, col, possibilities1.Last());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies the naked pairs sudoku huristic to a given block on the board.
        /// </summary>
        /// <param name="blockStartRow"> The Sudoku board start row of the block to apply the naked pair huristic. </param>
        /// <param name="blockStartCol"> The Sudoku board start column of the block to apply the naked pair huristic. </param>
        /// <returns></returns>
        private static void ApplyNakedPairsInBlock(SudokuBoard board, int blockStartRow, int blockStartCol)
        {
            int boardSize = board.BoardSize;
            int boxLength = (int)Math.Sqrt(boardSize);
            List<BoardCell> emptyCells = board.GetEmptyCellsInBlock(blockStartRow, blockStartCol);

            // looking for two empty cells with the same possibilities pair
            for (int i = 0; i < emptyCells.Count; i++)
            {
                for (int j = i + 1; j < emptyCells.Count; j++)
                {
                    var cell1 = emptyCells[i];
                    var cell2 = emptyCells[j];

                    var possibilities1 = cell1.GetPossibilities();
                    var possibilities2 = cell2.GetPossibilities();

                    if (possibilities1.SetEquals(possibilities2) && possibilities1.Count == 2)
                    {
                        // removing the possibilities pair from the empty cell in the given block
                        for (int row = blockStartRow; row < blockStartRow + boxLength; row++)
                        {
                            for (int col = blockStartCol; col < blockStartCol + boxLength; col++)
                            {
                                if ((row != cell1.Row || col != cell1.Col) && (row != cell2.Row || col != cell2.Col))
                                {
                                    board.RemoveCellPossibility(row, col, possibilities1.First());
                                    board.RemoveCellPossibility(row, col, possibilities1.Last());
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to the whole board.
        /// A hidden single is when a number can only appear in one possible position in a row, column and block.
        /// The function places the number in that position and reduces possibilities for other cells.
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// </summary>
        public static void ApplyHiddenSingles(SudokuBoard board)
        {
            int boardSize = board.BoardSize;
            int boxLength = (int)Math.Sqrt(boardSize);

            // Apply hidden singles for rows
            for (int row = 0; row < boardSize; row++)
            {
                ApplyHiddenSinglesInRow(board, row);
            }

            // Apply hidden singles for columns
            for (int col = 0; col < boardSize; col++)
            {
                ApplyHiddenSinglesInColumn(board, col);
            }

            // Apply hidden singles for blocks
            for (int boxRow = 0; boxRow < boardSize; boxRow += boxLength)
            {
                for (int boxCol = 0; boxCol < boardSize; boxCol += boxLength)
                {
                    ApplyHiddenSinglesInBlock(board, boxRow, boxCol);
                }
            }
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to a given row on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="row"> The Sudoku board row to apply the hidden signles heuristic. </param>
        /// <returns></returns>
        private static void ApplyHiddenSinglesInRow(SudokuBoard board, int row)
        {
            int boardSize = board.BoardSize;
            HashSet<int> usedValues = new HashSet<int>();

            // Check all cells in the row
            for (int col = 0; col < boardSize; col++)
            {
                var cell = board.GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    usedValues.Add(cell.GetValue());
                }
            }

            // Check for hidden singles
            for (int value = 1; value <= boardSize; value++)
            {
                if (!usedValues.Contains(value))  // If the number is not placed yet
                {
                    bool flag = false;
                    int possibleCell = -1;
                    for (int col = 0; col < boardSize; col++)
                    {
                        var cell = board.GetCell(row, col);
                        if (cell.IsEmpty() && cell.GetPossibilities().Contains(value))
                        {
                            if (!flag)
                            {
                                possibleCell = col;
                                flag = true;
                            }
                            else
                            {
                                possibleCell = -1;
                                break;
                            }
                        }
                    }

                    // If there's only one possible cell for the number
                    if (possibleCell != -1)
                    {
                        board.SetCellValue(row, possibleCell, value);
                    }
                }
            }
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to a given column on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="col"> The Sudoku board column to apply the hidden signles heuristic. </param>
        private static void ApplyHiddenSinglesInColumn(SudokuBoard board, int col)
        {
            int boardSize = board.BoardSize;
            HashSet<int> usedValues = new HashSet<int>();

            // Check all cells in the column
            for (int row = 0; row < boardSize; row++)
            {
                var cell = board.GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    usedValues.Add(cell.GetValue());
                }
            }

            // Check for hidden singles
            for (int value = 1; value <= boardSize; value++)
            {
                if (!usedValues.Contains(value))  // If the number is not placed yet
                {
                    bool flag = false;
                    int possibleCell = -1;
                    for (int row = 0; row < boardSize; row++)
                    {
                        var cell = board.GetCell(row, col);
                        if (cell.IsEmpty() && cell.GetPossibilities().Contains(value))
                        {
                            if (!flag) // still have not found a cell with this value possibility
                            {
                                possibleCell = row;
                                flag = true;
                            }
                            else
                            {
                                possibleCell = -1;
                                break;
                            }
                        }
                    }

                    // If there's only one possible cell for the number
                    if (possibleCell != -1)
                    {
                        board.SetCellValue(possibleCell, col, value);
                    }
                }
            }
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to a given block on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="blockStartRow"> The Sudoku board start row of the block to apply the hidden signles heuristic. </param>
        /// <param name="blockStartCol"> The Sudoku board start column of the block to apply the hidden signles heuristic. </param>
        /// <returns></returns>
        private static void ApplyHiddenSinglesInBlock(SudokuBoard board, int blockStartRow, int blockStartCol)
        {
            int boardSize = board.BoardSize;
            int boxLength = (int)Math.Sqrt(boardSize);
            HashSet<int> usedValues = new HashSet<int>();

            // Check all cells in the block
            for (int row = blockStartRow; row < blockStartRow + boxLength; row++)
            {
                for (int col = blockStartCol; col < blockStartCol + boxLength; col++)
                {
                    var cell = board.GetCell(row, col);
                    if (!cell.IsEmpty())
                    {
                        usedValues.Add(cell.GetValue());
                    }
                }
            }

            // Check for hidden singles
            for (int value = 1; value <= boardSize; value++)
            {
                if (!usedValues.Contains(value))  // If the number is not placed yet
                {
                    bool flag = false;
                    int possibleCellRow = -1;
                    int possibleCellCol = -1;
                    for (int row = blockStartRow; row < blockStartRow + boxLength; row++)
                    {
                        for (int col = blockStartCol; col < blockStartCol + boxLength; col++)
                        {
                            var cell = board.GetCell(row, col);
                            if (cell.IsEmpty() && cell.GetPossibilities().Contains(value))
                            {
                                if (!flag)
                                {
                                    possibleCellRow = row;
                                    possibleCellCol = col;
                                    flag = true;
                                }
                                else
                                {
                                    possibleCellRow = -1;
                                    possibleCellCol = -1;
                                    break;
                                }
                            }
                        }
                    }

                    // If there's only one possible cell for the number
                    if (possibleCellRow != -1 && possibleCellCol != -1)
                    {
                        board.SetCellValue(possibleCellRow, possibleCellCol, value);
                    }
                }
            }
        }



        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to a given row on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="row"> The Sudoku board row to apply the hidden pair heuristic. </param>
        /// <returns></returns>
        public static void ApplyHiddenPairsInRow(SudokuBoard board, int row)
        {
            bool foundFlag = false ;
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

                            }
                        }
                    }

                }

            }
        }


        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to a given column on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="col"> The Sudoku board column to apply the hidden pair heuristic. </param>
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


        /// <summary>
        /// Applies the hidden pairs sudoku heuristic to the whole board.
        /// A hidden pair are two empty cells in the same row/column/block that both includes the same two value possibilities that could not appear nowhere else.
        /// The function removes all the other values from these 2 cells to reduce possibilities.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <returns></returns>
        public static void ApplyHiddenPairs(SudokuBoard board)
        {
            int boardSize = board.BoardSize;
            int boxLength = (int)Math.Sqrt(boardSize);

            for (int row = 0; row < boardSize; row++)
            {
                ApplyHiddenPairsInRow(board, row);
            }

            for (int col = 0; col < boardSize; col++)
            {
                ApplyHiddenPairsInColumn(board, col);
            }

            for (int boxRow = 0; boxRow < boardSize; boxRow += boxLength)
            {
                for (int boxCol = 0; boxCol < boardSize; boxCol += boxLength)
                {
                    ApplyHiddenPairsInBlock(board, boxRow, boxCol);
                }
            }
        }
    }
}
