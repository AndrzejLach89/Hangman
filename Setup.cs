using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

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
            }
        }

        private static CountriesAndCapitols LoadCapitols()
        {
            string path = GameSettings.Settings["countriesPath"];
            var loadedData = new CountriesAndCapitols();
            string[] lines = File.ReadAllLines(path);
            foreach (string i in lines)
            {
                if (i.Contains('|'))
                {
                    string normalized;
                    if (i.IsNormalized(NormalizationForm.FormD))
                    {
                        normalized = i;
                    }
                    else
                    {
                        normalized = NormalizeName(i);
                    }
                    string[] splittedLine = normalized.Split('|');
                    string country = splittedLine[0].Trim(' ');
                    string capitol = splittedLine[1].Trim(' ');
                    loadedData.Add(country, capitol);
                }
            }
            return loadedData;
        }

        private static string NormalizeName(string input)
        {
            // https://stackoverflow.com/questions/2460206/how-to-convert-from-unicode-to-ascii
            return string.Concat(input.Normalize(NormalizationForm.FormD).Where(
            c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }
    }
}
