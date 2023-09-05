using Smells.Interfaces;

namespace Smells
{
    public class MooGameStats : IGameStats
    {
        public string CurrentPlayerName { get; set; }
        public List<PlayerData> Players { get; set; }
        public IDataStorage DataConnection { get; set; }


        public MooGameStats()
        {
            CurrentPlayerName = "";
            Players = new List<PlayerData>();
            //GetResults();
        }

        internal void GetResults()
        {
            Players = DataConnection.GetScores();
        }

        public void RecordStats(int guesses)
        {
            DataConnection.RecordScore(CurrentPlayerName, guesses);
            GetResults();
        }

    }
}
