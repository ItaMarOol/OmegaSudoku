using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Validators
{
    public class BoardValidator
    {
        private readonly SudokuBoard _board;

        public BoardValidator(SudokuBoard board)
        {
            _board = board;
        }

        public bool IsRowValid(int row)
        {
            HashSet<int> seenValues = new HashSet<int>();
            for (int col = 0; col < _board.BoardSize; col++)
            {
                int value = _board.GetCellValue(row, col);
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

        public bool IsColumnValid(int col)
        {
            HashSet<int> seenValues = new HashSet<int>();
            for (int row = 0; row < _board.BoardSize; row++)
            {
                int value = _board.GetCellValue(row, col);
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

        public bool IsBlockValid(int startRow, int startCol, int blockSize)
        {
            HashSet<int> seenValues = new HashSet<int>();
            for (int row = startRow; row < startRow + blockSize; row++)
            {
                for (int col = startCol; col < startCol + blockSize; col++)
                {
                    int value = _board.GetCellValue(row, col);
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


        public bool IsBoardValid()
        {
            int boardSize = _board.BoardSize;
            int blockSize = (int)Math.Sqrt(boardSize);
            for (int row = 0; row < boardSize; row++)
            {
                if (!IsRowValid(row))
                    return false;
            }

            for (int col = 0; col < boardSize; col++)
            {
                if (!IsColumnValid(col))
                    return false;
            }

            for (int row = 0; row < boardSize; row += blockSize)
            {
                for (int col = 0; col < boardSize; col += blockSize)
                {
                    if (!IsBlockValid(row, col, blockSize))
                        return false;
                }
            }

            return true;
        }
    }

}
