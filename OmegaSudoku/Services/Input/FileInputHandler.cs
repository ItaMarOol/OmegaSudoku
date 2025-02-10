using OmegaSudoku.Services.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Input
{
    internal class FileInputHandler : IInputHandler
    {
        private readonly string _filePath;

        /// <summary>
        /// Constructor to initialize a FileInputHandler object.
        /// It also validates the file path.
        /// </summary>
        /// <param name="filePath">The path to the input file containing the board string.</param>
        /// <exception cref="ArgumentException">Thrown when the provided file path is null, empty, or only contains whitespace.</exception>
        public FileInputHandler(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is invalid");
            filePath = filePath.Trim();
            filePath = filePath.Trim('"');
            _filePath = filePath;
        }

        /// <summary>
        /// Reads input from the file in the file path.
        /// </summary>
        /// <returns>A trimmed string containing the data read from the file .</returns>
        /// <exception cref="FileNotFoundException">Thrown when the file at the file path cannot be found.</exception>
        public string GetInput()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("File not found. Please check the path and try again.");
            }
            string userInput = File.ReadAllText(_filePath).Trim();
            return userInput;
        }
        public string GetFilePath()
            { return _filePath; }
    }
}
