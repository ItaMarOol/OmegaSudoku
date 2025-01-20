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

            int index = 0;
            int remainingCells = BoardSize * boardSize - boardString.Length;

            // cells initialization
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    BoardCell boardCell;

                    if (index < boardString.Length)
                    {
                        int value = boardString[index] - Constants.AsciiDigitDiff;
                        boardCell = new BoardCell(row, col, BoardSize, value);
                        if (value != 0)
                            _filledCellsCount++;
                    }
                    else
                    {
                        boardCell = new BoardCell(row, col, BoardSize, 0);
                    }
                    _board.Add((row, col), boardCell);
                    index++;


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

        public void AddCellPossibility(int row, int col, int possibilityValue)
        {
            var cell = _board[(row, col)];
            bool cellWasEmptyFlag = cell.IsEmpty();
            cell.AddPossibility(possibilityValue);
            if (!cellWasEmptyFlag && cell.IsEmpty())
                _filledCellsCount--;
        }

        public void RemoveCellPossibility(int row, int col, int possibilityValue)
        {
            var cell = _board[(row, col)];
            bool cellWasEmptyFlag = cell.IsEmpty();
            cell.RemovePossibility(possibilityValue);
            if (cellWasEmptyFlag && !cell.IsEmpty())
                _filledCellsCount++;
        }

        public bool IsValueInRow(int row, int value)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                var cell = _board[(row,col)];
                if (cell.GetValue() == value)
                    return true;
            }
            return false; 
        }

        public bool IsValueInCol(int col, int value)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                var cell = _board[(row, col)];
                if (cell.GetValue() == value)
                    return true;
            }
            return false;
        }

        public bool IsValueInBlock(int row, int col, int value)
        {
            int boxSize = (int)Math.Sqrt(BoardSize);
            int boxFirstRow = (row / boxSize) * boxSize;
            int boxFirstCol = (col / boxSize) * boxSize;

            for (int r = boxFirstRow; r < boxFirstRow + boxSize; r++)
            {
                for (int c = boxFirstCol; c < boxFirstCol + boxSize; c++)
                {
                    var cell = _board[(r, c)];
                    if (cell.GetValue() == value)
                        return true;
                }
            }

            return false;
        }

        public bool CanValueBePlaced(int row, int col, int value)
        {
            return !IsValueInRow(row,value) && !IsValueInCol(col,value) && !IsValueInBlock(row,col,value);
        }



    }

}
