namespace Smells.Interfaces
{
    public interface IMooGame
    {
        int Guesses { get; }
        string GetHint(string guess);
        bool IsGuessCorrect(string guess);
        bool IsOngoing();
        bool IsValidGuess(string guess);
        void SetupNewGame();
    }
}