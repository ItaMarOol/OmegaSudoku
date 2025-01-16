using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Models
{
    public class BoardCell
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        private HashSet<int> _possibleValues { get; set; }


        public BoardCell(int row, int col, int Boardsize, int value)
        {
            Row = row;
            Col = col;
            _possibleValues = new HashSet<int>();
            if (IsEmpty())
                InitializePossibilities(Boardsize); // adding all possible cell values
        }
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

        public void SetValue(int newValue)
        {
            if (!IsEmpty())
                _possibleValues.Clear();
            _possibleValues.Add(newValue);

            // I will add here possibilities initlize after adding board size in constants class
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

    }
}
