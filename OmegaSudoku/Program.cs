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

        string input = "4...3.......6..8..........1....5..9..8....6...7.2........1.27..5.3....4.9........\r\n";
        input = input.Replace('.', '0');
        SudokuBoard board = new SudokuBoard(9, input);

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
