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
        public int BlockSize { get; private set; }
        private Dictionary<(int, int), BoardCell> _board { get; set; }
        private List<BoardCell> _emptyCells;


        /// <summary>
        /// Constructor to initialize a SudokuBoard object with a given board size and an initial board string.
        /// The constructor creates a board of the given size and sets up the initial values of the board based on the provided string.
        /// A '0' in the string represents an empty cell, and other digits represent filled cells.
        /// </summary>
        /// <param name="boardSize">The size of the Sudoku board.</param>
        /// <param name="boardString">A string representing the initial board. Each '0' in the string represents an empty cell and any other digit represents a filled cell.</param>
        public SudokuBoard(int boardSize, string boardString)
        {
            int value, charIndex = 0;
            BoardSize = boardSize;
            BlockSize = (int)Math.Sqrt(boardSize);
            _board = new Dictionary<(int, int), BoardCell>();
            _emptyCells = new List<BoardCell>();

            // cells initialization
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    BoardCell boardCell;
                    value = boardString[charIndex] - Constants.AsciiDigitDiff;
                    boardCell = new BoardCell(row, col, BoardSize, value);
                    if (value == 0) // empty cell
                        _emptyCells.Add(boardCell); // adding each empty cell the the empty cells list
                    _board.Add((row, col), boardCell); // adding each cell the the board
                    charIndex++;
                }
            }
        }

        public BoardCell GetCell(int row, int col) 
        {
            return _board[(row,col)];
        }

        public List<BoardCell> GetEmptyCellsInRow(int row)
        {
            return _emptyCells.Where(cell => cell.Row == row).ToList();
        }

        public List<BoardCell> GetEmptyCellsInColumn(int col)
        {
            return _emptyCells.Where(cell => cell.Col == col).ToList();
        }

        public List<BoardCell> GetEmptyCellsInBlock(int blockStartRow, int blockStartCol)
        {
            return _emptyCells.Where(cell =>
                cell.Row >= blockStartRow && cell.Row < blockStartRow + BlockSize &&
                cell.Col >= blockStartCol && cell.Col < blockStartCol + BlockSize
            ).ToList();
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
            if (value == 0 && !_emptyCells.Contains(cell))
                _emptyCells.Add((cell));
            else if (value != 0 && _emptyCells.Contains(cell))
                _emptyCells.Remove(cell);
        }

        public void AddCellPossibility(int row, int col, int possibilityValue)
        {
            var cell = _board[(row, col)];
            var oldPossibilitiesCount = cell.GetPossibilitesCount();
            cell.AddPossibility(possibilityValue);
            if (oldPossibilitiesCount == 1 && cell.GetPossibilitesCount() > 1)
                _emptyCells.Add(cell);
        }

        public void RemoveCellPossibility(int row, int col, int possibilityValue)
        {
            var cell = _board[(row, col)];
            cell.RemovePossibility(possibilityValue);
            if (cell.GetPossibilitesCount() == 1)
                _emptyCells.Remove(cell);
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
            int boxFirstRow = (row / BlockSize) * BlockSize;
            int boxFirstCol = (col / BlockSize) * BlockSize;

            for (int r = boxFirstRow; r < boxFirstRow + BlockSize; r++)
            {
                for (int c = boxFirstCol; c < boxFirstCol + BlockSize; c++)
                {
                    var cell = _board[(r, c)];
                    if (cell.GetValue() == value)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a value can be placed in a specific cell without violating Sudoku rules.
        /// Verifies the value does not appear in the same row / column / block.
        /// </summary>
        /// <param name="row">The row of the target cell.</param>
        /// <param name="col">The column of the target cell.</param>
        /// <param name="value">The value to check for placement.</param>
        /// <returns>True if the value can be placed without violating Sudoku rules, else - false.</returns>
        public bool CanValueBePlaced(int row, int col, int value)
        {
            return !IsValueInRow(row,value) && !IsValueInCol(col,value) && !IsValueInBlock(row,col,value);
        }

        /// <summary>
        /// Checks if any cell on the board has zero possible values.
        /// It used to indicate an unsolvable board state.
        /// </summary>
        /// <returns>True if a cell has no possible values, false if all cells have at least one possible value.</returns>
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

        public bool IsBoardFull()
        {
            return _emptyCells.Count == 0;
        }

        /// <summary>
        /// updates a given Sudoku board cell his row value possibilities.
        /// </summary>
        /// <param name="cell"> The Sudoku board cell to be updated. </param>
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
        public void UpdatePossibleBlockValues(BoardCell cell)
        {
            int boxFirstRow = (cell.Row / BlockSize) * BlockSize;
            int boxFirstCol = (cell.Col / BlockSize) * BlockSize;

            for (int row = boxFirstRow; row < boxFirstRow + BlockSize; row++)
            {
                for (int col = boxFirstCol; col < boxFirstCol + BlockSize; col++)
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
        public void UpdatePossiableValues(BoardCell cell)
        {
            UpdatePossibleRowValues(cell);
            UpdatePossibleColumnValues(cell);
            UpdatePossibleBlockValues(cell);
        }

        /// <summary>
        /// updates all of a sudoku board cells possibilities.
        /// </summary>
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
            BoardCell lowestCell = null;
            int lowestPossibilities = BoardSize;

            foreach (BoardCell emptycell in _emptyCells)
            {
                if (emptycell.GetPossibilitesCount() <= lowestPossibilities)
                {
                    lowestPossibilities = emptycell.GetPossibilitesCount();
                    lowestCell = emptycell;
                }
            }
            return lowestCell;
        }

        /// <summary>
        /// returns a set with all the unused values in a given board row.
        /// </summary>
        /// <param name="row"> The board row to be searched. </param>
        /// <returns> returns a set with all the unused values in the board row.</returns>
        public HashSet<int> GetRowUnusedValuesSet(int row)
        {
            HashSet<int> unusedValues = new HashSet<int>();
            unusedValues.UnionWith(Enumerable.Range(1, BoardSize)); // initializing the set with all the possibilities

            // removing the placed values in the row from the row possibilities set
            for (int col = 0; col < BoardSize; col++)
            {
                var cell = GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    unusedValues.Remove(cell.GetValue());
                }
            }

            return unusedValues;
        }


        /// <summary>
        /// returns a set with all the unused values in a given board column.
        /// </summary>
        /// <param name="col"> The board column to be searched. </param>
        /// <returns> returns a set with all the unused values in the board column.</returns>
        public HashSet<int> GetColumnUnusedValuesSet(int col)
        {
            HashSet<int> unusedValues = new HashSet<int>();
            unusedValues.UnionWith(Enumerable.Range(1, BoardSize)); // initializing the set with all the possibilities

            // removing the placed values in the row from the row possibilities set
            for (int row = 0; row < BoardSize; row++)
            {
                var cell = GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    unusedValues.Remove(cell.GetValue());
                }
            }

            return unusedValues;
        }


        /// <summary>
        /// returns a set with all the unused values in a given board block.
        /// </summary>
        /// <param name="blockStartRow"> The board starting row of te block to be searched. </param>
        /// <param name="blockStartCol"> The board starting column of te block to be searched. </param>
        /// <returns> returns a set with all the unused values in the board block.</returns>
        public HashSet<int> GetBlockUnusedValuesSet(int blockStartRow, int blockStartCol)
        {
            HashSet<int> unusedValues = new HashSet<int>();
            unusedValues.UnionWith(Enumerable.Range(1, BoardSize)); // initializing the set with all the possibilities

            // removing the placed values in the row from the row possibilities set
            for (int row = blockStartRow; row < blockStartRow + BlockSize; row++)
            {
                for (int col = blockStartCol; col < blockStartCol + BlockSize; col++)
                {
                    var cell = GetCell(row, col);
                    if (!cell.IsEmpty())
                    {
                        unusedValues.Remove(cell.GetValue());
                    }
                }
            }

            return unusedValues;
        }

        /// <summary>
        /// returns a set with all the used values in a given board row.
        /// </summary>
        /// <param name="row"> The board row to be searched. </param>
        /// <returns> returns a set with all the used values in the board row.</returns>
        public HashSet<int> GetRowUsedValuesSet(int row)
        {
            HashSet<int> usedValues = new HashSet<int>();

            // removing the placed values in the row from the row possibilities set
            for (int col = 0; col < BoardSize; col++)
            {
                var cell = GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    usedValues.Add(cell.GetValue());
                }
            }

            return usedValues;
        }


        /// <summary>
        /// returns a set with all the used values in a given board column.
        /// </summary>
        /// <param name="col"> The board column to be searched. </param>
        /// <returns> returns a set with all the used values in the board column.</returns>
        public HashSet<int> GetColumnUsedValuesSet(int col)
        {
            HashSet<int> usedValues = new HashSet<int>();

            // removing the placed values in the row from the row possibilities set
            for (int row = 0; row < BoardSize; row++)
            {
                var cell = GetCell(row, col);
                if (!cell.IsEmpty())
                {
                    usedValues.Add(cell.GetValue());
                }
            }

            return usedValues;
        }


        /// <summary>
        /// returns a set with all the used values in a given board block.
        /// </summary>
        /// <param name="blockStartRow"> The board starting row of te block to be searched. </param>
        /// <param name="blockStartCol"> The board starting column of te block to be searched. </param>
        /// <returns> returns a set with all the used values in the board block.</returns>
        public HashSet<int> GetBlockUsedValuesSet(int blockStartRow, int blockStartCol)
        {
            HashSet<int> usedValues = new HashSet<int>();

            // removing the placed values in the row from the row possibilities set
            for (int row = blockStartRow; row < blockStartRow + BlockSize; row++)
            {
                for (int col = blockStartCol; col < blockStartCol + BlockSize; col++)
                {
                    var cell = GetCell(row, col);
                    if (!cell.IsEmpty())
                    {
                        usedValues.Add(cell.GetValue());
                    }
                }
            }

            return usedValues;
        }

        /// <summary>
        /// Converts the sudoku board to a single long string representation.
        /// </summary>
        /// <returns>A single long string representing the board.</returns>
        public string ConvertBoardToString()
        {
            StringBuilder boardString = new StringBuilder();

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    char value = (char)(GetCellValue(row, col) + Constants.AsciiDigitDiff);
                    boardString.Append(value);
                }
            }
            return boardString.ToString();
        }

        /// <summary>
        /// Saves the board state into a dictionary with cell as the key and possibilities as the value.
        /// </summary>
        /// <returns>A dictionary with the current board state.</returns>
        public Dictionary<BoardCell, HashSet<int>> SaveBoardState()
        {
            var state = new Dictionary<BoardCell, HashSet<int>>();

            foreach (BoardCell emptyCell in _emptyCells)
            {
                state[emptyCell] = new HashSet<int>(emptyCell.GetPossibilities());
            }

            return state;
        }

        /// <summary>
        /// Restores a board state by setting the current board cells possibilities from the saved state.
        /// </summary>
        /// <param name="savedState"> The Sudoku board state to apply.</param>
        public void RestoreBoardState(Dictionary<BoardCell, HashSet<int>> savedState)
        {
            foreach (var savedCell in savedState)
            {
                BoardCell cell = savedCell.Key;
                HashSet<int> possibilities = savedCell.Value;
                cell.SetPossibilities(possibilities);
                if (!_emptyCells.Contains(cell))
                    _emptyCells.Add(cell);
            }
        }
    }

}

