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
        bool IsInputLengthValid(string input);
        bool IsValidSudokuValues(string input);
        List<char> GetInvalidInputChars(string input);
        bool IsInputValidSudoku(string input);
       // SudokuBoard BuildSudokuBoard(string input);


    }
}
