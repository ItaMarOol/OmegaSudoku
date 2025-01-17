using OmegaSudoku.Models;
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

        public SudokuSolver(SudokuBoard board)
        { 
            _board = board;
            _boardSize = board.BoardSize;
        }

        /// <summary>
        /// updates a given Sudoku board cell his row value possibilities.
        /// </summary>
        /// <param name="cell">The Sudoku board cell to be updated.</param>
        /// <returns></returns>
        public void UpdatePossibleRowValues(BoardCell cell) 
        {
            int row = cell.Row;
            for (int col = 0; col < _boardSize; col++)
            {
                if (col != cell.Col)
                {
                    int value = _board.GetCellValue(row, col);
                    cell.RemovePossibility(value);
                }
            }
        }

        /// <summary>
        /// updates a given Sudoku board cell his column value possibilities.
        /// </summary>
        /// <param name="cell">The Sudoku board cell to be updated.</param>
        /// <returns></returns>
        public void UpdatePossibleColumnValues(BoardCell cell)
        {
            int col = cell.Col;
            for (int row = 0; row < _boardSize; row++)
            {
                if (row != cell.Row)
                {
                    int value = _board.GetCellValue(row, col);
                    cell.RemovePossibility(value);
                }
            }
        }

        /// <summary>
        /// updates a given Sudoku board cell his block value possibilities.
        /// </summary>
        /// <param name="cell">The Sudoku board cell to be updated.</param>
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
                        cell.RemovePossibility(value);
                    }
                }
            }
        }

        /// <summary>
        /// updates a given Sudoku board cell all of his value possibilities.
        /// </summary>
        /// <param name="cell">The Sudoku board cell to be updated.</param>
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
                        UpdatePossiableValues(cell);
                }
            }
        }

    }
}
