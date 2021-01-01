using Breach;

using FluentAssertions;

using NUnit.Framework;

using System.Collections.Generic;

namespace BreachTest
{
    public class AttemptTest
    {
        public Attempt Subject(IEnumerable<int[]> input) => new Attempt(input);

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

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
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

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
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

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
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

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_5_PartDuplicate()
        {
            var input = new List<int[]>
            {
                new [] {1,1},
                new [] {1,1,2},
            };

            var expected = new[] { 1, 1, 2 };

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_6_FullDuplicate()
        {
            var input = new List<int[]>
            {
                new [] {1,1,2},
                new [] {1,1,2},
            };

            var expected = new[] { 1, 1, 2 };

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_7_FullDuplicatePlusExtra()
        {
            var input = new List<int[]>
            {
                new [] {1,1,1,1},
                new [] {1,1,2},
                new [] {1,1,2},
            };

            var expected = new[] { 1, 1, 1, 1, 2 };

            Attempt.Squish(input).Should().BeEquivalentTo(expected);
        }
    }
}