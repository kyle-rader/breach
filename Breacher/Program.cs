using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Breacher
{
    class Program
    {
        public const string Help = @"Usage: breacher
See https://github.com/kyle-rader/breacher/blob/main/README.md for full usage.

You can enter input manually:
  breacher

You can pass an input file:
  breacher INPUT_FILENAME

Or you can pipe through stdin:

  windows:
    type puzzle.txt | breacher

  osx or linux:
    cat puzzle.txt | breacher
";

        static void Main(string[] args)
        {
            string filename = null;
            if (args.Length >= 1)
            {
                switch (args[0].ToLower())
                {
                    case "help":
                    case "--help":
                    case "-h":
                    case "-?":
                    case "/?":
                        Console.WriteLine(Help);
                        Environment.Exit(0);
                        break;
                    case "-v":
                    case "--version":
                        Console.WriteLine(Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
                        Environment.Exit(0);
                        break;
                    default:
                        filename = args[0];
                        break;
                }
            }

            Stopwatch stopWatch = new Stopwatch();

            string input = null;
            if (!string.IsNullOrEmpty(filename))
            {
                if (File.Exists(filename))
                {
                    stopWatch.Start();
                    Console.WriteLine($"Reading input from {filename}");
                    input = File.ReadAllText(filename);
                }
                else
                {
                    Console.Error.WriteLine($"Input file '{filename}' does not exist.");
                    Environment.Exit(1);
                }
            }
            else if (Console.IsInputRedirected)
            {
                stopWatch.Start();
                Console.WriteLine("Reading from standard input");
                input = Console.In.ReadToEnd();
            }
            else
            {
                Console.WriteLine("You have opted to enter your puzzle manually. Enter your full puzzle input now.");
                Console.WriteLine("End your input with a single q at the end to finish entry and begin solving.");
                StringBuilder sb = new StringBuilder();
                char c;
                while ((c = (char)Console.Read()) != 'q')
                {
                    sb.Append(c);
                }
                stopWatch.Start();
                input = sb.ToString();
            }

            // Split input on double newlines (separated by a single blank line).
            string[] inputSplit = input.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (inputSplit.Length != 3)
            {
                Console.Error.WriteLine("We failed to get all 3 input parts (buffer size, puzzle, and targets)");
                Environment.Exit(1);
            }

            int bufferSize = int.Parse(inputSplit[0].Trim());
            int[,] puzzle = InputParser.ParseMatrix(inputSplit[1]);
            PuzzleMatrix matrix = new PuzzleMatrix(puzzle);
            Target[] targets = InputParser.ParseTargets(inputSplit[2]).ToArray();

            foreach (var attempt in GenerateAttempts(targets).OrderBy(a => -a.Weight))
            {
                if (matrix.Check(attempt.Chain, bufferSize, out var path))
                {
                    Console.WriteLine($"Found solution with weight {attempt.Weight} length: {path.Count}");
                    matrix.Print(path.Reverse());
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
    }
}
