using System;

namespace Final_Project_connect_4_Group_J
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, char symbol) : base(name, symbol) { }

        public override int GetMove()
        {
            Console.Write($"{Name} ({Symbol}), choose a column (1–7): ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int column))
                return column - 1;

            return -1;
        }
    }
}
