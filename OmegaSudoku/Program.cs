using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;
using System;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        CliInputHandler inputHandler = new CliInputHandler();
        CliOutputHandler outputHandler = new CliOutputHandler();

        SudokuBoard board = new SudokuBoard(9, "400000805030000000000700000020000060000080400000010000000603070500200000104000000");

        outputHandler.PrintBoard(board);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        bool flag = SudokuSolver.Solve(board);

        stopwatch.Stop();


        if (!flag)
        {
            Console.WriteLine("\nBoard is unsolvable");
            Console.WriteLine($"Time it took: {stopwatch.ElapsedMilliseconds} ms");
        }

        else
        {
            outputHandler.PrintBoard(board);
            Console.WriteLine($"Solving time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
