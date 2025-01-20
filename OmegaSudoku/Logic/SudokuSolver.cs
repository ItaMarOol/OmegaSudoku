using OmegaSudoku.Models;
using OmegaSudoku.Services.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic
{
    public class SudokuSolver
    {
        private SudokuBoard _board;
        private int _boardSize;
        public CliOutputHandler CliOutputHandler;

        public SudokuSolver(SudokuBoard board)
        { 
            _board = board;
            _boardSize = board.BoardSize;
            CliOutputHandler = new CliOutputHandler();
        }
        public SudokuBoard GetSudokuBoard()
        {
            return _board;
        }

        /// <summary>
        /// updates a given Sudoku board cell his row value possibilities.
        /// </summary>
        /// <param name="cell"> The Sudoku board cell to be updated. </param>
        /// <returns></returns>
        public void UpdatePossibleRowValues(BoardCell cell) 
        {
            int row = cell.Row;
            for (int col = 0; col < _boardSize; col++)
            {
                if (col != cell.Col)
                {
                    int value = _board.GetCellValue(row, col);
                    _board.RemoveCellPossibility(cell.Row, cell.Col, value);
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
            for (int row = 0; row < _boardSize; row++)
            {
                if (row != cell.Row)
                {
                    int value = _board.GetCellValue(row, col);
                    _board.RemoveCellPossibility(cell.Row, cell.Col, value);
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

            int boxSize = (int)Math.Sqrt(_boardSize);
            int boxFirstRow = (cell.Row / boxSize) * boxSize;
            int boxFirstCol = (cell.Col / boxSize) * boxSize;

            for (int row = boxFirstRow; row < boxFirstRow + boxSize; row++)
            {
                for (int col = boxFirstCol; col < boxFirstCol + boxSize; col++)
                {
                    if (col != cell.Col && row != cell.Row)
                    {
                        var value = _board.GetCellValue(row, col);
                        _board.RemoveCellPossibility(cell.Row, cell.Col, value);
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
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    var cell = _board.GetCell(row, col);
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
        private BoardCell GetLowestPossibilitesCell()
        {
            BoardCell LowestCell = null;
            int LowestPossibilities = _boardSize;

            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                { 
                    var cell = _board.GetCell(row,col);
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
        private Dictionary<BoardCell, HashSet<int>> SaveBoardState()
        {
            var state = new Dictionary<BoardCell, HashSet<int>>();
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    var cell = _board.GetCell(row, col);
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
        /// <param name="savedState"> The Sudoku board state to be applied. </param>
        /// <returns></returns>
        private void RestoreBoardState(Dictionary<BoardCell, HashSet<int>> savedState)
        {
            foreach (var savedCell in savedState)
            {
                BoardCell cell = savedCell.Key;
                HashSet<int> possibilities = savedCell.Value;
                cell.SetPossibilities(possibilities);
            }
        }

        /// <summary>
        /// setting all the board cells values by backtracking algorithm.
        /// </summary>
        /// <returns> True if the sudoku board has been filled correctly with values. else - returns false </returns>
        private bool BackTrack()
        {
            BoardCell lowestCell = GetLowestPossibilitesCell(); // getting the cell with the lowest possibilities
            if (lowestCell == null && _board.IsBoardFull()) // check if the board has been solved
                return true;

            HashSet<int> possibilities = new HashSet<int>(lowestCell.GetPossibilities()); // getting the lowest cell possibilities
            Dictionary<BoardCell, HashSet<int>> savedState = SaveBoardState(); // saving the board state

            foreach (int possibleValue in possibilities)
            {
                int lowestRow = lowestCell.Row;
                int lowestCol = lowestCell.Col;
                if (_board.CanValueBePlaced(lowestRow, lowestCol, possibleValue))
                {
                    savedState = SaveBoardState(); 
                    _board.SetCellValue(lowestRow, lowestCol, possibleValue);
                    // UpdateAllCellsPossibilities();

                    if (BackTrack())
                        return true;

                    // the possible value is incorrect
                    _board.SetCellValue(lowestRow, lowestCol, 0);
                    RestoreBoardState(savedState); 
                }

            }

            return false;
        }

        /// <summary>
        /// solving the board.
        /// </summary>
        /// <returns> True if the sudoku board has been solved. else - returns false </returns>
        public bool Solve()
        {
            bool flag;

            UpdateAllCellsPossibilities(); // updates all the board cells possibilties by sudoku rules.
            flag = BackTrack();
           
            return flag;
        }


    }
}
