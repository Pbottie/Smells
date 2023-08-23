namespace Smells
{
    public class GameController
    {
        private MooGame game;
        private IUI ui;
        IGameStats stats;

        public GameController(MooGame game, IUI ui, IGameStats stats)
        {
            this.game = game;
            this.ui = ui;
            this.stats = stats;

        }

        public void Run()
        {
            GetPlayerData();
            SetupNewGame();
            while (game.IsOngoing())
            {
                RunGame();
                CheckToPlayAgain();
            }

        }

        private void GetPlayerData()
        {
            ui.WriteString("Enter your user name:\n");
            stats.CurrentPlayerName = ui.GetString();
        }

        private void SetupNewGame()
        {
            game.SetupNewGame();
        }

        private void RunGame()
        {
            ui.WriteString("New game:\n");
            //comment out or remove next line to play real games!
            //ui.WriteString("For practice, number is: " + game.getAnswer() + "\n");


            string guess = "";
            while (game.IsOngoing())
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
        private void CheckToPlayAgain()
        {
            ui.WriteString("Continue?");
            string answer = GetValidInput();
            if (answer == "y")
                SetupNewGame();

        }

        private void OutputHint(string guess)
        {
            string hint = game.GetHint(guess);
            ui.WriteString(hint);
        }
        private void RecordResult()
        {
            stats.RecordStats(game.Guesses);
        }

        private void ShowResultsAndEndGame()
        {
            ShowTopList();
            ui.WriteString("Correct, it took " + game.Guesses + " guesses");
        }

        private string GetValidGuess()
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
        private string GetValidInput()
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

        void ShowTopList()
        {
            ui.WriteString("Player   games average");
            foreach (PlayerData p in stats.Players)
            {
                ui.WriteString(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.GamesPlayed, p.Average()));
            }

        }
    }
}
