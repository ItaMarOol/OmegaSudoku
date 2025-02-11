using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmegaSudoku;

namespace OmegaSudokuTests.ValidatorsTests
{

    /// <summary>
    /// Tests for validating user input for sudoku boards using InputValidator.
    /// </summary>
    [TestClass]
    public class InputValidatorTests
    {

        [TestMethod]
        public void EmptyInputValidatonTest()
        {
            // Arrange
            SudokuConstants.BoardSize = 9;
            string initialBoardString = "";

            // Act + Assert
            Assert.ThrowsException<InvalidBoardSizeException>(() => InputValidator.IsBasicInputValid(initialBoardString));
        }

        [TestMethod]
        public void TooShortInputValidatonTest()
        {

            // Arrange
            SudokuConstants.BoardSize = 9;
            string initialBoardString = "000000068";

            // Act + Assert
            Assert.ThrowsException<InvalidBoardSizeException>(() => InputValidator.IsBasicInputValid(initialBoardString));

        }

        [TestMethod]
        public void TooLongInputValidatonTest()
        {

            // Arrange
            SudokuConstants.BoardSize = 4;
            string initialBoardString = "00000000000045000000000000000000";

            // Act + Assert
            Assert.ThrowsException<InvalidBoardSizeException>(() => InputValidator.IsBasicInputValid(initialBoardString));
        }

        [TestMethod]
        public void InputWithInvalidCharactersValidatonTest()
        {

            // Arrange
            SudokuConstants.BoardSize = 4;
            SudokuConstants.MaxCellValue = 4;
            string initialBoardString = "082305kjpg;;.125";

            // Act + Assert
            Assert.ThrowsException<InvalidCellValuesException>(() => InputValidator.IsBasicInputValid(initialBoardString));

            // returns the constants to their defult
            SudokuConstants.BoardSize = 9;
            SudokuConstants.MaxCellValue = 9;
        }

        [TestMethod]
        public void InputWithBiggerValuesThanBoardSizeValidatonTest()
        {

            // Arrange
            SudokuConstants.BoardSize = 4;
            SudokuConstants.MaxCellValue = 4;
            string initialBoardString = "0000143000008000";

            // Act + Assert
            Assert.ThrowsException<InvalidCellValuesException>(() => InputValidator.IsBasicInputValid(initialBoardString));

            // returns the constants to their defult
            SudokuConstants.BoardSize = 9;
            SudokuConstants.MaxCellValue = 9;
        }

        [TestMethod]
        public void InputWithDuplicatedValuesValidatonTest()
        {

            // Arrange
            SudokuConstants.BoardSize = 4;
            SudokuConstants.MaxCellValue = 4;
            string initialBoardString = "1223000000000000";

            // Act + Assert
            Assert.ThrowsException<DuplicateValueException>(() => InputValidator.IsBasicInputValid(initialBoardString));

            // returns the constants to their defult
            SudokuConstants.BoardSize = 9;
            SudokuConstants.MaxCellValue = 9;
        }
    }
}
