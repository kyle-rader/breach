using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Breacher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your puzzle input (or pipe from stdin):");

            string bufferInput = ReadLinesUntilDoubleNewline();
            string puzzleInput = ReadLinesUntilDoubleNewline();
            string targetsInput = ReadLinesUntilDoubleNewline();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            int[,] puzzle = InputParser.ParseMatrix(puzzleInput);
            PuzzleMatrix matrix = new PuzzleMatrix(puzzle);
            Target[] targets = InputParser.ParseTargets(targetsInput).ToArray();
            int bufferSize = int.Parse(bufferInput.Trim());

            foreach (var attempt in GenerateAttempts(targets).OrderBy(a => -a.Weight))
            {
                if (matrix.Check(attempt.Chain, bufferSize, out var path))
                {
                    Console.WriteLine($"Found solution with weight {attempt.Weight} length: {path.Count}");
                    foreach (var step in path.Reverse())
                    {
                        Console.WriteLine($" [{step.row}, {step.col} ] ({puzzle[step.row, step.col]:X})");
                    }
                    break;
                }
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Solved in {ts.TotalMilliseconds:F2} ms");
        }

        static IEnumerable<Attempt> GenerateAttempts(Target[] targets)
        {
            HashSet<Attempt> tried = new HashSet<Attempt>();

            foreach (var path in targets.GetAllCombinationsAndPermutations())
            {
                var attempt = new Attempt(path);
                if (tried.Add(attempt))
                    yield return attempt;
            }
        }

        static string ReadLinesUntilDoubleNewline()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                sb.Append(line);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
