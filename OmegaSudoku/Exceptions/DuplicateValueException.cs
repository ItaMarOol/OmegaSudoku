using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class DuplicateValueException : Exception
    {
        public DuplicateValueException(int duplicatedValue)
            : base($"Duplicated value in the same row/column/block entered: '{duplicatedValue}'")
        { }
    }
}
