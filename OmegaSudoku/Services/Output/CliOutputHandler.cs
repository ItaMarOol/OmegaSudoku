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
        public void PrintBoardAsString(SudokuBoard board)
        {
            string boardString = BoardFormatter.ConvertBoardToString(board);
            Console.WriteLine(boardString);
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine($"\n{message}");
        }

        public void PrintError(string error)
        {
            Console.WriteLine($"\nError: {error}");
        }

        public void PrintBoard(SudokuBoard board)
        {
            string boardString = BoardFormatter.ConvertBoardToString(board);
            PrintBoard(boardString);
        }

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



        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Enter Sudoku board manually.");
            Console.WriteLine("2. Enter Sudoku board from file.");
            Console.WriteLine("3. Open sudoku chars dictionary.");
            Console.WriteLine("4. Exit the program.");
            Console.Write("\nPlease choose an option: ");
        }

        public void RequestBoardInput(int boardSize)
        {
            Console.WriteLine($"Please enter {boardSize}x{boardSize} sudoku board as one string (empty cells represented by '0'): ");
        }
        public void RequestBoardSize()
        {
            Console.Write($"Please enter the sudoku board size (has to be a value between {Constants.MinBoardSize}-{Constants.MaxBoardSize} that it's square root is an integer): ");
        }

        public void ShowWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to Itamar's Omega Sudoku!");
            Console.ResetColor();
        }

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
