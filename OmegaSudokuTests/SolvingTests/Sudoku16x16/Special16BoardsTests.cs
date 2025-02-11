using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;

namespace OmegaSudokuTests.SolvingTests.Sudoku16x16
{

    /// <summary>
    /// Tests for solving special 16x16 sudoku boards (like empty boards and unsolvable boards).
    /// </summary>
    [TestClass]
    public class Special16BoardsTests
    {
        // Empty 16x16 board

        [TestMethod]
        public void EmptySudokuTest()
        {
            // Arrange
            string initialBoardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

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
            string initialBoardString = "00003000000900<>50>072<0000@060000?0>=000004:000=<;90?0013000700000:0002>4010000900041=0000?3<:5?0040003706008;9;000<00000002?00<00007;0=60>?0008?=09:>50@70;00<>:00280000;0@0700090001?800050040800?00>00=009060;00090000000=00:960000020?7435000000000:0000001";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act + Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board));
        }

        [TestMethod]
        public void UnsolveableSudokuTest2()
        {

            // Arrange
            string initialBoardString = "00003000000900<>50>072<0000@060000?0>=000004:000=<;90?0013000700000:0002>4010000900041=0000?3<:5?0040003706008;9;000<00000002?00<00007;0=60>?0008?=09:>50@70;00<>:00280000;0@0700090001?800050040800?00>00=009060;00090000000=00:960000020?7435000000000:0000002";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act + Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board));
        }

        [TestMethod]
        public void UnsolveableSudokuTest3()
        {

            // Arrange
            string initialBoardString = "00003000000900<>50>072<0000@060000?0>=000004:000=<;90?0013000700000:0002>4010000900041=0000?3<:5?0040003706008;9;000<00000002?00<00007;0=60>?0008?=09:>50@70;00<>:00280000;0@0700090001?800050040800?00>00=009060;00090000000=00:960000020?7435000000000:0000008";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act + Assert
            Assert.ThrowsException<UnsolvableBoardException>(() => SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board));
        }

    }
}
