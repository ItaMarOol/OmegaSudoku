namespace OmegaSudoku.Exceptions
{
    /// <summary>
    /// This class represents an exception that is thrown when invalid cell values are detected in the Sudoku board.
    /// </summary>
    public class InvalidCellValuesException : Exception
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
