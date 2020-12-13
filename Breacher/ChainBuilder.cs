using System.Collections.Generic;
using System.Linq;

namespace Breacher
{
    public class ChainBuilder
    {
        private const int SQUISH_RESULT_CAPACITY = 25;
        public static IEnumerable<int> Squish(IEnumerable<int[]> targets)
        {
            List<int> result = new List<int>(SQUISH_RESULT_CAPACITY);
            result.AddRange(targets.First());

            foreach (var target in targets.Skip(1))
            {
                int i_t = 0;
                for (int i_result = 0; i_result < result.Count; i_result++)
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
