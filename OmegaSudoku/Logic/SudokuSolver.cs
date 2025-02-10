using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic
{
    public static class SudokuSolver
    {

        /// <summary>
        /// Setting all the board cells values by backtracking algorithm.
        /// </summary>
        /// <param name="board"> Sudoku board to be solved. </param>
        /// <returns> True if the sudoku board has been filled correctly with values. else - returns false </returns>
        private static bool SolveWithBackTrack(SudokuBoard board)
        {
            List<BoardCell> unassignedCells = new List<BoardCell>();

            if (board.HasZeroCountCell() || !BoardValidator.IsBoardValid(board))
                return false;

            if (board.IsBoardFull()) // check if the board has been solved
                return true;

            BoardCell lowestCell = board.GetLowestPossibilitesCell(); // gets the board cell with the lowest value possibilities.
            if (lowestCell == null) // checks if there is not a lowest cell, which means that the board have been solved.
                return true;

            HashSet<int> possibilities = new HashSet<int>(lowestCell.GetPossibilities()); // getting the lowest cell possibilities
            foreach (int possibleValue in possibilities)
            {
                int lowestRow = lowestCell.Row;
                int lowestCol = lowestCell.Col;
                if (board.CanValueBePlaced(lowestRow, lowestCol, possibleValue)) // checks if a possible value can be placed in the cell
                {
                    Dictionary<BoardCell, HashSet<int>> savedState = board.SaveBoardState(); // saving the board state
                    board.SetCellValue(lowestRow, lowestCol, possibleValue);
                    try 
                    { 
                        SudokuHeuristics.ApplySudokuHeuristics(board); // applying sudoku heuristics to the board to reduce possibilities
                    }
                    catch // hidden pairs heuristics detected an invalid board
                    {
                        board.SetCellValue(lowestRow, lowestCol, 0);
                        board.RestoreBoardState(savedState);
                        continue; // countinue to the next possible value (if there is one)
                    }

                    if (SolveWithBackTrack(board)) // recursive call to the backtrack (with heuristics) solving
                        return true;

                    // the possible value is incorrect
                    board.SetCellValue(lowestRow, lowestCol, 0); // resetting the attempted cell
                    board.RestoreBoardState(savedState); // restoring the board state
                }

            }
            return false;
        }

        /// <summary>
        /// Solving the board.
        /// </summary>
        /// <exception cref="UnsolvableBoardException">Thrown when there are more than 2 values that have to be in exactly 2 cells, which means that the board is unsolvable.</exception>
        /// <returns> True if the sudoku board has been solved. else - returns false </returns>
        public static bool Solve(SudokuBoard board)
        {
            bool flag;

            board.UpdateAllCellsPossibilities(); // updates all the board cells possibilties by sudoku rules.
            SudokuHeuristics.ApplySudokuHeuristics(board); // applying sudoku heuristics to the board to reduce possibilities

            flag = SolveWithBackTrack(board); // trying to solve the sudoku with backtracking algorithm.

            if (!flag) // could not solve the board, which means that the board is unsolveable
                throw new UnsolvableBoardException();

            return flag;
        }


    }
}
