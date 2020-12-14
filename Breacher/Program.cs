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
                new Target(new [] { 1, 2 }, 1),
                new Target(new [] { 2, 1, 3 }, 2),
                new Target(new [] { 1, 3, 2 }, 3),
            };

            foreach (var path in targets.GetAllCombinationsAndPermutations())
            {
                var solution = new Solution(path);
                Console.WriteLine(solution);
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("RunTime (ms)" + ts.TotalMilliseconds);
        }
    }
}
