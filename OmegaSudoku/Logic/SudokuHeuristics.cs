using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic.Heuristics;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic
{
    public static class SudokuHeuristics
    {
        /// <summary>
        /// Applies sudoku heuristics to a given board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        public static bool ApplySudokuHeuristics(SudokuBoard board)
        {
            bool changeFlag = false;
            NakedPairsHeuristic nakedPairsHeuristic = new NakedPairsHeuristic();
            HiddenSinglesHeuristic hiddenSinglesHeuristic = new HiddenSinglesHeuristic();
            HiddenPairsHeuristic hiddenPairsHeuristic = new HiddenPairsHeuristic();

            changeFlag |= nakedPairsHeuristic.ApplyHeuristic(board);  // updates all the board cells possibilties by sudoku 'naked pair' heuristic.
            board.UpdateAllCellsPossibilities(); // updates all the board cells possibilties by sudoku rules.
            changeFlag |= hiddenSinglesHeuristic.ApplyHeuristic(board); // updates all the board cells possibilties by sudoku 'hidden singles' heuristic.
            changeFlag |= hiddenPairsHeuristic.ApplyHeuristic(board); // updates all the board cells possibilties by sudoku 'hidden pairs' heuristic.
            
            return changeFlag;
        }
    }
}
