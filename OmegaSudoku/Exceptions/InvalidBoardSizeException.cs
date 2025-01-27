using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class InvalidBoardSizeException : Exception
    {
        public InvalidBoardSizeException(int invalidBoardSize)
            : base($"Invalid cells amount entered: {invalidBoardSize}")
        { }
    }
}
