using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Input
{
    public interface IInputHandler
    {
        string GetInput();
       // SudokuBoard BuildSudokuBoard(string input);


    }
}
