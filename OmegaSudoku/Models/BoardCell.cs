using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Models
{
    public class BoardCell
    {
        public int Row { get;}
        public int Col { get;}
        private int Value { get; set; }
        private HashSet<int> PossibleValues { get; set; }


        public BoardCell(int row, int col, int Boardsize, int value)
        {
            Row = row;
            Col = col;
            Value = value;
            PossibleValues = new HashSet<int>();
            if (IsEmpty())
                InitializePossibilities(Boardsize); // adding all possible cell values
        }
        private void InitializePossibilities(int boardSize)
        {
            PossibleValues.UnionWith(Enumerable.Range(1, boardSize));
        }

        public bool IsEmpty()
        {
            return Value == 0;
        }

        public int GetValue()
        { 
            return Value; 
        }

        public void SetValue(int newValue)
        {
            Value = newValue;
            if (!IsEmpty())
                PossibleValues.Clear();

            // I will add here possibilities initlize after adding board size in constants class
        }

        public void AddPossibility(int possibilityValue)
        {
            PossibleValues.Add(possibilityValue);
        }

        public void RemovePossibility(int possibilityValue)
        {
            PossibleValues.Remove(possibilityValue);
        }

        public int GetPossibilitesCount()
        { 
            return PossibleValues.Count; 
        }

    }
}
