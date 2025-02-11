using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions
{
    public class UnsolvableBoardException : Exception
    {

        /// <summary>
        /// Constructor to initialize an UnsolvableBoardException with a default message that the board is unsolvable.
        /// </summary>
        public UnsolvableBoardException()
            : base("Unsolvable board entered")
        { }
    }
}
