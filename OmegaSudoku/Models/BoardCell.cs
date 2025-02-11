namespace OmegaSudoku.Models
{

    /// <summary>
    /// This class represents a single cell on the sudoku board. Each cell contains its row, column, and possible values.
    /// A cell could be empty (with multiple possible values) or filled (with a single and final value).
    /// </summary>
    public class BoardCell
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        private HashSet<int> _possibleValues { get; set; }


        /// <summary>
        /// Constructor to initialize a BoardCell object with a specific row, column, and value.
        /// If the value is 0 (empty), all possible values between 1 and the board size are added to the possible values set.
        /// If the value is not a zero (filled cell), only that value is added to the possible values set.
        /// </summary>
        /// <param name="row">The row of the cell on the Sudoku board.</param>
        /// <param name="col">The column of the cell on the Sudoku board.</param>
        /// <param name="boardSize">The size of the Sudoku board.</param>
        /// <param name="value">The initial value of the cell. If the value is 0 (empty), possible values are set based on the board size.</param>
        public BoardCell(int row, int col, int boardSize, int value)
        {
            Row = row;
            Col = col;
            _possibleValues = new HashSet<int>();
            if (value == 0)
                InitializePossibilities(boardSize); // adding all possible cell values
            else
                _possibleValues.Add(value);
        }

        /// <summary>
        /// Initializes the possible values for an empty cell. It fills the possibilities with all the values between 1 and the board size (inclusive).
        /// This is called when the cell is empty (value is 0).
        /// </summary>
        /// <param name="boardSize">The size of the Sudoku board.</param>
        private void InitializePossibilities(int boardSize)
        {
            _possibleValues.UnionWith(Enumerable.Range(1, boardSize));
        }

        public bool IsEmpty()
        {
            return _possibleValues.Count > 1; // if the cell is empty there are more then 1 possible value
        }

        public int GetValue()
        {
            return _possibleValues.Count == 1 ? _possibleValues.First() : 0;
        }

        public void SetValue(int newValue, int boardSize)
        {
            if (newValue == 0 && !IsEmpty()) // removing a filled cell
                InitializePossibilities(boardSize);
            else if (newValue != 0)
            {
                _possibleValues.Clear();
                _possibleValues.Add(newValue);
            }
        }

        public void AddPossibility(int possibilityValue)
        {
            _possibleValues.Add(possibilityValue);
        }

        public void RemovePossibility(int possibilityValue)
        {
            _possibleValues.Remove(possibilityValue);
        }

        public int GetPossibilitesCount()
        {
            return _possibleValues.Count;
        }

        public HashSet<int> GetPossibilities() { return _possibleValues; }
        public void SetPossibilities(HashSet<int> possibilities) { _possibleValues = possibilities; }

    }
}
