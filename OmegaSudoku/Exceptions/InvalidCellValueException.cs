using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class InvalidCellValueException : Exception
    {
        public InvalidCellValueException(int invalidCellValue) 
            : base($"Invalid cell value entered: {invalidCellValue}")
        { }
    }
}
