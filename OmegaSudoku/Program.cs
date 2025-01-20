using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;

public class Program
{
    public static void Main(string[] args)
    {
        // temporary main for testing some classes

        CliInputHandler inputHandler = new CliInputHandler();
        CliOutputHandler outputHandler = new CliOutputHandler();

        SudokuBoard board = new SudokuBoard(9, "100000027000304015500170683430962001900007256006810000040600030012043500058001000");
        outputHandler.PrintBoard(board);
        SudokuSolver sudokuSolver = new SudokuSolver(board);
        bool flag = sudokuSolver.Solve();
        board = sudokuSolver.GetSudokuBoard();
        outputHandler.PrintBoard(board);




    }
}