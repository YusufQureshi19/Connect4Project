using System;

namespace Final_Project_connect_4_Group_J
{
    public class ComputerPlayer : Player
    {
        private const int MAX_DEPTH = 6;
        private const int WIN_SCORE = 1000000;
        private const int LOSE_SCORE = -1000000;

        public ComputerPlayer(string name, char symbol) : base(name, symbol) { }

        public override int GetMove()
        {
            int bestMove = -1;
            int bestScore = int.MinValue;

            for (int col = 0; col < GameBoard.Columns; col++)
            {
                GameBoard clone = new GameBoard();
                clone.CopyFrom(GameController.CurrentBoard);

                if (clone.DropDisc(col, Symbol))
                {
                    int score = Minimax(clone, MAX_DEPTH - 1, false, int.MinValue, int.MaxValue);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = col;
                    }
                }
            }

            Console.WriteLine($"{Name} ({Symbol}) plays column {bestMove + 1}");
            System.Threading.Thread.Sleep(500);
            return bestMove;
        }

        private int Minimax(GameBoard board, int depth, bool isMaximizing, int alpha, int beta)
        {
            if (board.CheckWin(Symbol)) return WIN_SCORE;
            if (board.CheckWin(GameController.HumanSymbol)) return LOSE_SCORE;
            if (board.IsFull() || depth == 0) return Evaluate(board);

            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

            for (int col = 0; col < GameBoard.Columns; col++)
            {
                GameBoard clone = new GameBoard();
                clone.CopyFrom(board);

                char currentSymbol = isMaximizing ? Symbol : GameController.HumanSymbol;

                if (clone.DropDisc(col, currentSymbol))
                {
                    int score = Minimax(clone, depth - 1, !isMaximizing, alpha, beta);

                    if (isMaximizing)
                    {
                        bestScore = Math.Max(bestScore, score);
                        alpha = Math.Max(alpha, score);
                    }
                    else
                    {
                        bestScore = Math.Min(bestScore, score);
                        beta = Math.Min(beta, score);
                    }

                    if (beta <= alpha) break;
                }
            }

            return bestScore;
        }

        private int Evaluate(GameBoard board)
        {
            int score = 0;
            for (int r = 0; r < GameBoard.Rows; r++)
            {
                for (int c = 0; c < GameBoard.Columns; c++)
                {
                    if (board.GetCell(r, c) == Symbol)
                        score += 5 + Math.Abs(3 - c);
                    else if (board.GetCell(r, c) == GameController.HumanSymbol)
                        score -= 5 + Math.Abs(3 - c);
                }
            }
            return score;
        }
    }
}
