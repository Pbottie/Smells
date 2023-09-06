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
        }

        internal void GetResults()
        {
            if (DataConnection == null)
                ThrowNullException();
            else
                Players = DataConnection.GetScores();
        }

        public void RecordStats(int guesses)
        {
            if (DataConnection == null)
                ThrowNullException();
            else
            {
                DataConnection.RecordScore(CurrentPlayerName, guesses);
                GetResults();
            }
        }

        internal void ThrowNullException()
        {
            throw new NullReferenceException("DataConnection is not set!");
        }

    }
}
