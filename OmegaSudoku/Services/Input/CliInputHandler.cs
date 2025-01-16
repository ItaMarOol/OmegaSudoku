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
            Console.WriteLine("Enter 9x9 sudoku board");
            String input = Console.ReadLine();

            return input;
        }
    }
}
