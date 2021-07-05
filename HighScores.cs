using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Hangman
{
    class HighScores
    {
        private static readonly string _path = "./HighScores.json";
        private List<PlayerScore> Scores;
        public HighScores()
        {
            Scores = LoadHighScores();
        }

        private List<PlayerScore> LoadHighScores()
        {
            List<PlayerScore> rawData;
            var data = new List<PlayerScore>();
            try
            {
                string loaded = File.ReadAllText(_path);
                rawData = JsonSerializer.Deserialize<List<PlayerScore>>(loaded);
            }
            catch
            {
                return data;
            }
            /*foreach (var i in rawData)
            {
                data.Add(new PlayerScore(i[0].ToString(),
                                         Convert.ToInt32(i[1]),
                                         Convert.ToInt32(i[2]),
                                         Convert.ToInt32(i[3]),
                                         i[4].ToString()));
            }*/
            rawData.Sort();
            return rawData;
        }

        private void SaveHighScores()
        {
            if (Scores.Count > 10)
            {
                Scores.RemoveRange(10, Scores.Count);
            }
            var output = new List<object[]>();
            foreach (PlayerScore i in Scores)
            {
                object[] data = { i.Name, i.Lives, i.GameTime, i.Points, i.Id };
                output.Add(data);
            }

            string serialized = JsonSerializer.Serialize(output);
            File.WriteAllText(_path, serialized);
        }

        public void AddScore(int lives, int gameTime, int points)
        {
            Console.Clear();
            bool valid = false;
            string name;
            do
            {
                Console.WriteLine("Congratulations! You scored {0}!", points);
                Console.Write("Enter your name: ");
                name = Console.ReadLine();
                if (name.Length > 10)
                {
                    name = name.Substring(0, 10);
                }
                valid = ValidateName(name);
                Console.WriteLine(valid);
                Console.ReadKey();
            }
            while (!valid);

            //AddPlayer(string name, int lives, int gameTime, int points);
            var player = new PlayerScore(name, lives, gameTime, points);
            RefreshList();
            ShowHighScores(player.Id);
        }

        private bool ValidateName(string name)
        {
            bool valid = true;
            foreach (char i in name)
            {
                if (!Char.IsLetter(i) && !Char.IsNumber(i))
                {
                    valid = false;
                }
            }
            return valid;
        }

        private void AddPlayer(string name, int lives, int gameTime, int points)
        {
            Scores.Add(new PlayerScore(name, lives, gameTime, points));
        }

        public void ShowHighScores(string? id)
        {
            Console.Clear();
            List<string> lines = new List<string>();
            lines.Add(CreateHeader());
            foreach (PlayerScore i in Scores)
            {
                lines.Add(CreateEntry(i, id));
            }
            foreach (string i in lines)
            {
                if (i.Contains('>'))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(i);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            SaveHighScores();
        }

        private string CreateHeader()
        {
            int n1 = GameSettings.WindowWidth / 2;
            int n2 = GameSettings.WindowWidth;
            string header = "HIGH SCORES".PadLeft(n1, '=');
            string header2 = "Name".PadRight(15, ' ') + "Lives".PadRight(8, ' ') + "Time".PadRight(8, ' ') + "Score".PadRight(5, ' ');
            int n = GameSettings.WindowWidth - header2.Length;
            header2 = header2.PadLeft(n / 2, ' ').PadRight(n, ' ');
            header = header.PadRight(n2, '=') + "\n\n" + header2;
            return header;
        }

        private string CreateEntry(PlayerScore player, string? id)
        {
            string output = "";
            if (id == player.Id)
            {
                output = output + ">>";
            }
            output = output + player.Name;
            output = output.PadRight(15, '.') + player.Lives.ToString().PadRight(8, '.') + player.GameTime.ToString().PadRight(8, '.') + player.Points.ToString().PadRight(5, '.');
            int n = GameSettings.WindowWidth - output.Length;
            output = output.PadLeft(n / 2, ' ').PadRight(n, ' ');
            return output;
        }

        private void RefreshList()
        {
            Scores.Sort();
        }
    }
}
