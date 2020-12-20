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

            Stopwatch stopWatch = new Stopwatch();

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
            }

            // Split input on double newlines (separated by a single blank line).
            string[] inputSplit = input.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            /*
             * Buffer size defaults to 4.
             * Then override with the env var
             * Final override with actual input
             */
            int bufferSize = DEFAULT_BUFFER_SIZE;
            string bufferEnvVar = Environment.GetEnvironmentVariable(BREACH_BUFFER_ENV_VAR);
            if (!string.IsNullOrWhiteSpace(bufferEnvVar) && int.TryParse(bufferEnvVar, out bufferSize))
            {
                ConsoleHelper.DarkGray($"environment variable {BREACH_BUFFER_ENV_VAR} is set to {bufferSize}\n");
            }

            int inputOffset = 0;
            if (inputSplit.Length == 3)
            {
                inputOffset = 1;
                int.TryParse(inputSplit[0].Trim(), out bufferSize);
            }

            int[,] puzzle = InputParser.ParseMatrix(inputSplit[0 + inputOffset]);
            PuzzleMatrix matrix = new PuzzleMatrix(puzzle);
            Target[] targets = InputParser.ParseTargets(inputSplit[1 + inputOffset]).ToArray();

            Console.WriteLine($"Solving for buffer size: {bufferSize} ...");
            bool notFound = true;
            foreach (var attempt in GenerateAttempts(targets).OrderBy(a => -a.Weight))
            {
                if (matrix.Check(attempt.Chain, bufferSize, out var path))
                {
                    notFound = false;
                    Console.WriteLine($"Found solution with weight: {attempt.Weight} & length: {path.Count}");
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
            Console.WriteLine($"Took {ts.TotalMilliseconds:F2} ms");

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
