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
