using OmegaSudoku.Exceptions;
using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Input;
using OmegaSudoku.Services.Output;
using OmegaSudoku.Utilities;
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

        public void Start()
        {
            _outputHandler.ShowWelcomeMessage();
            while (true)
            {
                // todo: add ctrl + (x) "treatments"
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

                    case "3":
                        _outputHandler.ShowCharsDictionary();
                        break;

                    case "4":
                        _outputHandler.PrintMessage("\nExiting from program.");
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
                _outputHandler.PrintMessage("Please enter the file path to load the Sudoku board: ");
                string filePath = _inputHandler.GetInput().Trim();

                if (!File.Exists(filePath))
                {
                    _outputHandler.PrintError("File not found. Please check the path and try again.");
                    return;
                }

                string boardInput = File.ReadAllText(filePath).Trim();
                int boardSize = (int)Math.Sqrt(boardInput.Length);
                if (!InputValidator.IsBoardSizeValid(boardSize))
                {
                    _outputHandler.PrintError("Invalid board size. The size must be valid (perfect square) and within the valid range.");
                    return;
                }
                Constants.BoardSize = boardSize;
                Constants.MaxCellValue = boardSize;
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

            _stopwatch.Start();
            SudokuSolver.Solve(board); 
            _stopwatch.Stop();
            _outputHandler.PrintMessage("\nSolved sudoku board:");
            _outputHandler.PrintBoard(board); // todo: if the given sudoku is from a file, print the solved *string* to the file
            _outputHandler.PrintMessage($"Solving time: {_stopwatch.ElapsedMilliseconds} ms");
            _stopwatch.Reset();
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
