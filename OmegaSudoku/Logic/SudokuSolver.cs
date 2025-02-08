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
        /// <returns> True if the sudoku board has been filled correctly with values. else - returns false </returns>
        private static bool BackTrack(SudokuBoard board)
        {
            List<BoardCell> unassignedCells = new List<BoardCell>();

            if (board.HasZeroCountCell() || !BoardValidator.IsBoardValid(board))
                return false;

            if (board.IsBoardFull()) // check if the board has been solved
                return true;

            BoardCell lowestCell = board.GetLowestPossibilitesCell();
            if (lowestCell == null) 
                return true;

            HashSet<int> possibilities = new HashSet<int>(lowestCell.GetPossibilities()); // getting the lowest cell possibilities
            foreach (int possibleValue in possibilities)
            {
                int lowestRow = lowestCell.Row;
                int lowestCol = lowestCell.Col;
                if (board.CanValueBePlaced(lowestRow, lowestCol, possibleValue))
                {
                    Dictionary<BoardCell, HashSet<int>> savedState = board.SaveBoardState(); // saving the board state
                    board.SetCellValue(lowestRow, lowestCol, possibleValue);


                    SudokuHeuristics.ApplyNakedPairs(board); // updates all the board cells possibilties by sudoku 'naked pair' heuristic.
                    board.UpdateAllCellsPossibilities(); // updates all the board cells possibilties by sudoku rules.
                    SudokuHeuristics.ApplyHiddenSingles(board); // updates all the board cells possibilties by sudoku 'hidden singles' heuristic.
                    try 
                    { 
                        SudokuHeuristics.ApplyHiddenPairs(board); 
                    }
                    catch (Exception e) // hidden pairs heuristics detected invalid board
                    {
                        board.SetCellValue(lowestRow, lowestCol, 0);
                        board.RestoreBoardState(savedState);
                    }

                    if (BackTrack(board))
                        return true;

                    // the possible value is incorrect
                    board.SetCellValue(lowestRow, lowestCol, 0);
                    board.RestoreBoardState(savedState);
                }

            }
            return false;
        }

        /// <summary>
        /// Solving the board.
        /// </summary>
        /// <returns> True if the sudoku board has been solved. else - returns false </returns>
        public static bool Solve(SudokuBoard board)
        {
            bool flag;

            board.UpdateAllCellsPossibilities(); // updates all the board cells possibilties by sudoku rules.
            SudokuHeuristics.ApplyNakedPairs(board); // updates all the board cells possibilties by sudoku 'naked pair' heuristic.
            SudokuHeuristics.ApplyHiddenSingles(board); // updates all the board cells possibilties by sudoku 'hidden singles' heuristic.
            try { SudokuHeuristics.ApplyHiddenPairs(board); }
            catch { throw new UnsolvableBoardException(); }
            flag = BackTrack(board); // trying to solve the sudoku with backtracking algorithm.

            if (!flag)
                throw new UnsolvableBoardException();
            return flag;
        }


    }
}
