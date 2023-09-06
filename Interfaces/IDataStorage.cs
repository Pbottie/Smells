namespace Smells.Interfaces
{
    public interface IDataStorage
    {
        public string ResultsAccess { get; set; }
        public List<PlayerData> GetScores();
        public void RecordScore(string name, int score);
    }
}
