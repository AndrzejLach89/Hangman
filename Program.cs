using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Setup.CapitolsLoaded + "!");
            Console.WriteLine(Setup.CapitolsData.PickCountry());
            GameMenu.MainMenu();
            //string dupa;
            //dupa = Console.ReadLine();
            //Console.WriteLine(dupa);
        }
    }
}
