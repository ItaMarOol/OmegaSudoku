using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class InvalidCellValuesException : Exception
    {
        public InvalidCellValuesException(List<char> invalidCellValues) 
            : base($"Invalid cell values entered: {string.Join(",", invalidCellValues)}")
        { }
    }
}
