﻿using OmegaSudoku.Logic;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;

namespace OmegaSudokuTests.SolvingTests.Sudoku25x25
{

    /// <summary>
    /// Tests for solving special 25x25 sudoku boards (like empty boards and unsolvable boards).
    /// </summary>
    [TestClass]
    public class Special25BoardsTests
    {
        // Empty 25x25 board

        [TestMethod]
        public void EmptySudokuTest()
        {
            // Arrange
            string initialBoardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                + "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                + "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                + "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"
                + "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";


            SudokuBoard board = new SudokuBoard(25, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }



    }
}
