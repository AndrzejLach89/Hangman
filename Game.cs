using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Game
    {
        private static readonly string _letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string _country;
        private string _capitol;
        private int toGuess;
        private int _roundCounter;
        private int GameTime;
        private Word _guess;

        public int Lives { get; private set; }
        public List<char> DiscardedLetters { get; private set; }

        public DateTime StartTime { get; private set; }
        public string Mask { get; private set; }
        //private char[] MaskedWord;

        public Game()//string[] pickedCountry)
        {
            var pickedCountry = Setup.CapitolsData.PickCountry();
            _country = pickedCountry[0];
            _capitol = pickedCountry[1];
            _guess = new Word(_capitol, Convert.ToChar(GameSettings.Settings["symbol"]));
            _roundCounter = 0;
            toGuess = _capitol.Length;
            Lives = Convert.ToInt32(GameSettings.Settings["lives"]);
            DiscardedLetters = new List<char>();
            Mask = "".PadRight(toGuess, Convert.ToChar(GameSettings.Settings["symbol"]));
            //MaskedWord = 
            StartTime = DateTime.Now;
            NextRound();
        }

        private void NextRound()
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
                PlayRound();
            }
        }

        private string GetLetters()
        {
            string output = "";
            foreach (var i in DiscardedLetters)
            {
                output = output + ", " + i;
            }
            return output;
        }

        private void ShowRoundInfo()
        {
            Console.Clear();
            Console.WriteLine("ROUND {0}\nLives: {1}\nUsed letters: {2}", _roundCounter, Lives, GetLetters());
            Console.WriteLine(Mask);
        }

        private void PlayRound()
        {
            ShowRoundInfo();
            Console.WriteLine("What do you want to guess?\n(L)ETTER\n(W)ORD\n(Q)uit");
            char[] options = { 'l', 'w', 'q' };
            char input;
            do
            {
                input = Console.ReadKey().KeyChar;
            }
            while (!options.Contains(input));
            //while (Array.FindIndex(options, input) == -1);
            input = Char.ToLower(input);
            switch (Char.ToLower(input))
            {
                case 'l':
                    GuessLetter();
                    break;
                case 'w':
                    GuessWord();
                    break;
                case 'q':
                    EndGame(0);
                    break;
            }
        }

        private void GuessLetter()
        {
            bool validChar = false;
            char input;
            do
            {
                ShowRoundInfo();
                Console.Write("\nEnter letter: ");
                input = Char.ToUpper(Console.ReadKey().KeyChar);
                if (!Char.IsLetter(input))
                {
                    Console.WriteLine("Please use only alphabet letters.\nPress any key to continue.");
                    validChar = false;
                    Console.ReadKey();
                }
                else
                {
                    validChar = ValidateInput(input);
                    if (!validChar)
                    {
                        Console.WriteLine("You have already used letter {0}!\nPress any key to continue.", input);
                        Console.ReadKey();
                    }
                }
            }
            while (!validChar);
            if (_guess.CheckLetter(input))
            {
                Mask = _guess.UpdateMask(input);
                toGuess = _guess.UpdateCounter();
                ShowRoundInfo();
                Console.WriteLine("Well done!\n");
            }
            else
            {
                DiscardedLetters.Add(input);
                Lives--;
                ShowRoundInfo();
                Console.WriteLine("Oh no, bad guess! :( You lose one life!");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            NextRound();
        }

        private void GuessWord()
        {
            string input;
            bool validInput;
            do
            {
                ShowRoundInfo();
                Console.Write("\nEnter word: ");
                input = Console.ReadLine();
                input = input.ToUpper();
                validInput = ValidateInput(input);
                if (!validInput)
                {
                    Console.WriteLine("Please use only alphabet letters.\nPress any key to continue.");
                    Console.ReadKey();
                }
            }
            while (!validInput);
            if (_guess.CheckWord(input))
            {
                Mask = _guess.UpdateMask(input);
                toGuess = _guess.UpdateCounter();
                ShowRoundInfo();
                Console.WriteLine("Well done!");
            }
            else
            {
                Lives -= 2;
                ShowRoundInfo();
                Console.WriteLine("Oh no, bad guess! :( You lose 2 lives!");
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            NextRound();
        }

        private bool ValidateInput(char i)
        {
            if (DiscardedLetters.Contains(i) || Mask.Contains(i))
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
                if (!Char.IsLetter(i) && !i.Equals(' '))//(!_letters.Contains(i))
                {
                    return false;
                }
            }
            return true;
        }

        private void EndGame(int status)
        {
            var highScores = new HighScores();
            if (status == 0)
            {
                Console.Clear();
                Console.WriteLine("Game over!\nYou lose!");
                Console.ReadKey();
            }
            else if (status == 1)
            {
                GameTime = DateTime.Now.Subtract(StartTime).Seconds;
                int score = CalculateScore();
                Console.Clear();
                Console.WriteLine("Well done! It took you {0} seconds to guess.\nYou scored {1} points!\n{2} is the capitol of {3}.", GameTime, score, _capitol, _country);
                Console.ReadKey();
                highScores.AddScore(Lives, GameTime, score);
            }
            highScores.ShowHighScores(null);
        }

        private int CalculateScore()
        {
            int score = 0;
            int lives = Lives * 10;
            int time;
            if (GameTime == 0)
            {
                score = 999;
                return score;
            }
            else
            {
                time = 1000 / GameTime;
                score = lives + time;
                return score;
            }
        }
    }
}
