namespace Smells
{
    public class PlayerData
    {

        public string Name { get; private set; }
        public int NGames { get; private set; }
        int totalGuess;
        public PlayerData(string name, int guesses)
        {
            this.Name = name;
            NGames = 1;
            totalGuess = guesses;
        }

        public void Update(int guesses)
        {
            totalGuess += guesses;
            NGames++;
        }

        public double Average()
        {
            return (double)totalGuess / NGames;
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
