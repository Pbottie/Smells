using Smells.Interfaces;

namespace Smells
{
    internal class TextDataStorage : IDataStorage
    {
        const string ScoreSeparator = "#&#";
        public string ResultsFilename { get; private set; }


        public TextDataStorage(string fileName)
        {
            ResultsFilename = fileName;
            CreateFile();
        }
        public List<PlayerData> GetScores()
        {
            StreamReader input = new StreamReader(ResultsFilename);
            #region OLD
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

            #endregion
            input.Close();

            return players;
        }

        public void RecordScore(string playerName, int guesses)
        {
            StreamWriter output = new StreamWriter(ResultsFilename, append: true);
            output.WriteLine(playerName + ScoreSeparator + guesses);
            output.Close();

        }

        private void CreateFile()
        {
            StreamWriter sr = new StreamWriter(ResultsFilename, append: true);
            sr.Close();
        }
    }
}
