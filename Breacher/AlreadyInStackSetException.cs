using System;

namespace Breacher
{
    public class AlreadyInStackSetException : Exception
    {
        public AlreadyInStackSetException(string message) : base(message) { }
    }
}
