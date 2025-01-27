using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;
using OmegaSudoku.Utilities;
using System;

namespace OmegaSudoku.Logic
{
    public class SudokuController
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;

        public SudokuController(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public void Start()
        {
            while (true)
            {
                // todo: add title and maybe some coloring, add ctrl + (x) "treatments"
                ShowMenu();
                string command = _inputHandler.GetInput().ToLower();

                switch (command)
                {
                    case "1":
                        EnterBoardFromConsole();
                        break;

                    case "2":
                        EnterBoardFromFile();
                        break;

                    case "omega>sigit":
                        Console.WriteLine("\nExiting from program.");
                        return;

                    default:
                        _outputHandler.PrintError("Invalid selection. Please choose a valid option.");
                        break;
                }
            }
        }

        private void EnterBoardFromConsole()
        {
            try
            {
                int boardSize = GetBoardSize();
                Constants.BoardSize = boardSize;
                Constants.MaxCellValue = boardSize; 
                string boardInput = GetBoardInput(boardSize);
                ValidateAndSolveBoard(boardSize, boardInput);
            }
            catch (Exception e)
            {
                _outputHandler.PrintError(e.Message);
            }
        }

        private void EnterBoardFromFile()
        {
            try
            {
                Console.Write("Please enter the file path to load the Sudoku board: ");
                string filePath = _inputHandler.GetInput().Trim();

                if (!File.Exists(filePath))
                {
                    _outputHandler.PrintError("File not found. Please check the path and try again.");
                    return;
                }

                string boardInput = File.ReadAllText(filePath).Trim();
                int boardSize = (int)Math.Sqrt(boardInput.Length); // todo: add length check
                ValidateAndSolveBoard(boardSize, boardInput);
            }
            catch (Exception e)
            {
                _outputHandler.PrintError(e.Message);
            }
        }

        private void ValidateAndSolveBoard(int boardSize, string boardInput)
        {
            InputValidator.IsBasicInputValid(boardInput);
            SudokuBoard board = new SudokuBoard(boardSize, boardInput);
            _outputHandler.PrintMessage("\nInitial sudoku board:");
            _outputHandler.PrintBoard(board);

            SudokuSolver.Solve(board); 
            _outputHandler.PrintMessage("\nSolved sudoku board:");
            _outputHandler.PrintBoard(board); // todo: if the given sudoku is from a file, print the solved *string* to the file
        }


        private void ShowMenu()
        {
            _outputHandler.ShowMenu();
        }
        private int GetBoardSize()
        {
            return _inputHandler.GetBoardSize();
        }

        private string GetBoardInput(int boardSize)
        {
            return _inputHandler.GetBoardInput(boardSize);
        }
    }
}
