using OmegaSudoku.Services.Output;
using OmegaSudoku.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Validators
{
    public class InputValidator
    {
        private string _input;

        public InputValidator(string input)
        {
            _input = input;
        }
        public bool IsInputLengthValid()
        {
            if (_input.Length > Constants.BoardSize * Constants.BoardSize)
                return false;
            return true;
        }

        public bool IsValidSudokuValues()
        {
            int index;
            for (index = 0; index < _input.Length; index++)
            {
                int value = _input[index] - '0'; // converting char to int (by ascii values)
                if (value < Constants.MinCellValue || value > Constants.MaxCellValue)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsBasicInputValid()
        {
            CliOutputHandler outputHandler = new CliOutputHandler(); // temporary cli output handler
            if (!IsInputLengthValid())
            {
                outputHandler.PrintError("Board length is invalid"); // throw new InvalidInputException
                return false;
            }
            if (!IsValidSudokuValues())
            {
                List<char> invalidChars = GetInvalidInputChars();
                outputHandler.PrintError($"Board values ({string.Join(",", invalidChars)}) are invalid "); // throw new InvalidInputException
                return false;
            }
            return true;
        }

        public List<char> GetInvalidInputChars()
        {
            int index;
            List<char> invalid = new List<char>();
            for (index = 0; index < _input.Length; index++)
            {
                int value = _input[index] - '0'; // converting char to int (by ascii values)
                if (value != 0 && value < Constants.MinCellValue || value > Constants.MaxCellValue)
                {
                    invalid.Add(_input[index]);
                }
            }
            return invalid;
        }
    }
}
