using System;

namespace Breacher
{
    public class InputParser
    {
        public int[,] ParseMatrix(string input)
        {
            var parts = input.Split(new[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            int n = (int)Math.Sqrt(parts.Length);
            int[,] m = new int[n, n];
            for (int i = 0; i < parts.Length; i++)
            {
                m[i / n, i % n] = int.Parse(parts[i], System.Globalization.NumberStyles.HexNumber);
            }
            return m;
        }
    }
}
