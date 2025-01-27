using OmegaSudoku.Models;
using OmegaSudoku.Services.Output;
using OmegaSudoku.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Input
{
    public class CliInputHandler : IInputHandler
    {
        private readonly CliOutputHandler _outputHandler;

        public CliInputHandler()
        {
            _outputHandler = new CliOutputHandler();
        }
        public string GetInput()
        {
            String input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return input.Trim();
        }

        public string GetBoardInput(int boardSize)
        {
            _outputHandler.RequestBoardInput(boardSize);
            return GetInput();
        }
        public int GetBoardSize()
        {
            while (true)
            {
                _outputHandler.RequestBoardSize();
                string boardSize = GetInput();

                if (int.TryParse(boardSize, out int size))
                {
                    if (Math.Sqrt(size) % 1 == 0 && size >= Constants.MinBoardSize && size <= Constants.MaxBoardSize)
                    {
                        return size;
                    }
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
