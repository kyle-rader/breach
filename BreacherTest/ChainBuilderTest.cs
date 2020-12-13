using Breacher;

using FluentAssertions;

using NUnit.Framework;

namespace BreacherTest
{
    public class ChainBuilderTest
    {

        [Test]
        public void Squish_Test_1()
        {
            var input = new int[][]
            {
                new [] {1,2},
                new [] {2,5,6},
                new [] {5,6,2},
            };

            var expected = new[] { 1, 2, 5, 6, 2 };

            ChainBuilder.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_2()
        {
            var input = new int[][]
            {
                new [] {1,2},
                new [] {2,5,6,1},
                new [] {1,2,8},
            };

            var expected = new[] { 1, 2, 5, 6, 1, 2, 8 };

            ChainBuilder.Squish(input).Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Squish_Test_3()
        {
            var input = new int[][]
            {
                new [] {1,2},
                new [] {1,2,8},
            };

            var expected = new[] { 1, 2, 8 };

            ChainBuilder.Squish(input).Should().BeEquivalentTo(expected);
        }
    }
}