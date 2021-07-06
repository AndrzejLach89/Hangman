using System;
using System.Collections.Generic;
using System.IO;

namespace Hangman
{
    static class GameSettings
    {
        public static int WindowWidth { get; private set; }
        private static int _minWidth = 80;
        public static Dictionary<string, string> Settings;
        static GameSettings()
        {
            try
            {
                LoadSettings();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Settings = CreateConfig();
                SaveConfig();
            }
            WindowWidth = Console.WindowWidth;
            if (WindowWidth < _minWidth)
            {
                WindowWidth = _minWidth;
            }
        }

        private static void LoadSettings()
        {
            Settings = ReadData.ReadLines("game.cfg", '-');
            try
            {
                _minWidth = Convert.ToInt32(Settings["minWidth"]);
            }
            catch
            {
                _minWidth = 80;
            }
            
        }

        private static Dictionary<string, string> CreateConfig()
        {
            var config = new Dictionary<string, string>()
            {
                {"countriesPath", "./countries_and_capitals.txt" },
                {"symbol", "_" },
                {"lives", "5" },
                {"minWidth", "80" }
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
