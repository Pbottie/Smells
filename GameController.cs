namespace Smells
{
    public class GameController
    {
        private MooGame game;
        private IUI ui;
        private string name;

        public GameController(MooGame game, IUI ui)
        {
            this.game = game;
            this.ui = ui;
        }

        public void Run()
        {
            getPlayerData();

            while (game.IsOngoing())
            {
                runGame();
            }
        }

        private void getPlayerData()
        {
            ui.WriteString("Enter your user name:\n");
            name = ui.GetString();
        }

        private void runGame()
        {
            game.SetupNewGame();

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
                    endGame();
                }
            }



            StreamWriter output = new StreamWriter("result.txt", append: true);
            output.WriteLine(name + "#&#" + game.Guesses);
            output.Close();
            showTopList();
            ui.WriteString("Correct, it took " + game.Guesses + " guesses\nContinue?");
            string answer = ui.GetString();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                game.setIsNotFinished(false);
            }
        }

        private void outputHint(string guess)
        {
            string hint = game.GetHint(guess);

            ui.WriteString(hint);
        }

        private void endGame()
        {
            throw new NotImplementedException();
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


        void showTopList()
        {
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new PlayerData(name, guesses);
                int pos = results.IndexOf(pd);
                if (pos < 0)
                {
                    results.Add(pd);
                }
                else
                {
                    results[pos].Update(guesses);
                }


            }
            results.Sort((p1, p2) => p1.Average().CompareTo(p2.Average()));

            ui.WriteString("Player   games average");
            foreach (PlayerData p in results)
            {
                ui.WriteString(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NGames, p.Average()));
            }
            input.Close();
        }
    }
}
