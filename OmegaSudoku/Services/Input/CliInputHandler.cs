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

        public bool IsInputLengthValid(string input)
        {
            if (input.Length > Constants.BoardSize * Constants.BoardSize)
                return false;
            return true;
        }

        public bool IsValidSudokuValues(string input)
        {
            int index;
            for (index = 0; index < input.Length; index++)
            {
                int value = input[index] - '0'; // converting char to int (by ascii values)
                if (value < Constants.MinCellValue || value > Constants.MaxCellValue)
                {
                    return false;
                }
            }
            return true;
        }

        public List<char> GetInvalidInputChars(string input)
        {
            int index;
            List<char> invalid = new List<char>();
            for (index = 0; index < input.Length; index++)
            {
                int value = input[index] - '0'; // converting char to int (by ascii values)
                if (value != 0 && value < Constants.MinCellValue || value > Constants.MaxCellValue)
                {
                    invalid.Add(input[index]);
                }
            }
            return invalid;
        }

        public bool IsInputValidSudoku(string input)
        {
            CliOutputHandler outputHandler = new CliOutputHandler();
            if (!IsInputLengthValid(input))
            {
                outputHandler.PrintError("Board length is invalid"); // throw new InvalidInputException
                return false;
            }
            if (!IsValidSudokuValues(input))
            {
                List<char> invalidChars = GetInvalidInputChars(input);
                outputHandler.PrintError($"Board values ({string.Join(",", invalidChars)}) are invalid "); // throw new InvalidInputException
                return false;
            }
            return true;
        }

        /*public SudokuBoard BuildSudokuBoard(string input)
        {

        }*/
    }
}
