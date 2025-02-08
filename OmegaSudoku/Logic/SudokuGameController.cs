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
            ((CliOutputHandler)_outputHandler).ShowWelcomeMessage();
            while (true)
            {
                // todo: add ctrl + (x) "treatments"
                ((CliOutputHandler)_outputHandler).ShowMenu();
                string command = _inputHandler.GetInput().ToLower();

                switch (command)
                {
                    case "1":
                        EnterBoardFromConsole();
                        _stopwatch.Reset();
                        break;

                    case "2":
                        EnterBoardFromFile();
                        _stopwatch.Reset();
                        break;

                    case "3":
                        ((CliOutputHandler)_outputHandler).ShowCharsDictionary();
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
                int boardSize = ((CliInputHandler)_inputHandler).GetBoardSize();
                Constants.BoardSize = boardSize;
                Constants.MaxCellValue = boardSize; 
                string boardInput = ((CliInputHandler)_inputHandler).GetBoardInput(boardSize);
                ValidateAndSolveBoard(boardSize, boardInput, false);
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

                FileInputHandler fileInputHandler = new FileInputHandler(filePath);

                string boardInput = fileInputHandler.GetInput();
                int boardSize = (int)Math.Sqrt(boardInput.Length);
                if (!InputValidator.IsBoardSizeValid(boardSize))
                {
                    throw new InvalidBoardSizeException(boardSize);
                }
                Constants.BoardSize = boardSize;
                Constants.MaxCellValue = boardSize;
                ValidateAndSolveBoard(boardSize, boardInput, true, filePath);
            }
            catch (Exception e)
            {
                _outputHandler.PrintError(e.Message);
            }
        }

        private void ValidateAndSolveBoard(int boardSize, string boardInput, bool isFromFile, string? filePath = null)
        {
            InputValidator.IsBasicInputValid(boardInput);
            SudokuBoard board = new SudokuBoard(boardSize, boardInput);
            _outputHandler.PrintMessage("\nInitial sudoku board:");
            ((CliOutputHandler)_outputHandler).PrintBoard(board);

            _stopwatch.Start();
            SudokuSolver.Solve(board); 
            _stopwatch.Stop();

            _outputHandler.PrintMessage("\nSolved sudoku board:");
            ((CliOutputHandler)_outputHandler).PrintBoard(board); 
            _outputHandler.PrintMessage($"Solving time: {_stopwatch.ElapsedMilliseconds} ms");
            if (isFromFile)
            {
                FileOutputHandler fileOutputHandler = new FileOutputHandler(filePath);
                fileOutputHandler.PrintBoardAsString(board);
            }
        }

    }
}
