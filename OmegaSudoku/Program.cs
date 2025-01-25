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

        string input = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
        input = input.Replace('.', '0');
        SudokuBoard board = new SudokuBoard(9, input);

        outputHandler.PrintBoard(board);
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        bool flag = false;

        try
        {
            flag = SudokuSolver.Solve(board);
        }
        catch (Exception ex) 
        {
            outputHandler.PrintError(ex.Message);
        }

        stopwatch.Stop();


        if (!flag)
        {
            Console.WriteLine($"Time it took: {stopwatch.ElapsedMilliseconds} ms");
        }

        else
        {
            outputHandler.PrintBoard(board);
            Console.WriteLine($"Solving time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
