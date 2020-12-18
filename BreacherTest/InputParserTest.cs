using Breacher;

using FluentAssertions;

using NUnit.Framework;

using System;

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

            InputParser.ParseMatrix(input).Should().BeEquivalentTo(new[,]
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

            InputParser.ParseMatrix(input).Should().BeEquivalentTo(new[,]
            {
                { 1,  2,  3,  4,  5 },
                { 6,  7,  8,  9,  10 },
                { 11, 12, 13, 14, 15 },
                { 16, 17, 18, 19, 20 },
                { 21, 22, 23, 24, 25 },
            });
        }

        [Test]
        public void Throws_Parsing_Error_If_Non_Hex()
        {
            var input = "zz 0a\n7a ff";
            Action subject = () => InputParser.ParseMatrix(input);
            subject.Should().Throw<HexParsingException>();
        }

        [Test]
        public void Parse_The_Targets()
        {
            var input = @"
01 02
ff 0a ff";
            InputParser.ParseTargets(input).Should().BeEquivalentTo(new[]
            {
                new Target(new [] {1, 2}, 1),
                new Target(new [] {255, 10, 255}, 2),
            });
        }

        [Test]
        public void Parse_The_Targets_Throws()
        {
            var input = "01 ff zz";
            Action subject = () => InputParser.ParseTargets(input);
            subject.Should().Throw<HexParsingException>();
        }
    }
}
