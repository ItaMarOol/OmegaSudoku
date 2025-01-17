using OmegaSudoku.Models;
using OmegaSudoku.Services.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Input
{
    public class CliInputHandler : IInputHandler
    {

        public string GetInput()
        {
            String input = Console.ReadLine();
            if (input == null)
                return string.Empty;
            return input;
        }


        /*public SudokuBoard BuildSudokuBoard(string input)
        {

        }*/
    }
}
