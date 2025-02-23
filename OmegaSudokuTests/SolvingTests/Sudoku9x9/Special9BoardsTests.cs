﻿using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;

namespace OmegaSudokuTests.SolvingTests.Sudoku9x9
{

    /// <summary>
    /// Tests for solving special 9x9 sudoku boards (like empty boards and unsolvable boards).
    /// </summary>
    [TestClass]
    public class Special9BoardsTests
    {
        // Empty 9x9 board

        [TestMethod]
        public void EmptySudokuTest()
        {
            // Arrange
            string initialBoardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }


        // Unsolveable 9x9 boards

        [TestMethod]
        public void UnsolveableSudokuTest1()
        {

            // Arrange
            string initialBoardString = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act + Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board));
        }

        [TestMethod]
        public void UnsolveableSudokuTest2()
        {

            // Arrange
            string initialBoardString = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act + Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board));
        }

        [TestMethod]
        public void UnsolveableSudokuTest3()
        {

            // Arrange
            string initialBoardString = "704000002000801000300000000506001002000400000000000900003700000900005000800000060";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act + Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board));
        }

    }
}
