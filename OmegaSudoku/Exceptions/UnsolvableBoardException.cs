using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    class UnsolvableBoardException : Exception
    {
        public UnsolvableBoardException()
            : base("Unsolvable board entered")
        { }
    }
}
