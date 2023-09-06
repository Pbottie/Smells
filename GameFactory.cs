using Smells.Interfaces;

namespace Smells
{
    public class GameFactory
    {
        public static IMooGame GetGame(string gameType)
        {

            switch (gameType)
            {
                case "1":
                    return new MooGame();
                case "2":
                    return new AlphabetMooGame();
                default: throw new Exception("Gametype does not exist.");
            }

        }


    }
}
