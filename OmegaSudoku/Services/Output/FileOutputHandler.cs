using OmegaSudoku.Models;

namespace OmegaSudoku.Services.Output
{

    /// <summary>
    /// This class represents a file output handler. It handles writing output to a file. 
    /// It also allows printing the Sudoku board, messages, and errors to a specified file.
    /// </summary>

    public class FileOutputHandler : IOutputHandler
    {
        private readonly string _filePath; // file path string

        /// <summary>
        /// Constructor to initialize the FileOutputHandler with the given file path.
        /// Throws an exception if the provided file path is invalid or null.
        /// </summary>
        /// <param name="filePath">The file path where the output will be written.</param>
        /// <exception cref="ArgumentException">Thrown if the file path is invalid or null.</exception>
        public FileOutputHandler(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is invalid");

            filePath = filePath.Trim();
            filePath = filePath.Trim('"');
            _filePath = filePath;
        }

        /// <summary>
        /// Writes the Sudoku board as a string to the file.
        /// </summary>
        /// <param name="board">The Sudoku board to be written to the file.</param>
        public void PrintBoardAsString(SudokuBoard board)
        {
            try
            {
                string boardString = board.ConvertBoardToString();
                File.AppendAllText(_filePath, "\nSolved board:\n");
                File.AppendAllText(_filePath, boardString + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing board to file: {ex.Message}");
            }
        }

        /// <summary>
        /// Writes a general message to the file.
        /// </summary>
        /// <param name="message">The message to be written to the file.</param>
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

        /// <summary>
        /// Writes an error message to the file.
        /// </summary>
        /// <param name="error">The error message to be written to the file.</param>
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
    }

}
