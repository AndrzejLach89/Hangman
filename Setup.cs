using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    static class Setup
    {
        public static CountriesAndCapitols CapitolsData { get; private set;  }
        public static bool CapitolsLoaded { get; private set; }

        static Setup()
        {
            try
            {
                CapitolsData = LoadCapitols();
                CapitolsLoaded = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                CapitolsData = new CountriesAndCapitols();
                CapitolsLoaded = false;
                Console.WriteLine("dupa");
            }
        }

        private static CountriesAndCapitols LoadCapitols()
        {
            var loadedData = new CountriesAndCapitols();
            string[] lines = File.ReadAllLines("./countries_and_capitals.txt");
            foreach (string i in lines)
            {
                if (i.Contains('|'))
                {
                    string[] splittedLine = i.Split('|');
                    string country = splittedLine[0].Trim(' ');
                    string capitol = splittedLine[1].Trim(' ');
                    loadedData.Add(country, capitol);
                }
            }
            return loadedData;
        }
    }
}
