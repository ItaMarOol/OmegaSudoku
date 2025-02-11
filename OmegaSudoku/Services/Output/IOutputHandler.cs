using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Output
{

    /// <summary>
    /// This interface represents an output handler. It defindes methods for output handling, such as printing the Sudoku board, messages, and errors.
    /// </summary>

    public interface IOutputHandler
    {
        void PrintBoardAsString(SudokuBoard board); // prints a given board as a string
        void PrintMessage(string message);  // prints a general message
        void PrintError (string error); // prints an error

    }
}
