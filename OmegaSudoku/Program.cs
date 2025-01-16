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
        BoardFormatter boardFormatter = new BoardFormatter();
        outputHandler.printBoard(inputHandler.GetInput());


    }
}