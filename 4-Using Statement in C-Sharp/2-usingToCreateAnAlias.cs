//creating alias for System.Console
using Koko = System.Console;

namespace HelloWorld
{

    class Program
    {
        static void Main(string[] args)
        {

            // using Koko alias instead of System.Console
            Koko.WriteLine("Hello World!");
            Koko.ReadKey();
        }
    }
}