namespace Smells
{
    public class PlayerData
    {

        public string Name { get; private set; }
        public int GamesPlayed { get; private set; }
        int totalGuess;
        public PlayerData(string name, int guesses)
        {
            Name = name;
            GamesPlayed = 1;
            totalGuess = guesses;
        }

        public void Update(int guesses)
        {
            totalGuess += guesses;
            GamesPlayed++;
        }

        public double Average()
        {
            return (double)totalGuess / GamesPlayed;
        }


        override public bool Equals(Object p)
        {
            return Name.Equals(((PlayerData)p).Name);
        }


        override public int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
