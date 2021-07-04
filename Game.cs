using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Game
    {
        private string _country;
        private string _capitol;
        private int toGuess;
        private int _roundCounter;

        public int Lives { get; private set; }
        public List<char> DiscardedLetters { get; private set; }
        
        public DateTime StartTime { get; private set; }
        public string Mask { get; private set; }

        public Game(string[] pickedCountry)
        {
            _country = pickedCountry[0];
            _capitol = pickedCountry[1];
            _roundCounter = 0;
            toGuess = _capitol.Length;
            Lives = Convert.ToInt32(GameSettings.Settings["lives"]);
            DiscardedLetters = new List<char>();
            Mask = "".PadRight(toGuess, Convert.ToChar(GameSettings.Settings["symbol"]));
            StartTime = DateTime.Now;
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
            Console.WriteLine("What do you want to guess?\nL - letter\nW - word\nQ - Quit");
            char[] options = { 'l', 'w', 'q' };
            char input;
            do
            {
                input = Console.ReadKey().KeyChar;
            }
            while (!options.Contains(input));
            //while (Array.FindIndex(options, input) == -1);
            input = Char.ToLower(input);
            switch (input)
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
                input = Console.ReadKey().KeyChar;
                validChar = checkLetter(input, DiscardedLetters);
            }
            while (!validChar);

            bool checkLetter(char i, List<char> list)
            {
                if (list.Contains(i))
                {
                    return false;
                }
                else { return true; }
            }

        }

        private void GuessWord()
        {

        }


        private void EndGame(int status)
        {
            if (status == 0)
            {
                Console.Clear();
                Console.WriteLine("Game over!\nYou lose!");
                Console.ReadKey();
            }
            else if (status == 1)
            {
                var time = DateTime.Now.Subtract(StartTime).Seconds;

                Console.Clear();
                Console.WriteLine("Well done! It took you {0} seconds to guess.\n{1} is the capitol of {2}.", time, _capitol, _country);
                Console.ReadKey();
                //HighScores.Add()
            }
            //HighScores.See();
        }
    }
}
