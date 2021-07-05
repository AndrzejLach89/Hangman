using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Word
    {
        public string word { get; private set; }
        char MaskChar;
        char[] WordChars;
        char[] WordMask;

        public Word(string word, char maskChar)
        {
            this.word = word;
            MaskChar = maskChar;
            WordChars = this.word.ToUpper().ToCharArray(0, this.word.Length);
            WordMask = new char[this.word.Length];
            for (int i = 0; i < this.word.Length; i++)
            {
                WordMask[i] = MaskChar;
            }
        }

        public string UpdateMask(char x)
        {
            Console.WriteLine("char {0}; word: {1}, wordchars: {2}, wordmask: {3}", x, word, WordChars, WordMask);
            int index = 0;
            foreach (char i in WordChars)
            {
                if (Char.ToUpper(i).Equals(Char.ToUpper(x)))
                {
                    WordMask[index] = word[index];
                }
                index++;
            }
            return new string(WordMask);
        }

        public string UpdateMask(string x)
        {
            int index = 0;
            foreach (char i in x)
            {
                if (WordChars[index].Equals(i))
                {
                    WordMask[index] = word[index];
                }
                index++;
            }
            return new string(WordMask);
        }

        public bool CheckLetter(char letter)
        {
            Console.WriteLine("char {0}; word: {1}, wordchars: {2}, wordmask: {3}", letter, word, WordChars.ToString(), WordMask.ToString());
            foreach (var i in WordChars) { Console.Write(i); }
            Console.ReadKey();
            if (word.ToUpper().Contains(Char.ToUpper(letter)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckWord(string w)
        {
            if (w.Equals(this.word.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int UpdateCounter()
        {
            int cnt = 0;
            foreach (char i in WordMask)
            {
                if (i == MaskChar)
                {
                    cnt++;
                }
            }
            return cnt;
        }
    }
}
