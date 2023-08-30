namespace Smells
{
    public class PlayerData
    {

        public string Name { get; private set; }
        public int GamesPlayed { get; private set; }
        int totalGuesses;
        public PlayerData(string name, int guesses)
        {
            Name = name;
            GamesPlayed = 1;
            totalGuesses = guesses;
        }

        public void Update(int guesses)
        {
            totalGuesses += guesses;
            GamesPlayed++;
        }

        public double Average()
        {
            return (double)totalGuesses / GamesPlayed;
        }


        override public bool Equals(Object? p)
        {
            if (p == null) return false;
            return Name.Equals(((PlayerData)p).Name);
        }


        override public int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
