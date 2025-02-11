using OmegaSudoku.Exceptions;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Validators
{

    /// <summary>
    /// This class represents a static utility for validating a sudoku board.
    /// It includes methods to ensure that the rows, columns, and blocks of the board do not contain duplicate values.
    /// </summary>
    public static class BoardValidator
    {

        /// <summary>
        /// Validates a row of the Sudoku board. Checks that there are no duplicate values.
        /// </summary>
        /// <param name="board">The Sudoku board to validate.</param>
        /// <param name="row">The row to validate.</param>
        /// <returns>True if the row is valid. else - false.</returns>
        private static bool IsRowValid(SudokuBoard board, int row)
        {
            HashSet<int> seenValues = new HashSet<int>();
            for (int col = 0; col < board.BoardSize; col++)
            {
                int value = board.GetCellValue(row, col);
                if (value != 0)
                {
                    if (seenValues.Contains(value))
                    {
                        return false;
                    }
                    seenValues.Add(value);
                }
            }
            return true;
        }

        /// <summary>
        /// Validates a column of the Sudoku board. Checks that there are no duplicate values.
        /// </summary>
        /// <param name="board">The Sudoku board to validate.</param>
        /// <param name="col">The column to validate.</param>
        /// <returns>True if the row is valid. else - false.</returns>
        private static bool IsColumnValid(SudokuBoard board, int col)
        {
            HashSet<int> seenValues = new HashSet<int>();

            for (int row = 0; row < board.BoardSize; row++)
            {
                int value = board.GetCellValue(row, col);
                if (value != 0)
                {
                    if (seenValues.Contains(value))
                    {
                        return false;
                    }
                    seenValues.Add(value);
                }
            }
            return true;
        }

        /// <summary>
        /// Validates a block of the Sudoku board. Checks that there are no duplicate values.
        /// </summary>
        /// <param name="board">The Sudoku board to validate.</param>
        /// <param name="startRow">The start row of the block to validate.</param>
        /// <param name="startCol">The start column of the block to validate.</param>
        /// <returns>True if the block is valid. else - false.</returns>
        private static bool IsBlockValid(SudokuBoard board, int startRow, int startCol)
        {
            HashSet<int> seenValues = new HashSet<int>();

            for (int row = startRow; row < startRow + board.BlockSize; row++)
            {
                for (int col = startCol; col < startCol + board.BlockSize; col++)
                {
                    int value = board.GetCellValue(row, col);
                    if (value != 0)
                    {
                        if (seenValues.Contains(value))
                        {
                            return false;
                        }
                        seenValues.Add(value);
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Validates a Sudoku board. Checks that there are no duplicate values.
        /// </summary>
        /// <param name="board">The Sudoku board to validate.</param>
        /// <returns>True if the board is valid. else - false.</returns>
        public static bool IsBoardValid(SudokuBoard board)
        {
            int boardSize = board.BoardSize;
            int blockSize = board.BlockSize;
            for (int row = 0; row < boardSize; row++)
            {
                if (!IsRowValid(board, row))
                    return false;
            }

            for (int col = 0; col < boardSize; col++)
            {
                if (!IsColumnValid(board, col))
                    return false;
            }

            for (int row = 0; row < boardSize; row += blockSize)
            {
                for (int col = 0; col < boardSize; col += blockSize)
                {
                    if (!IsBlockValid(board, row, col))
                        return false;
                }
            }

            return true;
        }

    }

}
