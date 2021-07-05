using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    static class GameMenu
    {
        public static void MainMenu()
        {
            bool status = true;
            do
            {
                status = ShowMenu();
            }
            while (status);
            Console.Clear();
            Environment.Exit(0);
        }

        private static bool ShowMenu()
        {
            Console.Clear();
            int n = GameSettings.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("".PadLeft(n, '='));
            string header = "H A N G M A N";
            header = header.PadLeft((n + header.Length) / 2, ' ').PadRight(n, ' ');
            Console.WriteLine(header);
            Console.WriteLine("".PadLeft(n, '='));
            Console.ResetColor();
            Console.WriteLine("\n");

            //string[] options = { ">> MAIN MENU <<", "\n", "(N)EW GAME", "(H)IGH SCORES", "(Q)UIT" };
            string[] options = { ">> MAIN MENU <<", "(N)EW GAME", "(H)IGH SCORES", "(Q)UIT" };
            foreach (string i in options)
            {
                string line = i;
                line = line.PadLeft((n + line.Length) / 2, ' ').PadRight(n, ' ');
                //string line = i.PadLeft((n / 2 - i.Length) / 2, '.');//.PadRight(n, ' ');
                Console.WriteLine(line);
            }
            return SelectOption();
        }

        private static bool SelectOption()
        {
            char input = Char.ToLower(Console.ReadKey().KeyChar);
            switch (input)
            {
                case 'q':
                    return false;
                    //break;
                case 'n':
                    new Game();
                    return true;
                    //break;
                case 'h':
                    new HighScores().ShowHighScores(null);
                    return true;
                    //break;
                default:
                    return true;
                    //break;
            }
        }
    }
}
