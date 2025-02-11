namespace OmegaSudoku.Exceptions
{
    /// <summary>
    /// This class represents an exception that is thrown when the Sudoku board is unsolvable.
    /// </summary>
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
