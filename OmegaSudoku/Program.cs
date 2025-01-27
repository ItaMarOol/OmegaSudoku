using OmegaSudoku.Logic;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;
using System;
using System.Diagnostics;

public class Program
{
    static void Main(string[] args)
    {
        IInputHandler inputHandler = new CliInputHandler();
        IOutputHandler outputHandler = new CliOutputHandler();

        SudokuController controller = new SudokuController(inputHandler, outputHandler);
         controller.Start();
/*        SudokuBoard board = new SudokuBoard(9, "000030000060000400007050800000406000000900000050010300400000020000300000000000000");
        SudokuSolver.Solve(board);
        CliOutputHandler cli = new CliOutputHandler();
        cli.PrintBoard(board);*/
    }
}
