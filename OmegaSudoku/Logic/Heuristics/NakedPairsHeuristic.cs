using OmegaSudoku.Models;

namespace OmegaSudoku.Logic.Heuristics
{

    /// <summary>
    /// This class represents a heuristic that applies the "Naked Pairs" technique in sudoku. 
    /// A naked pair is when two empty cells in the same row/column/block have the same two possible values. 
    /// The heuristic removes those values from all other empty cells in the same row/column/block 
    /// to reduce the possibilities, which helps to solve the board faster.
    /// </summary>
    public class NakedPairsHeuristic : IHeuristic
    {

        /// <summary>
        /// Applies the naked pairs sudoku heuristic to the whole board.
        /// <param name="board"> The Sudoku board to be applied. </param>
        /// </summary>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        public bool ApplyHeuristic(SudokuBoard board)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;

            for (int row = 0; row < boardSize; row++)
            {
                changeFlag |= ApplyNakedPairsInRow(board, row);
            }

            for (int col = 0; col < boardSize; col++)
            {
                changeFlag |= ApplyNakedPairsInColumn(board, col);
            }

            for (int blockRow = 0; blockRow < boardSize; blockRow += board.BlockSize)
            {
                for (int blockCol = 0; blockCol < boardSize; blockCol += board.BlockSize)
                {
                    changeFlag |= ApplyNakedPairsInBlock(board, board.BlockSize, blockCol);
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the naked pairs sudoku huristic to a given row on the board.
        /// </summary>
        /// <param name="row"> The Sudoku board row to apply the naked pair huristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool ApplyNakedPairsInRow(SudokuBoard board, int row)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;
            List<BoardCell> emptyCells = board.GetEmptyCellsInRow(row);

            // looking for two empty cells with the same possibilities pair
            for (int firstValueIndex = 0; firstValueIndex < emptyCells.Count; firstValueIndex++)
            {
                for (int secondValueIndex = firstValueIndex + 1; secondValueIndex < emptyCells.Count; secondValueIndex++)
                {
                    var cell1 = emptyCells[firstValueIndex];
                    var cell2 = emptyCells[secondValueIndex];

                    var possibilities1 = cell1.GetPossibilities();
                    var possibilities2 = cell2.GetPossibilities();

                    if (possibilities1.SetEquals(possibilities2) && possibilities1.Count == 2)
                    {
                        // removing the possibilities pair from the empty cell in the given row
                        for (int col = 0; col < boardSize; col++)
                        {
                            if (col != cell1.Col && col != cell2.Col)
                            {
                                board.RemoveCellPossibility(row, col, possibilities1.First());
                                board.RemoveCellPossibility(row, col, possibilities1.Last());
                                changeFlag = true;
                            }
                        }
                    }
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the naked pairs sudoku huristic to a given column on the board.
        /// </summary>
        /// <param name="col"> The Sudoku board column to apply the naked pair huristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool ApplyNakedPairsInColumn(SudokuBoard board, int col)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;
            List<BoardCell> emptyCells = board.GetEmptyCellsInColumn(col);

            // looking for two empty cells with the same possibilities pair
            for (int firstValueIndex = 0; firstValueIndex < emptyCells.Count; firstValueIndex++)
            {
                for (int secondValueIndex = firstValueIndex + 1; secondValueIndex < emptyCells.Count; secondValueIndex++)
                {
                    var cell1 = emptyCells[firstValueIndex];
                    var cell2 = emptyCells[secondValueIndex];

                    var possibilities1 = cell1.GetPossibilities();
                    var possibilities2 = cell2.GetPossibilities();

                    if (possibilities1.SetEquals(possibilities2) && possibilities1.Count == 2)
                    {
                        // removing the possibilities pair from the empty cell in the given column
                        for (int row = 0; row < boardSize; row++)
                        {
                            if (row != cell1.Row && row != cell2.Row)
                            {
                                board.RemoveCellPossibility(row, col, possibilities1.First());
                                board.RemoveCellPossibility(row, col, possibilities1.Last());
                                changeFlag = true;
                            }
                        }
                    }
                }
            }
            return changeFlag;
        }

        /// <summary>
        /// Applies the naked pairs sudoku huristic to a given block on the board.
        /// </summary>
        /// <param name="blockStartRow"> The Sudoku board start row of the block to apply the naked pair huristic. </param>
        /// <param name="blockStartCol"> The Sudoku board start column of the block to apply the naked pair huristic. </param>
        /// <returns> returns true if there was a change on the board. eles - returns false.</returns>
        private static bool ApplyNakedPairsInBlock(SudokuBoard board, int blockStartRow, int blockStartCol)
        {
            bool changeFlag = false;
            int boardSize = board.BoardSize;
            List<BoardCell> emptyCells = board.GetEmptyCellsInBlock(blockStartRow, blockStartCol);

            // looking for two empty cells with the same possibilities pair
            for (int firstValueIndex = 0; firstValueIndex < emptyCells.Count; firstValueIndex++)
            {
                for (int secondValueIndex = firstValueIndex + 1; secondValueIndex < emptyCells.Count; secondValueIndex++)
                {
                    var cell1 = emptyCells[firstValueIndex];
                    var cell2 = emptyCells[secondValueIndex];

                    var possibilities1 = cell1.GetPossibilities();
                    var possibilities2 = cell2.GetPossibilities();

                    if (possibilities1.SetEquals(possibilities2) && possibilities1.Count == 2)
                    {
                        // removing the possibilities pair from the empty cell in the given block
                        for (int row = blockStartRow; row < blockStartRow + board.BlockSize; row++)
                        {
                            for (int col = blockStartCol; col < blockStartCol + board.BlockSize; col++)
                            {
                                if ((row != cell1.Row || col != cell1.Col) && (row != cell2.Row || col != cell2.Col))
                                {
                                    board.RemoveCellPossibility(row, col, possibilities1.First());
                                    board.RemoveCellPossibility(row, col, possibilities1.Last());
                                    changeFlag = true;
                                }
                            }
                        }
                    }
                }
            }
            return changeFlag;
        }
    }
}
