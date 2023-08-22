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
            getPlayerData();
            setupNewGame();
            while (game.IsOngoing())
            {
                runGame();
                checkToPlayAgain();
            }

        }

        private void getPlayerData()
        {
            ui.WriteString("Enter your user name:\n");
            stats.CurrentPlayerName = ui.GetString();
        }

        private void setupNewGame()
        {
            game.SetupNewGame();
        }

        private void runGame()
        {
            ui.WriteString("New game:\n");
            //comment out or remove next line to play real games!
            ui.WriteString("For practice, number is: " + game.getAnswer() + "\n");


            string guess = "";
            while (game.IsOngoing())
            {
                guess = getValidGuess();
                outputHint(guess);
                if (game.IsGuessCorrect(guess))
                {
                    recordResult();
                    showResultsAndEndGame();
                }
            }

        }
        private void checkToPlayAgain()
        {
            ui.WriteString("Continue?");
            string answer = getValidInput();
            if (answer == "y")
                setupNewGame();

        }

        private void outputHint(string guess)
        {
            string hint = game.GetHint(guess);
            ui.WriteString(hint);
        }
        private void recordResult()
        {
            stats.RecordStats(game.Guesses);
        }

        private void showResultsAndEndGame()
        {
            showTopList();
            ui.WriteString("Correct, it took " + game.Guesses + " guesses");
        }

        private string getValidGuess()
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
        private string getValidInput()
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

        void showTopList()
        {
            ui.WriteString("Player   games average");
            foreach (PlayerData p in stats.Players)
            {
                ui.WriteString(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.GamesPlayed, p.Average()));
            }

        }
    }
}
