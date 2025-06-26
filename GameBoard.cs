using System;

namespace Final_Project_connect_4_Group_J
{
    public class GameBoard
    {
        private char[,] grid;
        public const int Rows = 6;
        public const int Columns = 7;

        public GameBoard()
        {
            grid = new char[Rows, Columns];
            Reset();
        }

        public void Reset()
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    grid[r, c] = '.';
        }

        public void Display()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                    Console.Write(grid[r, c] + " ");
                Console.WriteLine();
            }
            Console.WriteLine("1 2 3 4 5 6 7");
        }

        public bool DropDisc(int column, char symbol)
        {
            if (column < 0 || column >= Columns) return false;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (grid[r, column] == '.')
                {
                    grid[r, column] = symbol;
                    return true;
                }
            }
            return false;
        }

        public bool IsFull()
        {
            for (int c = 0; c < Columns; c++)
                if (grid[0, c] == '.')
                    return false;
            return true;
        }

        public bool CheckWin(char symbol)
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c <= Columns - 4; c++)
                    if (grid[r, c] == symbol && grid[r, c + 1] == symbol && grid[r, c + 2] == symbol && grid[r, c + 3] == symbol)
                        return true;

            for (int c = 0; c < Columns; c++)
                for (int r = 0; r <= Rows - 4; r++)
                    if (grid[r, c] == symbol && grid[r + 1, c] == symbol && grid[r + 2, c] == symbol && grid[r + 3, c] == symbol)
                        return true;

            for (int r = 3; r < Rows; r++)
                for (int c = 0; c <= Columns - 4; c++)
                    if (grid[r, c] == symbol && grid[r - 1, c + 1] == symbol && grid[r - 2, c + 2] == symbol && grid[r - 3, c + 3] == symbol)
                        return true;

            for (int r = 0; r <= Rows - 4; r++)
                for (int c = 0; c <= Columns - 4; c++)
                    if (grid[r, c] == symbol && grid[r + 1, c + 1] == symbol && grid[r + 2, c + 2] == symbol && grid[r + 3, c + 3] == symbol)
                        return true;

            return false;
        }

        public char GetCell(int row, int col)
        {
            return grid[row, col];
        }

        public void CopyFrom(GameBoard other)
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    grid[r, c] = other.grid[r, c];
        }
    }
}
