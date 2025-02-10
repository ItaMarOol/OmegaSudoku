using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;
using System;
using System.Diagnostics;
using System.Drawing;

namespace OmegaSudoku.Logic
{
    public class SudokuController
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;
        private readonly Stopwatch _stopwatch;

        public SudokuController(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Runs the main game loop, where the user can select options to input a Sudoku board from the console, a file, or see other options.
        /// The loop continues until the user chooses to exit.
        /// </summary>
        public void Run()
        {
            ((CliOutputHandler)_outputHandler).ShowWelcomeMessage();
            while (true)
            {
                // disabling ctrl + _ from exiting the program
                static void ExitKeyHandler(object? sender, ConsoleCancelEventArgs args) => args.Cancel = true;
                Console.CancelKeyPress += new ConsoleCancelEventHandler(ExitKeyHandler);

                ((CliOutputHandler)_outputHandler).ShowMenu();
                string command = _inputHandler.GetInput().ToLower();

                switch (command)
                {
                    case "1": // enters sudoku from console
                        EnterBoardFromConsole();
                        _stopwatch.Reset();
                        break;

                    case "2": // loads sudoku from file
                        EnterBoardFromFile();
                        _stopwatch.Reset();
                        break;

                    case "3": // shows chars dictionary
                        ((CliOutputHandler)_outputHandler).ShowCharsDictionary();
                        break;

                    case "4": // exits
                        _outputHandler.PrintMessage("\nExiting from program.");
                        return;

                    default: // invalid command has been entered
                        _outputHandler.PrintError("Invalid selection. Please choose a valid option.");
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the input of the Sudoku board from the console. It allows the user to enter the board size and etner the board as a string.
        /// After validation, it will solve the board and display the solution.
        /// </summary>
        private void EnterBoardFromConsole()
        {
            try
            {
                int boardSize = ((CliInputHandler)_inputHandler).GetBoardSize(); // getting board size from the user through console
                SudokuConstants.BoardSize = boardSize;
                SudokuConstants.MaxCellValue = boardSize; 
                string boardInput = ((CliInputHandler)_inputHandler).GetBoardInput(boardSize); // getting the board from the user through console
                ValidateAndSolveBoard(boardSize, boardInput, false); // validates, solves the board and displaying the solved board on console
            }
            catch (Exception ex)
            {
                ProcessSolvingError(ex); // stops the timer, prints the error and prints the program run time
            }
        }

        /// <summary>
        /// Handles the input of the Sudoku board from a file. The user provides the file path, and the program tries to load the board from the file.
        /// After validation, it will solve the board and display the solution.
        /// </summary>
        private void EnterBoardFromFile()
        {
            try
            {
                _outputHandler.PrintMessage("Please enter the file path to load the Sudoku board: ");
                string filePath = _inputHandler.GetInput().Trim(); // getting the sudoku input file path from user

                FileInputHandler fileInputHandler = new FileInputHandler(filePath);

                string boardInput = fileInputHandler.GetInput(); // getting board input from the provided file
                int boardSize = (int)Math.Sqrt(boardInput.Length);

                if (!InputValidator.IsBoardSizeValid(boardSize)) // validates board size
                {
                    throw new InvalidBoardSizeException(boardSize);
                }
                SudokuConstants.BoardSize = boardSize;
                SudokuConstants.MaxCellValue = boardSize;
                ValidateAndSolveBoard(boardSize, boardInput, true, filePath);  // validates, solves the board and displaying the solved board on console
            }
            catch (Exception ex)
            {
                ProcessSolvingError(ex); // stops the timer, prints the error and prints the program run time
            }
        }

        /// <summary>
        /// Validates the board input (from console or file), solves the Sudoku board, and displays the solution.
        /// If the input comes from a file, the solution is saved to the input file as a string.
        /// </summary>
        /// <param name="boardSize">The size of the Sudoku board.</param>
        /// <param name="boardInput">The input string representing the initial Sudoku board.</param>
        /// <param name="isFromFile">Indicates whether the board was loaded from a file (used for saving output to file).</param>
        /// <param name="filePath">The file path (optional) to save the solution if the input is from a file.</param>
        private void ValidateAndSolveBoard(int boardSize, string boardInput, bool isFromFile, string? filePath = null)
        {
            InputValidator.IsBasicInputValid(boardInput); // validates the board input
            SudokuBoard board = new SudokuBoard(boardSize, boardInput);

            DisplayInitialBoard(board); // displays the initial board

            _stopwatch.Start();
            SudokuSolver.Solve(board);  // solving attempt
            _stopwatch.Stop();

            DisplaySolvedBoard(board); // displays the solved board

            if (isFromFile) // checks if the board input is from a file. if so - prints the solved board as a string to the same file. 
            {
                FileOutputHandler fileOutputHandler = new FileOutputHandler(filePath);
                fileOutputHandler.PrintBoardAsString(board);
            }
        }

        /// <summary>
        /// Displays an initial sudoku board.
        /// </summary>
        /// <param name="board">Solved sudoku board.</param>
        private void DisplayInitialBoard(SudokuBoard board)
        {
            _outputHandler.PrintMessage("\nInitial sudoku board:");
            ((CliOutputHandler)_outputHandler).PrintBoard(board);
        }

        /// <summary>
        /// Displays a solved sudoku board and the solving time (in ms).
        /// </summary>
        /// <param name="board">Solved sudoku board.</param>
        private void DisplaySolvedBoard(SudokuBoard board)
        {
            _outputHandler.PrintMessage("\nSolved sudoku board:");
            ((CliOutputHandler)_outputHandler).PrintBoard(board);
            _outputHandler.PrintMessage($"Solving time: {_stopwatch.ElapsedMilliseconds} ms");
        }

        /// <summary>
        /// Processes an error while trying to solve a board. It will stop the timer, print the error and print the program run time.
        /// </summary>
        /// <param name="ex">Thrown exception while solving.</param>
        private void ProcessSolvingError(Exception ex)
        {
            _stopwatch.Stop();
            _outputHandler.PrintError(ex.Message);
            Console.ForegroundColor = ConsoleColor.Yellow;
            _outputHandler.PrintMessage($"Program ran for: {_stopwatch.ElapsedMilliseconds} ms");
            Console.ResetColor();
        }

    }
}
