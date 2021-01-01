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

        public static void Red(string input)
        {
            WriteColor(input, ConsoleColor.Red);
        }

        public static void Green(string input)
        {
            WriteColor(input, ConsoleColor.Green);
        }

        public static void DarkGray(string input)
        {
            WriteColor(input, ConsoleColor.DarkGray);
        }

        public static void Yellow(string input)
        {
            WriteColor(input, ConsoleColor.Yellow);
        }

        public static void DarkYellow(string input)
        {
            WriteColor(input, ConsoleColor.DarkYellow);
        }
    }
}
