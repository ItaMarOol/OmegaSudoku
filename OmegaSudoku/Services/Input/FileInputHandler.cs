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

        public FileInputHandler(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is invalid");
            filePath = filePath.Trim();
            filePath = filePath.Trim('"');
            _filePath = filePath;
        }
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
