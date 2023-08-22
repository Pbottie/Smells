namespace Smells
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            MooGame game = new MooGame();
            IUI ui = new ConsoleUI();
            IGameStats stats = new MooGameStats();
            GameController controller = new GameController(game, ui, stats);
            controller.Run();

        }

    }

}