using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Util
{
    public class IHException : Exception
    {
        public IHException()
            : base()
        {
        }

        public IHException(string message)
            : base(message)
        {
        }

        public IHException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}