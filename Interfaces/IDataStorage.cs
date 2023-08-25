namespace Smells.Interfaces
{
    public interface IDataStorage
    {
        public List<PlayerData> GetScores();
        public void RecordScore(string name, int score);
    }
}
