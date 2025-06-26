using System;

namespace Final_Project_connect_4_Group_J
{
    public class GameController
    {
        private GameBoard board;
        private Player[] players;
        private int currentPlayerIndex;
        private bool isSinglePlayer;

        public static GameBoard CurrentBoard { get; private set; }
        public static char HumanSymbol { get; private set; }

        public GameController()
        {
            board = new GameBoard();
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Connect Four!");
            Console.WriteLine("Select Game Mode:");
            Console.WriteLine("1. One Player (vs Computer)");
            Console.WriteLine("2. Two Player (Human vs Human)");
            Console.Write("Enter option (1 or 2): ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                isSinglePlayer = true;
                SetupSinglePlayer();
            }
            else
            {
                isSinglePlayer = false;
                SetupTwoPlayer();
            }

            bool playAgain = true;

            while (playAgain)
            {
                board.Reset();
                CurrentBoard = board;
                bool gameEnded = false;
                Console.Clear();
                board.Display();

                while (!gameEnded)
                {
                    Player currentPlayer = players[currentPlayerIndex];
                    int move;
                    do
                    {
                        move = currentPlayer.GetMove();
                    } while (!board.DropDisc(move, currentPlayer.Symbol));

                    Console.Clear();
                    board.Display();

                    if (board.CheckWin(currentPlayer.Symbol))
                    {
                        Console.WriteLine($"{currentPlayer.Name} wins!");
                        gameEnded = true;
                    }
                    else if (board.IsFull())
                    {
                        Console.WriteLine("It's a tie!");
                        gameEnded = true;
                    }
                    else
                    {
                        currentPlayerIndex = (currentPlayerIndex + 1) % 2;
                    }
                }

                Console.Write("Play again? (y/n): ");
                playAgain = Console.ReadLine().ToLower() == "y";

                if (playAgain && isSinglePlayer)
                    SetupSinglePlayer();
            }
        }

        private void SetupTwoPlayer()
        {
            players = new Player[2]
            {
                new HumanPlayer("Player 1", 'X'),
                new HumanPlayer("Player 2", 'O')
            };
            currentPlayerIndex = 0;
        }

        private void SetupSinglePlayer()
        {
            Random rnd = new Random();
            bool userFirst = rnd.Next(2) == 0;

            if (userFirst)
            {
                players = new Player[2]
                {
                    new HumanPlayer("You", 'X'),
                    new ComputerPlayer("Computer", 'O')
                };
                HumanSymbol = 'X';
                currentPlayerIndex = 0;
            }
            else
            {
                players = new Player[2]
                {
                    new ComputerPlayer("Computer", 'X'),
                    new HumanPlayer("You", 'O')
                };
                HumanSymbol = 'O';
                currentPlayerIndex = 0;
            }
        }
    }
}
