using Breacher;

using FluentAssertions;

using NUnit.Framework;

using System.Collections.Generic;

namespace BreacherTest
{
    public class SolutionTest
    {
        public Solution Subject(IEnumerable<int[]> input) => new Solution(input);

        [Test]
        public void Squish_Test_1()
        {
            IEnumerable<int[]> input = new List<int[]>
            {
                new [] {1,2},
                new [] {2,5,6},
                new [] {5,6,2},
            };

            var expected = new[] { 1, 2, 5, 6, 2 };

            Solution.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_2()
        {
            var input = new List<int[]>
            {
                new [] {1,2},
                new [] {2,5,6,1},
                new [] {1,2,8},
            };

            var expected = new[] { 1, 2, 5, 6, 1, 2, 8 };

            Solution.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_3()
        {
            var input = new List<int[]>
            {
                new [] {1,2},
                new [] {1,2,8},
            };

            var expected = new[] { 1, 2, 8 };

            Solution.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_4()
        {
            var input = new List<int[]>
            {
                new [] {1,2},
                new [] {1,3,2},
                new [] {2,1,3},
            };

            var expected = new[] { 1, 2, 1, 3, 2 };

            Solution.Squish(input).Should().BeEquivalentTo(expected);
        }
    }
}