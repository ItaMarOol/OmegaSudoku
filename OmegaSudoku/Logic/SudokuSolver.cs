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
            BoardValidator boardValidator = new BoardValidator(board);
            BoardCell lowestCell = board.GetLowestPossibilitesCell(); // getting the cell with the lowest possibilities
            if (board.HasZeroCountCell() || !boardValidator.IsBoardValid())
                return false;

            if (lowestCell == null) // check if the board has been solved
                return true;

            HashSet<int> possibilities = new HashSet<int>(lowestCell.GetPossibilities()); // getting the lowest cell possibilities
            Dictionary<BoardCell, HashSet<int>> savedState = board.SaveBoardState(); // saving the board state

            foreach (int possibleValue in possibilities)
            {
                int lowestRow = lowestCell.Row;
                int lowestCol = lowestCell.Col;
                if (board.CanValueBePlaced(lowestRow, lowestCol, possibleValue))
                {
                    savedState = board.SaveBoardState(); 
                    board.SetCellValue(lowestRow, lowestCol, possibleValue);

                    board.UpdateAllCellsPossibilities(); // updates all the board cells possibilties by sudoku rules.
                    board.ApplyNakedPairs(); // updates all the board cells possibilties by sudoku 'naked pair' heuristic.

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
            board.ApplyNakedPairs(); // updates all the board cells possibilties by sudoku 'naked pair' heuristic.
            flag = BackTrack(board); // trying to solve the sudoku with backtracking algorithm.
           
            return flag;
        }


    }
}
