using System;

namespace Breach
{
    public class HexParsingException : Exception
    {
        public HexParsingException(string message) : base(message) { }
    }
}
