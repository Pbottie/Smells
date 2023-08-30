namespace Smells.Interfaces
{
    public interface IMooGame
    {
        int Guesses { get; }
        bool IsOngoing { get; }
        string GetHint(string guess);
        bool IsGuessCorrect(string guess);

        bool IsValidGuess(string guess);
        void SetupNewGame();
    }
}