using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Heuristics
{

    /// <summary>
    /// This class represents a heuristic that applies the "Hidden Singles" technique in sudoku. 
    /// A hidden single is when a number can only appear in one possible position in a row/column/block.
    /// The heuristic finds such singles, places the value in that cell and also eliminats the other cell possibilities 
    /// in that row/column/block to reduce the possibilities, which helps to solve the board faster.
    /// </summary>
    public class HiddenSinglesHeuristic : IHeuristic
    {

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to the whole board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>

        public bool ApplyHeuristic(SudokuBoard board)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;

            // Apply hidden singles for rows
            for (int row = 0; row < boardSize; row++)
            {
                changeFlag |= ApplyHiddenSinglesInRow(board, row);
            }

            // Apply hidden singles for columns
            for (int col = 0; col < boardSize; col++)
            {
                changeFlag |= ApplyHiddenSinglesInColumn(board, col);
            }

            // Apply hidden singles for blocks
            for (int blockRow = 0; blockRow < boardSize; blockRow += board.BlockSize)
            {
                for (int blockCol = 0; blockCol < boardSize; blockCol += board.BlockSize)
                {
                    changeFlag |= ApplyHiddenSinglesInBlock(board, blockRow, blockCol);
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to a given row on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="row"> The Sudoku board row to apply the hidden signles heuristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool ApplyHiddenSinglesInRow(SudokuBoard board, int row)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;

            // set with all the used values in the row
            HashSet<int> usedValues = board.GetRowUsedValuesSet(row);

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
                        changeFlag = true;
                    }
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to a given column on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="col"> The Sudoku board column to apply the hidden signles heuristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool ApplyHiddenSinglesInColumn(SudokuBoard board, int col)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;

            // set with all the used values in the column
            HashSet<int> usedValues = board.GetColumnUsedValuesSet(col);

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
                        changeFlag = true;  
                    }
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the hidden singles sudoku heuristic to a given block on the board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <param name="blockStartRow"> The Sudoku board start row of the block to apply the hidden signles heuristic. </param>
        /// <param name="blockStartCol"> The Sudoku board start column of the block to apply the hidden signles heuristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool ApplyHiddenSinglesInBlock(SudokuBoard board, int blockStartRow, int blockStartCol)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;

            // set with all the used values in the block
            HashSet<int> usedValues = board.GetBlockUsedValuesSet(blockStartRow, blockStartCol);

            // Check for hidden singles
            for (int value = 1; value <= boardSize; value++)
            {
                if (!usedValues.Contains(value))  // If the number is not placed yet
                {
                    bool flag = false;
                    int possibleCellRow = -1;
                    int possibleCellCol = -1;
                    for (int row = blockStartRow; row < blockStartRow + board.BlockSize; row++)
                    {
                        for (int col = blockStartCol; col < blockStartCol + board.BlockSize; col++)
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
                        changeFlag = true;
                    }
                }
            }
            return changeFlag;
        }
    }
}
