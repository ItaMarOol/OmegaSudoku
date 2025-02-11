using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Input
{
    /// <summary>
    /// This interface represents an input handler. It defindes methods for handling user input, such as getting input for a Sudoku board.
    /// </summary>

    public interface IInputHandler
    {
        string GetInput(); // gets user input string
    }
}
