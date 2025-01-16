using OmegaSudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Services.Output
{
    public class BoardFormatter
    {

        /// <summary>
        /// Converts a given Sudoku board to a single long string representation.
        /// </summary>
        /// <param name="board">The Sudoku board to be converted.</param>
        /// <returns>A single long string representing the board.</returns>
        public string ConvertBoardToString(SudokuBoard board)
        {
            StringBuilder boardString = new StringBuilder();

            for (int row = 0; row < board.BoardSize; row++)
            {
                for (int col = 0; col < board.BoardSize; col++)
                {
                    int value = board.GetCellValue(row, col);
                    boardString.Append(value);
                }
            }
            return boardString.ToString();
        }
    }
}
