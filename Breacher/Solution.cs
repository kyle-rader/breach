using System.Collections.Generic;
using System.Linq;

namespace Breacher
{
    public class Solution
    {
        private const int SQUISH_RESULT_CAPACITY = 25;
        private IEnumerable<Target> _path;
        public int Weight;
        public IEnumerable<int> Chain;

        public Solution(IEnumerable<Target> path)
        {
            SetInputs(path);
        }

        public Solution(IEnumerable<int[]> path)
        {
            IList<Target> result = new List<Target>();
            for (int i = 0; i < path.Count(); i++)
            {
                // Use order (i) as the weight.
                result.Add(new Target(path.ElementAt(i), i));
            }
            SetInputs(result);
        }

        private void SetInputs(IEnumerable<Target> path)
        {
            _path = path;
            Weight = path.Select(p => p.weight).Sum();
            Chain = Squish(path.Select(p => p.values));
        }

        public override string ToString()
        {
            return $"weight: {Weight,1}, cnt: {_path.Count(),1} Set: ({string.Join(", ", _path)}) : {string.Join(' ', Chain)}";
        }

        public static IEnumerable<int> Squish(IEnumerable<int[]> targets)
        {
            List<int> result = new List<int>(SQUISH_RESULT_CAPACITY);
            result.AddRange(targets.First());

            foreach (var target in targets.Skip(1))
            {
                int i_t = 0;
                for (int i_result = 0; i_result < result.Count && i_t < target.Length; i_result++)
                {
                    if (result[i_result] != target[i_t])
                    {
                        // reset the tracker in target.
                        i_t = 0;
                    }
                    else
                    {
                        // Matched, start or keep counting.
                        i_t++;
                    }
                }
                // int_i is either 0, no overlap, or the number of overlapping numbers.
                result.AddRange(target.Skip(i_t));
            }
            return result;
        }
    }
}
