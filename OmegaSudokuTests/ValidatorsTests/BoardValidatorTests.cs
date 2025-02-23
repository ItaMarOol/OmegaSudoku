﻿using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;

namespace OmegaSudokuTests.ValidatorsTests
{

    /// <summary>
    /// Tests for validating sudoku boards using the BoardValidator.
    /// </summary>
    [TestClass]
    public class BoardValidatorTests
    {

        [TestMethod]
        public void BoardValidationTest()
        {
            // Arrange
            bool isValid = true;
            string initialBoardString = "0033000000000000";
            SudokuBoard testBoard = new SudokuBoard(4, initialBoardString);

            // Act
            isValid = BoardValidator.IsBoardValid(testBoard);

            // Assert
            Assert.IsFalse(isValid);

        }
    }
}
