using OmegaSudoku.Logic.Validators;
using OmegaSudoku.Models;
using OmegaSudoku.Services.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Input
{

    /// <summary>
    /// This class represents a CLI (command-line interface) input handler. 
    /// It handles user input for the sudoku game through the CLI.
    /// It provides methods to request and read input for the sudoku game.
    /// </summary>
    public class CliInputHandler : IInputHandler
    {
        private readonly CliOutputHandler _outputHandler;


        /// <summary>
        /// Constructor to initialize a CliInputHandler object.
        /// It creates a new CliOutputHandler instance to handle output when requesting input from the user.
        /// </summary>
        public CliInputHandler()
        {
            _outputHandler = new CliOutputHandler();
        }

        /// <summary>
        /// Reads user input from the console.
        /// </summary>
        /// <returns>A string containing the user's input.</returns>
        public string GetInput()
        {
            String input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return input.Trim();
        }

        /// <summary>
        /// Requests and returns the user's input for the Sudoku board as a string.
        /// </summary>
        /// <param name="boardSize">The size of the board to be inputted by the user.</param>
        /// <returns>A string representing the user's input for the board.</returns>
        public string GetBoardInput(int boardSize)
        {
            _outputHandler.RequestBoardInput(boardSize);
            return GetInput();
        }

        /// <summary>
        /// Requests and returns the size of the Sudoku board from the user.
        /// The method validates the entered board size to ensure it's a valid number and a perfect square within the acceptable range. 
        /// If the input is invalid, it will ask the user to enter again.
        /// </summary>
        /// <returns>The size of the board entered by the user.</returns>
        public int GetBoardSize()
        {
            while (true)
            {
                _outputHandler.RequestBoardSize();
                string boardSize = GetInput();

                if (int.TryParse(boardSize, out int size))
                {
                    if (InputValidator.IsBoardSizeValid(size))
                        return size;
                    else
                    {
                        _outputHandler.PrintError("Invalid board size. The size must be valid (perfect square) and within the valid range.");
                    }
                }
                else
                {
                    _outputHandler.PrintError("Invalid input. Please enter a valid number.");
                }
            }
        }


    }
}
