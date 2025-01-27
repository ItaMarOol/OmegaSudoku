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
        void PrintBoard(SudokuBoard board);
        void PrintBoard(string board);
        void PrintMessage(string message);  
        void PrintError (string error);
        void ShowMenu();
        void ShowWelcomeMessage();
        void ShowCharsDictionary();
    }
}
