namespace Smells
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            MooGame game = new MooGame();
            GameController controller = new GameController(game);
            controller.Run();
            
        }
        
    }
       
}