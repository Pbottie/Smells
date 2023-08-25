using Smells.Interfaces;

namespace Smells
{
    public class MooGameStats : IGameStats
    {
        public string CurrentPlayerName { get; set; }
        public List<PlayerData> Players { get; set; }
        public IDataStorage DataConnection { get; set; }


        public MooGameStats(IDataStorage dataStorage)
        {
            CurrentPlayerName = "";
            Players = new List<PlayerData>();
            DataConnection = dataStorage;
            GetResults();
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
