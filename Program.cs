using Smells.Interfaces;

namespace Smells
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IMooGame game = new AlphabetMooGame();
            IUI ui = new ConsoleUI();
            IDataStorage dataStorage = new TextDataStorage("resultAlphabet.txt");
            IGameStats stats = new MooGameStats(dataStorage);
            GameController controller = new GameController(game, ui, stats);
            controller.Run();

        }
    }
}