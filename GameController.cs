using Smells.Interfaces;

namespace Smells
{
    public class GameController
    {
        internal IMooGame game;
        private IUI ui;
        private IGameStats stats;
        private IDataStorage storage;

        public GameController(IUI ui, IGameStats stats, IDataStorage storage)
        {
            this.ui = ui;
            this.stats = stats;
            this.storage = storage;
            stats.DataConnection = storage;

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
            string choice = GetGameType();
            game = GameFactory.GetGame(choice);
            stats.DataConnection.ResultsAccess = "results" + game.GetType().ToString();

            game.SetupNewGame();
        }

        internal string GetGameType()
        {
            string choice;

            ui.WriteString("Choose your game:\n");
            ui.WriteString("1. MooGame\n2. AlphabetMooGame");

            do
            {
                choice = ui.GetString();
            } while ((choice != "1") == (choice != "2"));

            return choice;
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
                    ShowResults();
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

        internal void ShowResults()
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
