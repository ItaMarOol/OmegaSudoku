using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Heuristics
{
    /// <summary>
    /// This interface represents a sudoku heuristic that can be applied to a sudoku board.
    /// </summary>
    public interface IHeuristic
    {

        /// <summary>
        /// Applies a sudoku heuristic to a given board.
        /// </summary>
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        bool ApplyHeuristic(SudokuBoard board);
    }
}
