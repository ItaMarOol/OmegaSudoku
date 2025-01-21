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
        private Dictionary<(int, int), BoardCell> _board { get; set; }
 

        public SudokuBoard(int boardSize)
        {

            BoardSize = boardSize;
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
            for (int row = 0; row <= BoardSize; row++) 
            {
                for (int col = 0; col <= BoardSize; col++)
                {
                    if (GetCellValue(row,col) == 0)
                        return false;
                }
            }
            return true;
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
            cell.SetValue(value, BoardSize);
        }

        public void AddCellPossibility(int row, int col, int possibilityValue)
        {
            var cell = _board[(row, col)];
            cell.AddPossibility(possibilityValue);
        }

        public void RemoveCellPossibility(int row, int col, int possibilityValue)
        {
            var cell = _board[(row, col)];
            cell.RemovePossibility(possibilityValue);
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

        public bool HasZeroCountCell()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (_board[(row, col)].GetPossibilitesCount() == 0)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// updates a given Sudoku board cell his row value possibilities.
        /// </summary>
        /// <param name="cell"> The Sudoku board cell to be updated. </param>
        /// <returns></returns>
        public void UpdatePossibleRowValues(BoardCell cell)
        {
            int row = cell.Row;
            for (int col = 0; col < BoardSize; col++)
            {
                if (col != cell.Col)
                {
                    int value = GetCellValue(row, col);
                    RemoveCellPossibility(cell.Row, cell.Col, value);
                }
            }
        }

        /// <summary>
        /// updates a given Sudoku board cell his column value possibilities.
        /// </summary>
        /// <param name="cell"> The Sudoku board cell to be updated. </param>
        /// <returns></returns>
        public void UpdatePossibleColumnValues(BoardCell cell)
        {
            int col = cell.Col;
            for (int row = 0; row < BoardSize; row++)
            {
                if (row != cell.Row)
                {
                    int value = GetCellValue(row, col);
                    RemoveCellPossibility(cell.Row, cell.Col, value);
                }
            }
        }

        /// <summary>
        /// updates a given Sudoku board cell his block value possibilities.
        /// </summary>
        /// <param name="cell"> The Sudoku board cell to be updated. </param>
        /// <returns></returns>
        public void UpdatePossibleBlockValues(BoardCell cell)
        {

            int boxSize = (int)Math.Sqrt(BoardSize);
            int boxFirstRow = (cell.Row / boxSize) * boxSize;
            int boxFirstCol = (cell.Col / boxSize) * boxSize;

            for (int row = boxFirstRow; row < boxFirstRow + boxSize; row++)
            {
                for (int col = boxFirstCol; col < boxFirstCol + boxSize; col++)
                {
                    if (col != cell.Col && row != cell.Row)
                    {
                        var value = GetCellValue(row, col);
                        RemoveCellPossibility(cell.Row, cell.Col, value);
                    }
                }
            }
        }

        /// <summary>
        /// updates a given Sudoku board cell all of his value possibilities.
        /// </summary>
        /// <param name="cell"> The Sudoku board cell to be updated. </param>
        /// <returns></returns>
        public void UpdatePossiableValues(BoardCell cell)
        {
            UpdatePossibleRowValues(cell);
            UpdatePossibleColumnValues(cell);
            UpdatePossibleBlockValues(cell);
        }

        /// <summary>
        /// updates all of a sudoku board cells possibilities.
        /// </summary>
        /// <returns></returns>
        public void UpdateAllCellsPossibilities()
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var cell = _board[(row, col)];
                    if (cell.IsEmpty())
                    {
                        UpdatePossiableValues(cell);
                    }
                }
            }
        }

        /// <summary>
        /// getting the empty cell with the lowest possibilities on the board.
        /// </summary>
        /// <returns> The cel with the lowest possibilites on the board. if not found - returns null. </returns>
        public BoardCell GetLowestPossibilitesCell()
        {
            BoardCell LowestCell = null;
            int LowestPossibilities = BoardSize;

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var cell = _board[(row, col)];
                    int possibilitiesCount = cell.GetPossibilitesCount();
                    if (cell.IsEmpty() && possibilitiesCount <= LowestPossibilities)
                    {
                        LowestCell = cell;
                        LowestPossibilities = possibilitiesCount;
                    }
                }
            }
            return LowestCell;
        }

        /// <summary>
        /// saving the board state into a dictionary with cell as the key and possibilities as the value.
        /// </summary>
        /// <returns>A dictionary with the current board state.</returns>
        public Dictionary<BoardCell, HashSet<int>> SaveBoardState()
        {
            var state = new Dictionary<BoardCell, HashSet<int>>();
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var cell = _board[(row, col)];
                    if (cell.IsEmpty())
                    {
                        state[cell] = new HashSet<int>(cell.GetPossibilities());
                    }
                }
            }
            return state;
        }

        /// <summary>
        /// restoring a board state by setting the current board cells possibilities from the saved state.
        /// </summary>
        /// <param name="savedState"> The Sudoku board state to apply. </param>
        /// <returns></returns>
        public void RestoreBoardState(Dictionary<BoardCell, HashSet<int>> savedState)
        {
            foreach (var savedCell in savedState)
            {
                BoardCell cell = savedCell.Key;
                HashSet<int> possibilities = savedCell.Value;
                cell.SetPossibilities(possibilities);
            }
        }


    }

}
