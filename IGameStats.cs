namespace Smells
{
    public interface IGameStats
    {
        string CurrentPlayerName { get; set; }
        List<PlayerData> Players { get; set; }

        void RecordStats(int guesses);
    }
}
