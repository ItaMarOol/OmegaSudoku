using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class InvalidCellValuesException : Exception
    {
        /// <summary>
        /// Constructor to initialize an InvalidCellValuesException object with a list of invalid cell values.
        /// </summary>
        /// <param name="invalidCellValues">The list of invalid cell values that caused the exception.</param>
        public InvalidCellValuesException(List<char> invalidCellValues) 
            : base($"Invalid cell values entered: {string.Join(",", invalidCellValues)}")
        { }
    }
}
