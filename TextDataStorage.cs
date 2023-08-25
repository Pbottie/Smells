using Smells.Interfaces;

namespace Smells
{
    internal class TextDataStorage : IDataStorage
    {
        const string ResultsFilename = "result.txt";
        const string ScoreSeparator = "#&#";
        public List<PlayerData> GetScores()
        {
            StreamReader input = new StreamReader(ResultsFilename);
            List<PlayerData> players = new List<PlayerData>();

            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndGuesses = line.Split(ScoreSeparator, StringSplitOptions.None);
                string name = nameAndGuesses[0];
                int guesses = Convert.ToInt32(nameAndGuesses[1]);
                PlayerData pd = new PlayerData(name, guesses);

                int pos = players.IndexOf(pd);
                if (pos < 0)
                {
                    players.Add(pd);
                }
                else
                {
                    players[pos].Update(guesses);
                }
            }
            players.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
            input.Close();

            return players;
        }

        public void RecordScore(string playerName, int guesses)
        {
            StreamWriter output = new StreamWriter(ResultsFilename, append: true);
            output.WriteLine(playerName + ScoreSeparator + guesses);
            output.Close();

        }
    }
}
