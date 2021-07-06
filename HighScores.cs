using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace Hangman
{
    class HighScores
    {
        private static readonly string _path = "./HighScores";
        private List<PlayerScore> Scores;
        public HighScores()
        {
            Scores = LoadHighScores();
        }

        private List<PlayerScore> LoadHighScores()
        {
            var loadedData = new List<PlayerScore>();
            List<string[]> rawData = ReadData.ReadScores();
            foreach (string[] i in rawData)
            {
                string name = i[0];
                int lives = Convert.ToInt32(i[1]);
                int gameTime = Convert.ToInt32(i[2]);
                int points = Convert.ToInt32(i[3]);
                int rounds = Convert.ToInt32(i[4]);
                string id = i[5];
                loadedData.Add(new PlayerScore(name, lives, gameTime, points, rounds, id));
            }
            return loadedData;
        }

        private void SaveHighScores()
        {
            if (Scores.Count > 10)
            {
                Scores.RemoveRange(9, Scores.Count-10);
            }
            var output = new StringBuilder();
            foreach (PlayerScore i in Scores)
            {
                output.AppendLine(i.ToString());
            }
            File.WriteAllText(_path, output.ToString());
        }

        public void AddScore(int lives, int gameTime, int rounds, int points)
        {
            bool valid = false;
            string name;
            do
            {
                string namePrompt = "Enter your name: ";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(namePrompt.PadLeft(GameSettings.WindowWidth / 2, ' '));
                Console.ResetColor();
                name = Console.ReadLine();
                if (name.Length > 10)
                {
                    name = name.Substring(0, 10);
                }
                valid = ValidateName(name);
            }
            while (!valid);
            PlayerScore player;
            if (name.ToLower().Equals("nwctim"))
            {
                name = "Wizard Tim";
                lives = 99;
                gameTime = 1;
                points = 999;
                rounds = 1;
                string id = "03.04.1975 00:00";
                player = new PlayerScore(name, lives, gameTime, points, rounds, id);
            }
            else
            {
                player = new PlayerScore(name, lives, gameTime, points, rounds);
            }
            Scores.Add(player);
            RefreshList();
            ShowHighScores(player.Id);
        }

        private bool ValidateName(string name)
        {
            return true;
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

        public void ShowHighScores(string? id)
        {
            RefreshList();
            Console.Clear();
            List<string> lines = new List<string>();
            lines.Add(CreateHeader());
            int position = 1;
            foreach (PlayerScore i in Scores)
            {
                lines.Add(CreateEntry(i, id, position));
                position++;
            }
            int cnt = 0;
            foreach (string i in lines)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    if (i.Contains(id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(i);
                        Console.ResetColor();
                    }
                    else if (cnt == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(i);
                        Console.ResetColor();
                        cnt++;
                    }
                    else
                    {
                        Console.WriteLine(i);
                    }
                }
                else
                {
                    if (cnt == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(i);
                        Console.ResetColor();
                        cnt++;
                    }
                    else
                    {
                        Console.WriteLine(i);
                    }
                }
            }
            string exit = "Press any key to continue...";
            exit = "\n" + exit.PadLeft(GameSettings.WindowWidth / 2 + exit.Length/2, ' ');
            Console.WriteLine(exit);
            Console.ReadKey();
            SaveHighScores();
        }

        private string CreateHeader()
        {
            int n = GameSettings.WindowWidth / 5 - 5;
            int n1 = GameSettings.WindowWidth / 2;
            int n2 = GameSettings.WindowWidth;
            string header = "H I G H   S C O R E S".PadLeft(n1+10, ' ');
            string header2 = "No.".PadRight(5, ' ') + "Name".PadRight(n, ' ') + "Date".PadRight(n, ' ') + "Tries".PadRight(n, ' ') + "Time".PadRight(n, ' ') + "Score".PadRight(5, ' ');
            
            header2 = header2.PadLeft(n1 + header2.Length / 2, ' ');
            header = "".PadRight(n2, '=') + "\n" + header.PadRight(n2, ' ') + "\n" + "".PadRight(n2, '=') + "\n\n" + header2;
            return header;
        }

        private string CreateEntry(PlayerScore player, string? id, int position)
        {
            string output = "";
            int n = GameSettings.WindowWidth / 5 - 5;
            string name = player.Name.PadRight(n, '.');
            string date = player.Id.PadRight(n, '.');
            string tries = player.Rounds.ToString().PadRight(n, '.');
            string gameTime = player.GameTime.ToString().PadRight(n, '.');
            string score = player.Points.ToString().PadRight(5, ' ');

            output = Convert.ToString(position).PadRight(5, ' ') + name + date + tries + gameTime + score;
            output = output.PadLeft(GameSettings.WindowWidth / 2 + output.Length / 2, ' ');
            return output;
        }

        private void RefreshList()
        {
            Scores.Sort();
            Scores.Reverse();
        }
    }
}
