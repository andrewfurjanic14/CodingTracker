namespace CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUI UI = new ConsoleUI(new DataAccess());
            UI.Run();

            Console.ReadKey();
        }
    }
}
