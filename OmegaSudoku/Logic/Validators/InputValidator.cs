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

        /// <summary>
        /// Validates if the input string contains only valid Sudoku values (0-9).
        /// </summary>
        /// <param name="input">The input string representing the Sudoku board.</param>
        /// <returns>True if the input string contains valid values. else - false.</returns>
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

        /// <summary>
        /// Validates if the length of the input string matches the required board size.
        /// </summary>
        /// <param name="input">The input string representing the Sudoku board.</param>
        /// <returns>True if the input length is valid. else - false.</returns>
        private static bool IsInputLengthValid(string input)
        {
            if (input.Length != Constants.BoardSize * Constants.BoardSize)
                return false;
            return true;
        }

        /// <summary>
        /// Validates if the board size is within valid limits and is a perfect square.
        /// </summary>
        /// <param name="boardSize">The size of the Sudoku board.</param>
        /// <returns>True if the board size is valid. else - false.</returns>
        public static bool IsBoardSizeValid(int boardSize)
        {
            if (Math.Sqrt(boardSize) % 1 == 0 && boardSize >= Constants.MinBoardSize && boardSize <= Constants.MaxBoardSize)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Validates if the input string is a valid Sudoku board input. Checking for valid values, length, and duplicate values.
        /// </summary>
        /// <param name="input">The input string representing the Sudoku board.</param>
        /// <returns>True if the input is valid. else - throws exceptions.</returns>
        /// <exception cref="InvalidCellValuesException">Thrown when the input contains invalid characters.</exception>
        /// <exception cref="InvalidBoardSizeException">Thrown when the input length is incorrect for the board size.</exception>
        /// <exception cref="DuplicateValueException">Thrown when a duplicate value is found in the input.</exception>
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

        /// <summary>
        /// returns a list of invalid characters from the input string.
        /// </summary>
        /// <param name="input">The input string representing the Sudoku board.</param>
        /// <returns>A list of invalid characters found in the input.</returns>
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

        /// <summary>
        /// Checks for duplicate values in the input string.
        /// </summary>
        /// <param name="input">The input string representing the Sudoku board.</param>
        /// <returns>The duplicated value if found. else - (-1).</returns>
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
