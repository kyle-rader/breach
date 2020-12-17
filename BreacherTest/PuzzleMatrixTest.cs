using Breacher;

using FluentAssertions;

using NUnit.Framework;

using System.Collections.Generic;

namespace BreacherTest
{
    public class PuzzleMatrixTest
    {
        readonly int[,] Board = new int[,]
        {
            {  1,  2,  3,  4 },
            {  5,  6,  7,  8 },
            {  9, 10, 11, 12 },
            { 13, 14, 15, 16 },
        };

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void True_With_Direct_Path_And_Enough_Buffer(int bufferSize)
        {
            var chain = new[] { 2, 10, 11 };
            PuzzleMatrix subject = new PuzzleMatrix(Board);

            subject.Check(chain, bufferSize, out var path).Should().BeTrue();
            path.Should().BeEquivalentTo(new[]
            {
                (0,1),
                (2,1),
                (2,2),
            });
        }

        [Test]
        public void False_When_Not_Enough_Buffer()
        {
            var chain = new[] { 2, 10, 12 };
            PuzzleMatrix subject = new PuzzleMatrix(Board);
            subject.Check(chain, 2, out var path).Should().BeFalse();
            path.Should().BeEmpty();
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(8)]
        public void False_When_Path_Is_Impossible_No_Matter_The_Buffer(int bufferSize)
        {
            var chain = new[] { 1, 2, 3, 4 };
            var subject = new PuzzleMatrix(Board);
            subject.Check(chain, bufferSize, out var path).Should().BeFalse();
            path.Should().BeEmpty();
        }

        [TestCase(4)]
        [TestCase(5)]
        public void True_When_Burning_Is_Required(int bufferSize)
        {
            var chain = new[] { 5, 7, 15 };
            var subject = new PuzzleMatrix(Board);
            subject.Check(chain, bufferSize, out var path).Should().BeTrue();
            path.Should().BeEquivalentTo(new[]
            {
                (0,0),
                (1,0),
                (1,2),
                (3,2),
            });
        }
    }
}
