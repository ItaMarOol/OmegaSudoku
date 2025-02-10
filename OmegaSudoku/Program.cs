using OmegaSudoku.Logic;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;
using System;
using System.Diagnostics;

public class Program
{

    /// <summary>
    /// The main entry point for the application. It sets up the necessary input and output handlers,
    /// initializes the Sudoku controller, and starts the game by running the controller's logic.
    /// </summary>
    static void Main(string[] args)
    {
        // Initialize the input handler for CLI-based input
        IInputHandler inputHandler = new CliInputHandler();

        // Initialize the output handler for CLI-based output
        IOutputHandler outputHandler = new CliOutputHandler();

        // Create and run the Sudoku controller
        SudokuController controller = new SudokuController(inputHandler, outputHandler);
        controller.Run();

    }
}
