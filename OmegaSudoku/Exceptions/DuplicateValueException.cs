namespace OmegaSudoku.Exceptions
{
    /// <summary>
    /// This class represents an exception that is thrown when a duplicated value is detected 
    /// in the same row, column, or block of a Sudoku board (which is invalid by the sudoku rules).
    /// </summary>
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
