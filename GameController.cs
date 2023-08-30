using Smells.Interfaces;

namespace Smells
{
    public class GameController
    {
        private IMooGame game;
        private IUI ui;
        private IGameStats stats;

        public GameController(IMooGame game, IUI ui, IGameStats stats)
        {
            this.game = game;
            this.ui = ui;
            this.stats = stats;

        }

        public void Run()
        {
            SetPlayerName();
            SetupNewGame();
            while (game.IsOngoing)
            {
                RunGame();
                CheckToPlayAgain();
            }

        }

        internal void SetPlayerName()
        {
            ui.WriteString("Enter your user name:\n");
            stats.CurrentPlayerName = ui.GetString();
        }

        internal void SetupNewGame()
        {
            game.SetupNewGame();
        }

        internal void RunGame()
        {
            ui.WriteString("New game:\n");

            string guess = "";
            while (game.IsOngoing)
            {
                guess = GetValidGuess();
                OutputHint(guess);
                if (game.IsGuessCorrect(guess))
                {
                    RecordResult();
                    ShowResultsAndEndGame();
                }
            }

        }
        internal void CheckToPlayAgain()
        {
            ui.WriteString("Continue?");
            string answer = GetYesNoInput();
            if (answer == "y")
                SetupNewGame();

        }

        internal void OutputHint(string guess)
        {
            string hint = game.GetHint(guess);
            ui.WriteString(hint);
        }
        internal void RecordResult()
        {
            stats.RecordStats(game.Guesses);
        }

        internal void ShowResultsAndEndGame()
        {
            ShowTopList();
            ui.WriteString("Correct, it took " + game.Guesses + " guesses");
        }

        internal string GetValidGuess()
        {
            string guess;
            bool isInvalidGuess = true;
            do
            {
                guess = ui.GetString();
                if (game.IsValidGuess(guess))
                    isInvalidGuess = false;
                else
                    ui.WriteString("Guess is in the wrong format, try again");

            } while (isInvalidGuess);


            return guess;
        }
        internal string GetYesNoInput()
        {
            string answer;
            bool isInvalid = true;

            do
            {
                answer = ui.GetString().Trim().ToLower();
                if (answer == "y" || answer == "n")
                    isInvalid = false;
                else
                    ui.WriteString("Answer with Y/y or N/n");

            } while (isInvalid);

            return answer;
        }

        internal void ShowTopList()
        {
            ui.WriteString("Player   games average");
            foreach (PlayerData p in stats.Players)
            {
                ui.WriteString(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.GamesPlayed, p.Average()));
            }

        }
    }
}
