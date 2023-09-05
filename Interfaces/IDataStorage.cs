namespace Smells.Interfaces
{
    public interface IDataStorage
    {
        public string ResultsKey { get; set; }
        public List<PlayerData> GetScores();
        public void RecordScore(string name, int score);
    }
}
