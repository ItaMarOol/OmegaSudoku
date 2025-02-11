using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudokuTests.SolvingTests.Sudoku16x16
{
    [TestClass]
    public class RegularBoardsTests
    {

        // Easy 16x16 boards tests

        [TestMethod]
        public void EasySudokuTest1()
        {
            // Arrange
            string initialBoardString = "500004>08000000;00;00300501?<0=080000:50>00090206002000000=900002010000:605400;?0540<0000930000><000003;0100=0000700208=000:000000>070053?01040=02090<000=00000000000@0?0;60090001:;>6900@0<?700>=960700?8;00000000000000<0>5=?00<0?004090:08;>0000:0000@5037006\r\n";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void EasySudokuTest2()
        {

            // Arrange
            string initialBoardString = "00;7030008140@0010:3008=0207004;0<020090?0000008?000:>00;060001000796?000<0:40=0=005<0000036000006@000:0105>00000000;=390000<0@090>0050004@0086:0?4000600782>;0002000800300170?05300?900>00<0100000<>00000030:;00030=000002090<00>0?060@00000000002;000<00?00070\r\n";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void EasySudokuTest3()
        {

            // Arrange
            string initialBoardString = "<001000=?00;3>070064>;057100080=0@0000010000<000=0>0000680:00000800000:00>00?00;00<0?0>063050080045>0<000?0096=0?=0200009000>70:0?0700200<000=0000=;0000:000000?0000500000000<10000000002;76:00501009067@0=00;0006003:0007;05008004<0850>06903000:702?<>00500009\r\n";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void EasySudokuTest4()
        {

            // Arrange
            string initialBoardString = "004051?=800:0000000000:00002000000>00;23@050?<:0@0<549000>00;00800000=00:@6;0100000<3005008004=>000;00020=005@0600000>000007:0000006020@>10000090904007000008:3@1<03000000000600>00@6000;2:40000000>0?0:06000;07=00?00>;1009<000019:030045006=006000<000082?0340\r\n";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }



        // Medium 16x16 boards tests

        [TestMethod]
        public void MediumSudokuTest1()
        {
            // Arrange
            string initialBoardString = "030000;062<000@0001;:620@80>700400020@8900035=109>0030000;0=0000=000620080003?05:00008000030000<0@000450;<0000090?4500000900>@0004000<:10>600870000000060008040=0200070050?010<0@873050?0:000200;<060000300000=020>070?0000500:60000501000;020000001006;00098030";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void MediumSudokuTest2()
        {

            // Arrange
            string initialBoardString = ">000?006;00=20000@000090700:0300300;080000096?0060000=;3000000<90>40000000300000820004<007600005;300@0020>40760:700:05=080009000076?00500820<0000900000:0=03080000209>40000000;30;002@00<0>0:700@002<0000?0000=0500000@040900:00000600350182000>0090:700300;0180";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void MediumSudokuTest3()
        {

            // Arrange
            string initialBoardString = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void MediumSudokuTest4()
        {

            // Arrange
            string initialBoardString = "00@00050=0000080030<7000:9040000000:>60<5001007050060=@0?<08001:02=;00?30007000<0@400<00020?070;<00040080>000=00057?0200960:00008060@0:907004000090@0001008<0:0240>0002000:000360:0200801@00000>9<10004080;000?0060500<00?00:0=9@72>0000000003000?00001700000000\r\n";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }


        // Hard 16x16 boards tests

        [TestMethod]
        public void HardSudokuTest1()
        {
            // Arrange
            string initialBoardString = "0000:=000000000?70050;01:00@90<8900800700004600=60:=080000070002=00030890>?500012;01@:000008007>00001000@0000900000<>?0740000000006@900000>0100002;0600=800<00500070002000000000<0900>?5;4020=0@0020=0@0<0907000>?500400=6000<803<0000002001@:0000=68000?0004020";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

        [TestMethod]
        public void HardSudokuTest2()
        {

            // Arrange
            string initialBoardString = "00;:00007050@000002008;000300:09000001:39008000060004@050>:0;00<00800?0001005000?000000400;<00030:00008@0000400>020000003=95000:4@00?0=0000092570000@4100000<000907>0006000:=00000000020000006>00608<0?0052000700000030:@0000900000=02>000?00300>00009000800000=";
            SudokuBoard board = new SudokuBoard(16, initialBoardString);

            // Act
            bool isSolvedAndValid = SudokuSolver.Solve(board) && BoardValidator.IsBoardValid(board);

            // Assert
            Assert.IsTrue(isSolvedAndValid);
        }

    }
}
