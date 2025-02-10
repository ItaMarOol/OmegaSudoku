using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Utilities
{
    public static class Constants
    {

        public static int BoardSize = 9; // board size (for example - '9' means the board size is 9x9)
        public const int MinBoardSize = 1; // minimum board size
        public const int MaxBoardSize = 25; // maximum board size
        public const int MinCellValue = 1; // minimum cell value
        public static int MaxCellValue = BoardSize; // maximum cell value (equals to the board size)

        public const char AsciiDigitDiff = '0'; // ascii digit that represents the differenece between integer values and their ascii values


    }

}
