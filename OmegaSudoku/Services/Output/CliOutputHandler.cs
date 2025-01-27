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
            int lineLength = boardSize * 2 + ((blockLength - 1) * 2) - 1;

            Console.WriteLine();
            for (int rowIndex = 0; rowIndex < boardSize; rowIndex++)
            {
                string row = board.Substring(rowIndex * boardSize, boardSize); 

                for (int j = 0; j < boardSize; j++)
                {
                    if (j % blockLength == 0 && j != 0) // right side of a block
                    {
                        Console.Write("| "); 
                    }
                    Console.Write($"{row[j]} ");
                }
                
                Console.WriteLine();
                if ((rowIndex + 1) % blockLength == 0 && rowIndex != boardSize-1) // bottom side of a row of blocks
                {
                    Console.WriteLine("-".PadLeft(lineLength, '-')); 
                }
            }
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine($"\n{message}");
        }

        public void PrintError(string error)
        {
            Console.WriteLine($"\nError: {error}");
        }

        public void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Enter Sudoku board manually.");
            Console.WriteLine("2. Enter Sudoku board from file.");
            Console.WriteLine("omega>sigit - Exit the program.");
            Console.Write("Please choose an option: ");
        }

        public void RequestBoardInput(int boardSize)
        {
            Console.WriteLine($"Please enter {boardSize}x{boardSize} sudoku board as one string (empty cells represented by '0'): ");
        }
        public void RequestBoardSize()
        {
            Console.WriteLine($"Please enter the sudoku board size (has to be a value between {Constants.MinBoardSize}-{Constants.MaxBoardSize} that it's square root is an integer): ");
        }



    }
}
