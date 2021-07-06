using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    class Game
    {
        private string _country;
        private string _capitol;
        private int toGuess;
        private int _roundCounter;
        private int GameTime;
        private Word _guess;

        public int Lives { get; private set; }
        public List<char> NotInWord { get; private set; }

        public DateTime StartTime { get; private set; }
        public string Mask { get; private set; }

        public Game()
        {
            string[] pickedCountry = Setup.CapitolsData.PickCountry();
            _country = pickedCountry[0];
            _capitol = pickedCountry[1];
            _guess = new Word(_capitol, Convert.ToChar(GameSettings.Settings["symbol"]));
            _roundCounter = 0;
            toGuess = _capitol.Length;
            Lives = Convert.ToInt32(GameSettings.Settings["lives"]);
            NotInWord = new List<char>();
            Mask = "".PadRight(toGuess, Convert.ToChar(GameSettings.Settings["symbol"]));
            StartTime = DateTime.Now;
            GodMode();
            NextRound(null);
        }

        private void GodMode()
        {
            Console.Clear();
            Console.WriteLine(_capitol);
            Console.ReadKey();
            Console.Clear();
        }

        private void NextRound(string? message)
        {
            if (Lives <= 0)
            {
                EndGame(0);
            }
            else if (toGuess == 0)
            {
                EndGame(1);
            }
            else
            {
                _roundCounter++;
                if (String.IsNullOrEmpty(message))
                {
                    PlayRound(null);
                }
                else
                {
                    PlayRound(message);
                }
                
            }
        }

        private string GetLetters()
        {
            string output = "";
            foreach (var i in NotInWord)
            {
                output = output + i + " ";
            }
            return output;
        }

        private void ShowRoundInfo_old()
        {
            Console.Clear();
            Console.WriteLine("ROUND {0}\nLives: {1}\nUsed letters: {2}", _roundCounter, Lives, GetLetters());
            if (Lives <= 1)
            {
                Console.WriteLine("HINT: Capitol of {0}", _country);
            }
            Console.WriteLine(Mask);
        }

        private void ShowRoundInfo()
        {
            Console.Clear();
            int n = GameSettings.WindowWidth;
            int n2 = n / 2;
            string round = "R O U N D   " + _roundCounter.ToString() + "";
            string lives = "Lives: " + Lives.ToString();
            string letters = "Used letters: " + GetLetters();
            string hint = "HINT: Capitol of " + _country;
            

            round = "".PadRight(n, '=') + "\n" + round.PadLeft(n/2 + round.Length/2, ' ').PadRight(n, ' ') + "\n" + "".PadRight(n, '=');
            //lives = lives.PadLeft(n / 2 + lives.Length / 2, ' ').PadRight(n, ' ');
            //letters = letters.PadLeft(n / 2 + letters.Length / 2, ' ').PadRight(n, ' ');
            //hint = hint.PadLeft(n / 2 + hint.Length / 2, ' ').PadRight(n, ' ');
            //string mask = Mask.PadLeft(n / 2 + Mask.Length / 2, ' ').PadRight(n, ' ');

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(round);
            Console.ResetColor();
            Console.WriteLine(lives);
            Console.WriteLine(letters);
            if (Lives <= 1)
            {
                Console.WriteLine(hint);
            }
            Console.WriteLine(Mask);
            //Console.WriteLine("ROUND {0}\nLives: {1}\nUsed letters: {2}", _roundCounter, Lives, GetLetters());
            //if (Lives <= 1)
            //{
            //    Console.WriteLine("HINT: Capitol of {0}", _country);
            //}
            //Console.WriteLine(Mask);
        }

        private void PlayRound(string? message)
        {
            ShowRoundInfo();
            if (!String.IsNullOrEmpty(message))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            Console.WriteLine("What do you want to do?\n(L)ETTER\n(W)ORD\n(Q)UIT");
            char[] options = { 'l', 'w', 'q' };
            char input;
            do
            {
                input = Console.ReadKey().KeyChar;
            }
            while (!options.Contains(input));
            input = Char.ToLower(input);
            switch (input)
            {
                case 'l':
                    GuessLetter(null);
                    break;
                case 'w':
                    GuessWord(null);
                    break;
                case 'q':
                    EndGame(0);
                    break;
                default:
                    PlayRound(message);
                    break;
            }
        }

        private void GuessLetter(string? message)
        {
            bool validChar = false;
            char input;
            ConsoleKeyInfo rawInput;
            do
            {
                ShowRoundInfo();
                if (!string.IsNullOrEmpty(message))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
                Console.Write("\nEnter letter: ");
                rawInput = Console.ReadKey();
                if (rawInput.Key == ConsoleKey.Escape)
                {
                    PlayRound(null);
                    return;
                }
                input = Char.ToUpper(rawInput.KeyChar);
                if (!Char.IsLetter(input) && input !=(' '))
                {

                    GuessLetter("Please use alphabet letters and whitespaces only.\nPress any key to continue.");
                    return;
                }
                else
                {
                    validChar = ValidateInput(input);
                    if (!validChar)
                    {
                        string msg = "You have already used letter " + input + "!";
                        GuessLetter(msg);
                        return;
                    }
                }
            }
            while (!validChar);
            string result;
            if (_guess.CheckLetter(input))
            {
                Mask = _guess.UpdateMask(input);
                toGuess = _guess.UpdateCounter();
                result = "Well done!";
            }
            else
            {
                NotInWord.Add(input);
                Lives--;
                result = "Oh no! You lose one life! :(";
            }
            NextRound(result);
        }

        private void GuessWord(string? message)
        {
            string input;
            bool validInput;
            do
            {
                ShowRoundInfo();
                if (!String.IsNullOrEmpty(message))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
                Console.Write("\nEnter word: ");
                input = Console.ReadLine();
                input = input.ToUpper();
                validInput = ValidateInput(input);
                if (!validInput)
                {
                    GuessWord("Please use alphabet letters and whitespaces only.");
                    return;
                }
            }
            while (!validInput);
            string result;
            if (_guess.CheckWord(input))
            {
                Mask = _guess.UpdateMask(input);
                toGuess = _guess.UpdateCounter();
                result = "Well done!";
            }
            else
            {
                Lives -= 2;
                result = "Oh no! You lose 2 lives! ;(";
            }
            NextRound(result);
        }

        private bool ValidateInput(char i)
        {
            if (NotInWord.Contains(i) || Mask.ToUpper().Contains(i))
            {
                return false;
            }
            else { return true; }
        }

        private bool ValidateInput(string s)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                return false;
            }
            foreach (char i in s)
            {
                if (!Char.IsLetter(i) && !i.Equals(' '))
                {
                    return false;
                }
            }
            return true;
        }

        private void EndGame(int status)
        {
            var highScores = new HighScores();
            int width = GameSettings.WindowWidth;
            if (status == 0)
            {
                string header = "G A M E   O V E R";
                string msg = "You lose! The capitol of " + _country + " is " + _capitol + "!";
                header = header.PadLeft(width / 2 + header.Length / 2, ' ');

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("".PadRight(width, '='));
                Console.WriteLine(header);
                Console.WriteLine("".PadLeft(width, '=') + "\n");
                Console.WriteLine(msg.PadLeft(width / 2 + msg.Length / 2, ' '));
                Console.ResetColor();
                Console.ReadKey();

                highScores.ShowHighScores(null);
            }
            else if (status == 1)
            {
                GameTime = DateTime.Now.Subtract(StartTime).Seconds;
                int score = CalculateScore();
                string header = "Y O U   W O N !";
                string msg = "Well done! The capitol of " + _country + " is " + _capitol + "!";
                string msg2 = "It took you " + GameTime.ToString() + " seconds to guess and you scored " + score.ToString() + " points!";
                header = header.PadLeft(width / 2 + header.Length / 2, ' ');

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("".PadRight(width, '='));
                Console.WriteLine(header);
                Console.WriteLine("".PadRight(width, '=') + "\n");
                Console.WriteLine(msg.PadLeft(width / 2 + msg.Length / 2, ' '));
                Console.WriteLine(msg2.PadLeft(width / 2 + msg2.Length / 2, ' '));
                Console.ResetColor();
                Console.ReadKey();
                
                highScores.AddScore(Lives, GameTime, _roundCounter, score);
            }
        }

        private int CalculateScore()
        {
            int score = 0;
            int lives = Lives * 30;
            int tries = 500 / _roundCounter;
            int time;
            if (GameTime == 0)
            {
                score = 999;
                return score;
            }
            else
            {
                time = 1000 / GameTime;
                score = lives + time + tries;
                return score;
            }
        }
    }
}
