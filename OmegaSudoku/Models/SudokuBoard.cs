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

        public Boolean IsBoardFull()
        {
            return _filledCellsCount == BoardSize * BoardSize;
        }

        public void SetCellValue(int row, int col, int value)
        {
            var cell = _board[(row, col)];
            if (cell.GetValue() == 0 && value != 0) // filling an empty cell
                _filledCellsCount++;  

            if (cell.GetValue() != 0 && value == 0) // removing a filled cell
                _filledCellsCount--;  

            cell.SetValue(value);
        }

         
    }

}
