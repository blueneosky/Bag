using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public class FastBuildGenException : Exception
    {
        public FastBuildGenException()
            : base()
        {
        }

        public FastBuildGenException(string message)
            : base(message)
        {
        }

        public FastBuildGenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}