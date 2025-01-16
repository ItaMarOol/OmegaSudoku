using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Output
{
    internal class CliOutputHandler : IOutputHandler
    {
        public void printBoard(string board)
        {
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
            Console.WriteLine($"{message}");
        }

        public void PrintError(string error)
        {
            Console.WriteLine($"Error: {error}");
        }




    }
}
