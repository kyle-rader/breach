using System;

namespace Breacher
{
    public static class ConsoleHelper
    {
        public static void WriteColor(string input, ConsoleColor color)
        {
            ConsoleColor original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(input);
            Console.ForegroundColor = original;
        }

        public static void Green(string input)
        {
            WriteColor(input, ConsoleColor.Green);
        }

        public static void DarkGray(string input)
        {
            WriteColor(input, ConsoleColor.DarkGray);
        }

        internal static void Red(string input)
        {
            WriteColor(input, ConsoleColor.Red);
        }
    }
}
