using OmegaSudoku.Exceptions;
using OmegaSudoku.Services.Output;
using OmegaSudoku.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Logic.Validators
{
    public static class InputValidator
    {
        private static bool IsValidSudokuValues(string input)
        {
            int index;
            for (index = 0; index < input.Length; index++)
            {
                int value = input[index] - Constants.AsciiDigitDiff; // converting char to int (by ascii values)
                if (value != 0 && value < Constants.MinCellValue || value > Constants.MaxCellValue)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool IsInputLengthValid(string input)
        {
            if (input.Length != Constants.BoardSize * Constants.BoardSize)
                return false;
            return true;
        }

        public static bool IsBoardSizeValid(int boardSize)
        {
            if (Math.Sqrt(boardSize) % 1 == 0 && boardSize >= Constants.MinBoardSize && boardSize <= Constants.MaxBoardSize)
            {
                return true;
            }
            return false;
        }

        public static bool IsBasicInputValid(string input)
        {
            if (!IsValidSudokuValues(input))
            {
                List<char> invalidChars = GetInvalidInputChars(input);
                throw new InvalidCellValuesException(invalidChars);
            }
            if (!IsInputLengthValid(input))
            {
                throw new InvalidBoardSizeException(input.Length);
            }
            int checkedValue = GetDuplicatedInputValue(input);
            if (checkedValue != -1)
                throw new DuplicateValueException(checkedValue);
            return true;
        }

        private static List<char> GetInvalidInputChars(string input)
        {
            int index;
            List<char> invalid = new List<char>();
            for (index = 0; index < input.Length; index++)
            {
                int value = input[index] - Constants.AsciiDigitDiff; // converting char to int (by ascii values)
                if (value != 0 && value < Constants.MinCellValue || value > Constants.MaxCellValue)
                {
                    invalid.Add(input[index]);
                }
            }
            return invalid;
        }

        private static int GetDuplicatedInputValue(string input)
        {
            int row, col;
            int boardSize = (int)Math.Sqrt(input.Length);
            List<int> seenValues = new List<int>();
            for (row = 0; row < boardSize; row++)
            {
                for (col = 0; col < boardSize; col++) 
                {
                    int checkedValue = input[row * boardSize + col] - Constants.AsciiDigitDiff;
                    if (checkedValue != 0)
                    {
                        if (!seenValues.Contains(checkedValue))
                            seenValues.Add(checkedValue);
                        else
                            return checkedValue;
                    }

                }
                seenValues.Clear();
            }
            return -1;
        }
    }
}
