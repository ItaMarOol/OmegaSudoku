using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;

internal class Program
{
    private static void Main(string[] args)
    {
        // temporary main for testing some classes

        CliInputHandler inputHandler = new CliInputHandler();
        CliOutputHandler outputHandler = new CliOutputHandler();
        outputHandler.printBoard(inputHandler.GetInput());
    }
}