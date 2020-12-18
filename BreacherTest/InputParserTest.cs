using Breacher;

using FluentAssertions;

using NUnit.Framework;

namespace BreacherTest
{
    public class InputParserTest
    {
        [Test]
        public void Parse_The_Matrix_3x3()
        {
            var input = @"
1c 55 bd
7a 1c e9
e9 55 1c";

            InputParser subject = new InputParser();
            subject.ParseMatrix(input).Should().BeEquivalentTo(new[,]
            {
                {  28, 85, 189 },
                { 122, 28, 233 },
                { 233, 85,  28 },
            });
        }

        [Test]
        public void Parse_The_Matrix_5x5()
        {
            var input = @"
01 02 03 04 05
06 07 08 09 0a
0b 0c 0d 0e 0f
10 11 12 13 14
15 16 17 18 19";

            InputParser subject = new InputParser();
            subject.ParseMatrix(input).Should().BeEquivalentTo(new[,]
            {
                { 1,  2,  3,  4,  5 },
                { 6,  7,  8,  9,  10 },
                { 11, 12, 13, 14, 15 },
                { 16, 17, 18, 19, 20 },
                { 21, 22, 23, 24, 25 },
            });
        }
    }
}
