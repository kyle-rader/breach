using Breacher;

using FluentAssertions;

using NUnit.Framework;

using System.Collections.Generic;

namespace BreacherTest
{
    public class PuzzleMatrixTest
    {
        [Test]
        public void ItCanCheckASolution()
        {
            Attempt attempt = new Attempt(new List<int[]>
            {
                new [] {1,1,4},
            });

            PuzzleMatrix matrix = new PuzzleMatrix(3, new int[,]
            {
                { 1, 2, 1, 3 },
                { 4, 1, 1, 4 },
                { 2, 4, 4, 4 },
                { 3, 3, 2, 1 },
            });

            //matrix.Check(attempt).Should().BeTrue();
        }
    }
}
