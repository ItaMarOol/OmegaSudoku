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
        void PrintBoardAsString(SudokuBoard board);
        void PrintMessage(string message);  
        void PrintError (string error);

    }
}
