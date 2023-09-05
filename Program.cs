using Smells.Interfaces;

namespace Smells
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IUI ui = new ConsoleUI();
            IDataStorage dataStorage = new TextDataStorage();
            IGameStats stats = new MooGameStats();
            GameController controller = new GameController(ui, stats, dataStorage);
            controller.Run();

        }
    }
}