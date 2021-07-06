using System.Collections.Generic;
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
                    string[] splittedLine = i.Split(splitChar);
                    string key = splittedLine[0].Trim(' ');
                    string value = splittedLine[1].Trim(' ');
                    loadedData.Add(key, value);
                }
            }
            return loadedData;
        }

        public static List<string[]> ReadScores()
        {
            var loadedData = new List<string[]>();
            if (File.Exists("./HighScores"))
            {
                string[] lines = File.ReadAllLines("./HighScores");
                foreach (string i in lines)
                {
                    if (i.Contains('|'))
                    {
                        string[] splittedLine = i.Split('|');
                        int cnt = 0;
                        foreach (string j in splittedLine)
                        {
                            splittedLine[cnt] = j.Trim(' ');
                            cnt++;
                        }
                        loadedData.Add(splittedLine);
                    }
                }
            }
            return loadedData;
        }
    }
}
