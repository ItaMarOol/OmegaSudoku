using OmegaSudoku.Logic;
using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Output
{
    internal class FileOutputHandler : IOutputHandler
    {
        private readonly string _filePath;

        public FileOutputHandler(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is invalid");

            filePath = filePath.Trim();
            filePath = filePath.Trim('"');
            _filePath = filePath;
        }

        public void PrintBoardAsString(SudokuBoard board)
        {
            try
            {
                string boardString = BoardFormatter.ConvertBoardToString(board);
                File.AppendAllText(_filePath, "\nSolved board:\n");
                File.AppendAllText(_filePath, boardString + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing board to file: {ex.Message}");
            }
        }

        public void PrintError(string error)
        {
            try
            {
                string errorMessage = $"Error: {error}{Environment.NewLine}";
                File.AppendAllText(_filePath, errorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing error message to file: {ex.Message}");
            }
        }

        public void PrintMessage(string message)
        {
            try
            {
                string messageText = $"{message}{Environment.NewLine}";
                File.AppendAllText(_filePath, messageText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing message to file: {ex.Message}");
            }
        }
    }

}
