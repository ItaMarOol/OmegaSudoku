using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    public class DuplicateValueException : Exception
    {

        /// <summary>
        /// Constructor to initialize a DuplicateValueException object with a given duplicated value.
        /// </summary>
        /// <param name="duplicatedValue">The duplicated value that caused the exception.</param>
        public DuplicateValueException(int duplicatedValue)
            : base($"Duplicated value in the same row/column/block entered: '{duplicatedValue}'")
        { }
    }
}
