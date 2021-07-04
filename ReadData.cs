using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    static class ReadData
    {
        public static Dictionary<string, string> ReadLines(string path, char splitChar)
        {
            var loadedData = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(path);
            foreach (string i in lines)
            {
                if (i.Contains(splitChar))
                {
                    string[] splittedLine = i.Split('|');
                    string key = splittedLine[0].Trim(' ');
                    string value = splittedLine[1].Trim(' ');
                    loadedData.Add(key, value);
                }
            }
            return loadedData;
        }
    }
}
