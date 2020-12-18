using Breacher;

using FluentAssertions;

using NUnit.Framework;

namespace BreacherTest
{
    public class InputParserTest
    {
        [Test]
        public void Parse_The_Matrix()
        {
            var input = @"1c 55 bd
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
    }
}
