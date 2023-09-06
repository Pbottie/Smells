using Smells.Interfaces;
using System.Runtime.InteropServices.Marshalling;

namespace Smells
{
    internal class TextDataStorage : IDataStorage
    {
        const string ScoreSeparator = "#&#";
        public string ResultsAccess { get; set; }


        public TextDataStorage()
        {
            ResultsAccess = "default.txt";
            CreateFile();
        }
        public List<PlayerData> GetScores()
        {
            using (StreamReader input = new StreamReader(ResultsAccess))
            {
                List<PlayerData> players = new List<PlayerData>();

                string? line;
                while ((line = input.ReadLine()) != null)
                {
                    string[] nameAndGuesses = line.Split(ScoreSeparator, StringSplitOptions.None);
                    string name = nameAndGuesses[0];
                    int guesses = Convert.ToInt32(nameAndGuesses[1]);
                    PlayerData playerData = new PlayerData(name, guesses);

                    int playerIndex = players.IndexOf(playerData);
                    if (playerIndex < 0)
                    {
                        players.Add(playerData);
                    }
                    else
                    {
                        players[playerIndex].Update(guesses);
                    }
                }
                players.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
                return players;
            }

        }

        public void RecordScore(string playerName, int guesses)
        {
            using (StreamWriter output = new StreamWriter(ResultsAccess, append: true))
            {
                output.WriteLine(playerName + ScoreSeparator + guesses);
            }

        }

        private void CreateFile()
        {
            StreamWriter sr = new StreamWriter(ResultsAccess, append: true);
            sr.Close();
        }
    }
}
