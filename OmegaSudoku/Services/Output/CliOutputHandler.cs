using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using OmegaSudoku.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Output
{
    public class CliOutputHandler : IOutputHandler
    {
        /// <summary>
        /// An empty constructor to initialize a CliOutputHandler object.
        /// </summary>
        public CliOutputHandler() { }

        /// <summary>
        /// Prints the Sudoku board as a formatted string.
        /// </summary>
        /// <param name="board">The Sudoku board to be printed.</param>
        public void PrintBoardAsString(SudokuBoard board)
        {
            string boardString =board.ConvertBoardToString();
            Console.WriteLine(boardString);
        }

        /// <summary>
        /// Prints a general message in yellow color.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        public void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints an error message in red color.
        /// </summary>
        /// <param name="error">The error message to be printed.</param>
        public void PrintError(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nError: {error}");
            Console.ResetColor();
        }

        /// <summary>
        /// Prints the Sudoku board in a formatted layout.
        /// </summary>
        /// <param name="board">The Sudoku board to be printed.</param>
        public void PrintBoard(SudokuBoard board)
        {
            string boardString = board.ConvertBoardToString();
            PrintBoard(boardString);
        }

        /// <summary>
        /// Prints the Sudoku board as a string with proper formatting.
        /// </summary>
        /// <param name="board">The Sudoku board to be printed as a string.</param>
        public void PrintBoard(string board)
        {
            board = board.Replace('0', '.');
            int boardSize = (int)Math.Sqrt(board.Length);
            int blockLength = (int)Math.Sqrt(boardSize);
            int lineLength = boardSize * 2 + ((blockLength - 1) * 2) +3;

            Console.WriteLine();
            Console.WriteLine("-".PadLeft(lineLength, '-'));
            for (int rowIndex = 0; rowIndex < boardSize; rowIndex++)
            {
                string row = board.Substring(rowIndex * boardSize, boardSize);
                Console.Write("| ");
                for (int j = 0; j < boardSize; j++)
                {
                    if (j % blockLength == 0 && j != 0) // right side of a block
                    {
                        Console.Write("| "); 
                    }
                    Console.Write($"{row[j]} ");
                }
                Console.Write("| ");

                Console.WriteLine();
                if ((rowIndex + 1) % blockLength == 0 && rowIndex != boardSize-1) // bottom side of a row of blocks
                {
                    Console.WriteLine("-".PadLeft(lineLength, '-')); 
                }
            }
            Console.WriteLine("-".PadLeft(lineLength, '-'));

        }


        /// <summary>
        /// Displays the main menu with options for the user to choose from.
        /// </summary>
        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Enter Sudoku board manually.");
            Console.WriteLine("2. Enter Sudoku board from file.");
            Console.WriteLine("3. Open sudoku chars dictionary.");
            Console.WriteLine("4. Exit the program.");
            Console.Write("\nPlease choose an option: ");
        }

        /// <summary>
        /// Requests the user to input a Sudoku board of the given size.
        /// </summary>
        /// <param name="boardSize">The size of the board.</param>
        public void RequestBoardInput(int boardSize)
        {
            Console.WriteLine($"Please enter {boardSize}x{boardSize} sudoku board as one string (empty cells represented by '0'): ");
        }

        /// <summary>
        /// Requests the user to input a valid Sudoku board size.
        /// </summary>
        public void RequestBoardSize()
        {
            Console.Write($"Please enter the sudoku board size (has to be a value between {Constants.MinBoardSize}-{Constants.MaxBoardSize} that it's square root is an integer): ");
        }

        /// <summary>
        /// Displays a welcome message when the program starts.
        /// </summary>
        public void ShowWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Itamar's Omega Sudoku!");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays the Sudoku character dictionary mapping characters to their digit values.
        /// </summary>
        public void ShowCharsDictionary()
        {
            Console.WriteLine("\nSudoku chars dictionary:");

            for (int digit = 0; digit <= Constants.MaxBoardSize; digit++)
            {
                char character = (char)(digit + Constants.AsciiDigitDiff);
                Console.WriteLine($"'{character}' = {digit}");
            }
        }




    }
}
