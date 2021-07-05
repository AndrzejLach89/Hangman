using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    static class GameSettings
    {
        public static int WindowWidth { get; private set; }
        private static int _minWidth = 80;
        public static Dictionary<string, string> Settings;
        static GameSettings()
        {
            WindowWidth = Console.WindowWidth;
            if (WindowWidth < _minWidth)
            {
                WindowWidth = _minWidth;
            }
            try
            {
                Settings = ReadData.ReadLines("game.cfg", '-');
                Console.WriteLine(Settings);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Settings = CreateConfig();
            }
        }

        private static Dictionary<string, string> CreateConfig()
        {
            var config = new Dictionary<string, string>()
            {
                {"countriesPath", "./countries_and_capitals.txt" },
                {"symbol", "_" },
                {"lives", "5" },
                {"caseSensitive", "false" }
            };
            return config;
        }

        public static void SaveConfig()
        {
            string config = "";
            foreach (var kv in Settings)
            {
                config = config + kv.Key + "-" + kv.Value + "\n";
            }
            try
            {
                File.WriteAllText("./game.cfg", config);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
