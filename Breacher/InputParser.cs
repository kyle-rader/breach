using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Breacher
{
    public class InputParser
    {
        public static int[,] ParseMatrix(string input)
        {
            var parts = input.Split(new[] { "\r\n", "\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            int n = (int)Math.Sqrt(parts.Length);

            if (n*n != parts.Length)
            {
                throw new FormatException("The puzzle input was not a square matrix.");
            }

            int[,] m = new int[n, n];

            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var value))
                {
                    throw new HexParsingException($"Cannot parse {parts[i]} as hex number.");
                }
                m[i / n, i % n] = value;
            }
            return m;
        }

        public static IEnumerable<Target> ParseTargets(string input)
        {
            IList<Target> targets = new List<Target>();
            int weight = 1;
            foreach (var line in input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    int[] values = line
                        .Split(' ')
                        .Select(num => int.Parse(num, NumberStyles.HexNumber))
                        .ToArray();

                    targets.Add(new Target(values, weight));
                    weight += weight;
                }
                catch (FormatException)
                {
                    throw new HexParsingException($"An item in {line} could not be parsed as hex.");
                }
            }
            return targets;
        }
    }
}
