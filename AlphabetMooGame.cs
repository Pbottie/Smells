using Smells.Interfaces;
using System.Text.RegularExpressions;

namespace Smells
{
    public class AlphabetMooGame : IMooGame
    {
        public int Guesses { get; private set; }

        public bool IsOngoing { get; private set; }
        internal int unknownLetters = 4;
        internal string answer = "";

        public string GetHint(string guess)
        {
            char[] guessCopy = guess.ToUpper().ToCharArray();
            char[] answerCopy = answer.ToCharArray();
            var (bulls, cows) = GetBullsAndCows(guessCopy, answerCopy);

            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);

        }

        internal (int bulls, int cows) GetBullsAndCows(char[] guessCopy, char[] answerCopy)
        {
            int bulls = 0;
            int cows = 0;
            for (int i = 0; i < unknownLetters; i++)
            {

                if (answerCopy[i] == guessCopy[i])
                {
                    bulls++;
                    answerCopy[i] = '_';
                    guessCopy[i] = '_';
                }
            }

            string remainingAnswerLetters = new string(answerCopy).Replace("_", "");
            string remainingGuessLetters = new string(guessCopy).Replace("_", "");

            foreach (char c in remainingAnswerLetters)
            {
                if (remainingGuessLetters.Contains(c))
                {
                    cows++;
                    remainingGuessLetters = remainingGuessLetters.Replace(c.ToString(), "");
                }
            }

            return (bulls, cows);
        }

        public bool IsGuessCorrect(string guess)
        {
            if (guess.ToUpper() == answer)
            {
                IsOngoing = false;
                return true;
            }
            return false;
        }

        public bool IsValidGuess(string guess)
        {
            //Match 4 Letters
            Regex regex = new Regex(@"^([a-zA-Z]){4}$");
            if (regex.IsMatch(guess))
            {
                Guesses++;
                return true;
            }

            return false;

        }

        public void SetupNewGame()
        {
            IsOngoing = true;
            Guesses = 0;
            unknownLetters = 4;
            GenerateAnswer();
        }

        public void GenerateAnswer()
        {
            string availableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //0-25 string array
            Random random = new Random();

            for (int i = 0; i < unknownLetters; i++)
            {
                answer += availableLetters[random.Next(26)];
            }
            //Remove before Actual gameplay
            Console.WriteLine(answer);
        }
    }
}