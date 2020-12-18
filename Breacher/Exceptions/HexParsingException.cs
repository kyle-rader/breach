using System;

namespace Breacher
{
    public class HexParsingException : Exception
    {
        public HexParsingException(string message) : base(message) { }
    }
}
