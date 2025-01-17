using OmegaSudoku.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Models
{
    public class SudokuBoard
    {
        public int BoardSize { get; private set; }
        private int _filledCellsCount { get; set; }
        private Dictionary<(int, int), BoardCell> _board { get; set; }


        public SudokuBoard(int boardSize)
        {

            BoardSize = boardSize;
            _filledCellsCount = 0;
            _board = new Dictionary<(int, int), BoardCell>();

            // cells initialization
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var boardCell = new BoardCell(row, col, BoardSize, 0);
                    _board.Add((row, col), boardCell);
                }
            }
        }

        public SudokuBoard(int boardSize, string boardString)
        {

            BoardSize = boardSize;
            _filledCellsCount = 0;
            _board = new Dictionary<(int, int), BoardCell>();

            int remainingCells = BoardSize * boardSize - boardString.Length;

            // cells initialization
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    BoardCell boardCell;

                    if (_filledCellsCount < boardString.Length)
                    {
                        int value = boardString[_filledCellsCount] - Constants.AsciiDigitDiff;
                        boardCell = new BoardCell(row, col, BoardSize, value);

                    }
                    else
                    {
                        boardCell = new BoardCell(row, col, BoardSize, 0);
                    }
                    _board.Add((row, col), boardCell);
                    _filledCellsCount++;


                }
            }
        }

        public bool IsBoardFull()
        {
            return _filledCellsCount == BoardSize * BoardSize;
        }

        public BoardCell GetCell(int row, int col) 
        {
            return _board[(row,col)];
        }

        public int GetCellValue(int row, int col)
        {
            var cell = _board[(row, col)];
            int value = cell.GetValue();

            return value;
        }

        public void SetCellValue(int row, int col, int value)
        {
            var cell = _board[(row, col)];
            if (cell.GetValue() == 0 && value != 0) // filling an empty cell
                _filledCellsCount++;  

            if (cell.GetValue() != 0 && value == 0) // removing a filled cell
                _filledCellsCount--;  

            cell.SetValue(value, BoardSize);
        }

         
    }

}
