using Smells.Interfaces;

namespace Smells
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IMooGame game = new MooGame();
            IUI ui = new ConsoleUI();
            IDataStorage dataStorage = new TextDataStorage();
            IGameStats stats = new MooGameStats(dataStorage);
            GameController controller = new GameController(game, ui, stats);
            controller.Run();

        }
    }
}