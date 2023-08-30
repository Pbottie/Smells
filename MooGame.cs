using System.Text.RegularExpressions;
using Smells.Interfaces;

namespace Smells
{
    public class MooGame : IMooGame
    {
        public int Guesses { get; private set; }
        public bool IsOngoing { get; private set; }
        internal int unknownNumbers;
        internal string answer;

        public void SetupNewGame()
        {
            IsOngoing = true;
            Guesses = 0;
            unknownNumbers = 4;
            GenerateAnswer();
        }


        public bool IsValidGuess(string guess)
        {
            //Match 4 unique digits like goal
            //Regex regex = new Regex(@"^(?:([\d])(?!.*\1)){4}$");
            //Match 4 digits like original
            Regex regex = new Regex(@"^([\d]){4}$");
            if (regex.IsMatch(guess))
            {
                IncrementGuesses();
                return true;
            }

            return false;
        }

        public bool IsGuessCorrect(string guess)
        {
            if (guess == answer)
            {
                IsOngoing = false;
                return true;
            }
            return false;

        }

        public string GetHint(string guess)
        {
            int cows = 0, bulls = 0;
            for (int i = 0; i < unknownNumbers; i++)
            {
                for (int j = 0; j < unknownNumbers; j++)
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

        internal void IncrementGuesses()
        {
            Guesses++;
        }
        internal void GenerateAnswer()
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
            answer = goal;
        }

    }
}