using System;


namespace Hangman
{
    static class Utilities
    {
        public static void WriteCommandKeys(string text)
        {
            foreach (char i in text)
            {
                if (i.Equals('('))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (i.Equals(')'))
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(i);
                }
            }
        }

        public static void WriteCommandKeys(string text, int offset)
        {
            if (text.Contains('(') && offset > 0)
            {
                Console.Write("".PadLeft(offset, ' '));
            }
            foreach (char i in text)
            {
                if (i.Equals('('))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (i.Equals(')'))
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(i);
                }
            }
        }
    }
}
