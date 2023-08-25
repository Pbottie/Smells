namespace Smells.Interfaces
{
    public interface IGameStats
    {
        public IDataStorage DataConnection { get; set; }
        string CurrentPlayerName { get; set; }
        List<PlayerData> Players { get; set; }
        void RecordStats(int guesses);
    }

}
