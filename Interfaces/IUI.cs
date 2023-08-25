namespace Smells.Interfaces
{
    public interface IUI
    {
        void WriteString(string s);
        string GetString();
        void Exit();
        void Clear();
    }
}
