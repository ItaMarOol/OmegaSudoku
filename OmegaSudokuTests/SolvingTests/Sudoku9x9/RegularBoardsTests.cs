using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudokuTests.SolvingTests.Sudoku9x9
{
    [TestClass]
    public class RegularBoardsTests
    {

        // Easy 9x9 boards tests
        
        [TestMethod]
        public void EasySudokuTest1()
        {
            // Arrange
            string initialBoardString = "165293004000001632023060090009175000500900018002030049098000006000000950000429381";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void EasySudokuTest2()
        {

            // Arrange
            string initialBoardString = "803700000026000004097100203705000908901070040038401567170950800680210435352846000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void EasySudokuTest3()
        {

            // Arrange
            string initialBoardString = "005000060000006302040081597012038754000200810087014000120007680000092030954860200";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void EasySudokuTest4()
        {

            // Arrange
            string initialBoardString = "405001068073628500009003070240790030006102005950000021507064213080217050612300007";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }
        


        // Medium 9x9 boards tests

        [TestMethod]
        public void MediumSudokuTest1()
        {
            // Arrange
            string initialBoardString = "206500008000041300900000000000050730800600000000000000070000400000209000010000000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void MediumSudokuTest2()
        {

            // Arrange
            string initialBoardString = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void MediumSudokuTest3()
        {

            // Arrange
            string initialBoardString = "000000020004300096500002100100000700000000432000050010060000000005970008090680000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void MediumSudokuTest4()
        {

            // Arrange
            string initialBoardString = "003080000000350000070000600005000000020009407000000001000000080060000030100004000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }


        // Hard 9x9 boards tests

        [TestMethod]
        public void HardSudokuTest1()
        {
            // Arrange
            string initialBoardString = "400030000000600800000000001000050090080000600070200000000102700503000040900000000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void HardSudokuTest2()
        {

            // Arrange
            string initialBoardString = "708000300000601000500000000040000026300080000000100090090200004000070500000000000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void HardSudokuTest3()
        {

            // Arrange
            string initialBoardString = "020004800054000600900000001000507068000030100080002000300000000070908052000070000";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void HardSudokuTest4()
        {

            // Arrange
            string initialBoardString = "100000000006000003080000401000678504000900000000500760002000000590814000000300900";
            SudokuBoard board = new SudokuBoard(9, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

    }
}
