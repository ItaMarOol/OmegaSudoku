using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Heuristics
{
    public interface IHeuristic
    { 
        bool ApplyHeuristic(SudokuBoard board);
    }
}
