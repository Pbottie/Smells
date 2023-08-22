namespace Smells
{
    public class MooGameStats : IGameStats
    {
        public string CurrentPlayerName { get; set; }
        public List<PlayerData> Players { get; set; }
        const string ResultsFilename = "result.txt";
        const string ScoreSeparator = "#&#";
        public MooGameStats()
        {
            CurrentPlayerName = "";
            Players = new List<PlayerData>();
            MakeSureResultsFileExists();
            ParseResultsFile();
        }

        private void MakeSureResultsFileExists()
        {
            new StreamWriter(ResultsFilename, true).Close();
        }

        private void ParseResultsFile()
        {
            StreamReader input = new StreamReader(ResultsFilename);

            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(ScoreSeparator, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new PlayerData(name, guesses);


                int pos = Players.IndexOf(pd);
                if (pos < 0)
                {
                    Players.Add(pd);
                }
                else
                {
                    Players[pos].Update(guesses);
                }


            }
            Players.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));
            input.Close();
        }

        public void RecordStats(int guesses)
        {
            StreamWriter output = new StreamWriter(ResultsFilename, append: true);
            output.WriteLine(CurrentPlayerName + ScoreSeparator + guesses);
            output.Close();
        }

    }
}
