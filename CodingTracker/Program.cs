using CodingTracker.UI;
using CodingTracker.Data;
namespace CodingTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConsoleUI UI = new ConsoleUI(new DataAccess());
                UI.Run();
            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
