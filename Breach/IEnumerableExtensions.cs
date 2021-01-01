using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breach
{
    static class EnumerableExtensions
    {
        private static void SetIndexes(int[] indexes, int lastIndex, int count)
        {
            indexes[lastIndex]++;
            if (lastIndex > 0 && indexes[lastIndex] == count)
            {
                SetIndexes(indexes, lastIndex - 1, count - 1);
                indexes[lastIndex] = indexes[lastIndex - 1] + 1;
            }
        }

        private static bool AllPlacesChecked(int[] indexes, int places)
        {
            for (int i = indexes.Length - 1; i >= 0; i--)
            {
                if (indexes[i] != places)
                    return false;
                places--;
            }
            return true;
        }

        public static IEnumerable<IEnumerable<T>> GetDifferentCombinations<T>(this IEnumerable<T> c, int count)
        {
            var collection = c.ToList();
            int listCount = collection.Count();

            if (count > listCount)
                throw new InvalidOperationException($"{nameof(count)} is greater than the collection elements.");

            int[] indexes = Enumerable.Range(0, count).ToArray();

            do
            {
                yield return indexes.Select(i => collection[i]).ToList();

                SetIndexes(indexes, indexes.Length - 1, listCount);
            }
            while (!AllPlacesChecked(indexes, listCount));
        }

        public static IEnumerable<IEnumerable<T>> GetAllCombinations<T>(this IEnumerable<T> c)
        {
            for (int i = c.Count(); i > 0; i--)
            {
                foreach (var j in c.GetDifferentCombinations(i)) yield return j;
            }
        }

        public static IEnumerable<IEnumerable<T>> GetAllCombinationsAndPermutations<T>(this IEnumerable<T> c)
        {
            for (int i = c.Count(); i > 0; i--)
            {
                foreach (var j in c.GetDifferentCombinations(i))
                {
                    foreach (var p in j.Permute())
                    {
                        yield return p;
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> set, IEnumerable<T> subset = null)
        {
            if (subset == null) subset = new T[] { };
            if (!set.Any()) yield return subset;

            for (var i = 0; i < set.Count(); i++)
            {
                var newSubset = set.Take(i).Concat(set.Skip(i + 1));
                foreach (var permutation in Permute(newSubset, subset.Concat(set.Skip(i).Take(1))))
                {
                    yield return permutation;
                }
            }
        }
    }
}
