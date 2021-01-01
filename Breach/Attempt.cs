using System.Collections.Generic;
using System.Linq;

namespace Breach
{
    public class Attempt
    {
        private const int SQUISH_RESULT_CAPACITY = 25;
        private IEnumerable<Target> _path;
        public int Weight;
        public int[] Chain;
        public int Length;
        private string _id;

        public Attempt(IEnumerable<Target> path)
        {
            SetInputs(path);
        }

        public Attempt(IEnumerable<int[]> path)
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
            Chain = Squish(path.Select(p => p.values)).ToArray();
            Length = Chain.Count();
            _id = string.Join("", Chain);
        }

        public override string ToString()
        {
            return $"weight: {Weight,1}, cnt: {_path.Count(),1} Set: ({string.Join(", ", _path)}) : {string.Join(' ', Chain)}";
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Attempt)) return false;
            return (obj as Attempt)._id.Equals(_id);
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
                        /* if i_t > 0, we need to back track i_result by that much to restart the tracking
                         * e.g.
                         *
                         * While trying to squish 112 into 1111:
                         *
                         * 1 1 1 1
                         * | | x          When i_t and i_result are at x, we stop, and reset.
                         * 1 1 2          We need to restart at the first position
                         *
                         * So we backtrack i_result by i_t. Leaving the next loop iteration checking:
                         * 1 1 1 1
                         *   | | x
                         *   1 1 2
                         *
                         * Followed by
                         *
                         * 1 1 1 1
                         *     | | ✔
                         *     1 1 2
                        */

                        i_result -= i_t;
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
