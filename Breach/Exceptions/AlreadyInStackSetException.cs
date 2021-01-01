using System;

namespace Breach
{
    public class AlreadyInStackSetException : Exception
    {
        public AlreadyInStackSetException(string message) : base(message) { }
    }
}
