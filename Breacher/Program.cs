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
        public const string BREACH_BUFFER_ENV_VAR = "BREACH_BUFFER";
        public const int DEFAULT_BUFFER_SIZE = 4;
        public const string Help = @"Usage:
See https://github.com/kyle-rader/breacher/blob/main/README.md for full usage.

You can enter input manually:
  breach

You can pass an input file:
  breach INPUT_FILENAME

Or you can pipe through stdin:

  windows:
    type puzzle.txt | breach

  osx or linux:
    cat puzzle.txt | breach
";

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            string version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            string title = $"breach version: {version}";

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
                        Console.WriteLine(title);
                        Console.WriteLine(Help);
                        Environment.Exit(0);
                        break;
                    case "-v":
                    case "--version":
                        Console.WriteLine(version);
                        Environment.Exit(0);
                        break;
                    default:
                        filename = args[0];
                        break;
                }
            }

            Console.WriteLine(title);

            /*
             * Buffer size defaults to 4.
             * Then override with the env var
             * Final override with actual input
             */
            int bufferSize = DEFAULT_BUFFER_SIZE;
            string bufferEnvVar = Environment.GetEnvironmentVariable(BREACH_BUFFER_ENV_VAR);
            if (!string.IsNullOrWhiteSpace(bufferEnvVar) && int.TryParse(bufferEnvVar, out bufferSize))
            {
                ConsoleHelper.DarkGray("Env Var ");
                ConsoleHelper.Green(BREACH_BUFFER_ENV_VAR);
                ConsoleHelper.DarkGray(" is set to ");
                ConsoleHelper.Green($"{bufferSize}\n\n");
            }
            else
            {
                ConsoleHelper.DarkGray($"buffer size env var {BREACH_BUFFER_ENV_VAR} is not set. Set it to your buffer size to skip manual entry of the buffer size.");
            }

            string input = null;
            if (!string.IsNullOrEmpty(filename))
            {
                if (File.Exists(filename))
                {
                    stopWatch.Start();
                    Console.WriteLine($"Reading input from: {filename}");
                    input = File.ReadAllText(filename);
                }
                else
                {
                    ConsoleHelper.Red($"Input file '{filename}' does not exist.");
                    Environment.Exit(1);
                }
            }
            else if (Console.IsInputRedirected)
            {
                stopWatch.Start();
                Console.WriteLine("Reading input from: standard input");
                input = Console.In.ReadToEnd();
            }
            else
            {
                Console.WriteLine("Waiting for manual puzzle entry.");
                Console.WriteLine("End your input with a single q at the end to finish entry and begin solving.");
                StringBuilder sb = new StringBuilder();
                char c;
                while ((c = (char)Console.Read()) != 'q')
                {
                    sb.Append(c);
                }
                stopWatch.Start();
                input = sb.ToString();
                Console.WriteLine();
            }

            // Split input on double newlines (separated by a single blank line).
            string[] inputSplit = input.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            int inputOffset = 0;
            if (inputSplit.Length == 3)
            {
                inputOffset = 1;
                if (int.TryParse(inputSplit[0].Trim(), out bufferSize))
                {
                    ConsoleHelper.DarkGray("Overriding ");
                    ConsoleHelper.Green(BREACH_BUFFER_ENV_VAR);
                    ConsoleHelper.DarkGray(" with input value ");
                    ConsoleHelper.Green($"{bufferSize}\n");
                }
            }

            int[,] puzzle = InputParser.ParseMatrix(inputSplit[0 + inputOffset]);
            PuzzleMatrix matrix = new PuzzleMatrix(puzzle);
            Target[] targets = InputParser.ParseTargets(inputSplit[1 + inputOffset]).ToArray();

            Console.Write($"\nSolving for buffer size: ");
            ConsoleHelper.Green($"{bufferSize}\n");
            bool notFound = true;
            foreach (var attempt in GenerateAttempts(targets).OrderBy(a => -a.Weight).ThenBy(a => a.Length))
            {
                if (matrix.Check(attempt.Chain, bufferSize, out var path))
                {
                    notFound = false;
                    Console.Write("Best Solution: ");
                    ConsoleHelper.DarkYellow("weight "); ConsoleHelper.Green($"{attempt.Weight} ");
                    ConsoleHelper.DarkYellow("length "); ConsoleHelper.Green($"{path.Count}\n");
                    matrix.Print(path.Reverse());
                    break;
                }
            }

            stopWatch.Stop();

            if (notFound)
            {
                ConsoleHelper.Red("Oh dear... There is no solution to this puzzle :(\nExit the interface and re-connect to get a different puzzle input.\n");
            }

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Finished in {ts.TotalMilliseconds:F2} ms (aka {ts.TotalSeconds:F2} seconds)");

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
