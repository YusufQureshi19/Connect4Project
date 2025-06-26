namespace Final_Project_connect_4_Group_J
{
    public abstract class Player
    {
        public string Name { get; protected set; }
        public char Symbol { get; protected set; }

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public abstract int GetMove();
    }
}