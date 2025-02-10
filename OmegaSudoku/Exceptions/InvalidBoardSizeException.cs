using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class InvalidBoardSizeException : Exception
    {
        /// <summary>
        /// Constructor to initialize an InvalidBoardSizeException object with a given invalid board size.
        /// </summary>
        /// <param name="invalidBoardSize">The invalid board size that caused the exception.</param>
        public InvalidBoardSizeException(int invalidBoardSize)
            : base($"Invalid board size. cells amount entered: {invalidBoardSize}")
        { }
    }
}
