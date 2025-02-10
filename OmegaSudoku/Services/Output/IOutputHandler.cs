using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Output
{
    public interface IOutputHandler
    {
        void PrintBoardAsString(SudokuBoard board); // prints a given board as a string
        void PrintMessage(string message);  // prints a general message
        void PrintError (string error); // prints an error

    }
}
