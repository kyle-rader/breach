using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Breacher
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var targets = new Target[]
            {
                new Target(new [] { 1, 1, 2 }, 1),
                new Target(new [] { 1, 1, 2 }, 2),
                new Target(new [] { 1, 1, 1, 1 }, 3),
            };

            HashSet<Attempt> tried = new HashSet<Attempt>();

            foreach (var path in targets.GetAllCombinationsAndPermutations())
            {
                var attempt = new Attempt(path);
                if (tried.Add(attempt))
                    Console.WriteLine(attempt);
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine($"Finished in {ts.TotalMilliseconds:F2} ms");
        }
    }
}
