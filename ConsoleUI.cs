using Smells.Interfaces;

namespace Smells
{
    public class ConsoleUI : IUI
    {
        public void WriteString(string s)
        {
            Console.WriteLine(s);
        }
        public string GetString()
        {
            return Console.ReadLine();
        }
        public void Exit()
        {
            System.Environment.Exit(0);
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
