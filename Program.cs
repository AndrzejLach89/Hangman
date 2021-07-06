using System;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("godmode"))
            {
                GameSettings.GodMode = true;
            }
            GameMenu.MainMenu();
        }
    }
}
