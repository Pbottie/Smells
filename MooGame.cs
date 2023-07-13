using System.Text.RegularExpressions;

namespace Smells
{
    public class MooGame
    {
        public int Guesses { get; set; }
        private bool isOngoing;
        private int unknownNumbers;
        private string answer;
        public MooGame()
        {
            SetupNewGame();
        }
        public void SetupNewGame()
        {
            isOngoing = true;
            Guesses = 0;
            unknownNumbers = 4;
            answer = generateAnswer();
        }

        public bool IsOngoing()
        {
            return isOngoing;
        }
        #region bridging methods
        public void setIsNotFinished(bool value) { isOngoing = value; }
        public string getAnswer() { return answer; }

        #endregion


        public bool IsValidGuess(string guess)
        {
            //Match 4 unique digits
            Regex regex = new Regex(@"^(?:([\d])(?!.*\1)){4}$");
            if (regex.IsMatch(guess))
            {
                incrementGuesses();
                return true;
            }

            return false;
        }

        public bool IsGuessCorrect(string guess)
        {
            if (guess == answer)
                return true;
            return false;

        }

        public string GetHint(string guess)
        {
            int cows = 0, bulls = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (answer[i] == guess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);

        }



        private void incrementGuesses()
        {
            Guesses++;
        }
        private string generateAnswer()
        {
            Random randomGenerator = new Random();
            string goal = "";
            for (int i = 0; i < unknownNumbers; i++)
            {
                int random = randomGenerator.Next(10);
                string randomDigit = "" + random;
                while (goal.Contains(randomDigit))
                {
                    random = randomGenerator.Next(10);
                    randomDigit = "" + random;
                }
                goal = goal + randomDigit;
            }
            return goal;

        }

    }
}