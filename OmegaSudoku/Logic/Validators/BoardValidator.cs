using OmegaSudoku.Exceptions;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Validators
{
    public static class BoardValidator
    {
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

        private static bool IsBlockValid(SudokuBoard board, int startRow, int startCol, int blockSize)
        {
            HashSet<int> seenValues = new HashSet<int>();
            for (int row = startRow; row < startRow + blockSize; row++)
            {
                for (int col = startCol; col < startCol + blockSize; col++)
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


        public static bool IsBoardValid(SudokuBoard board)
        {
            int boardSize = board.BoardSize;
            int blockSize = (int)Math.Sqrt(boardSize);
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
                    if (!IsBlockValid(board, row, col, blockSize))
                        return false;
                }
            }

            return true;
        }

    }

}
