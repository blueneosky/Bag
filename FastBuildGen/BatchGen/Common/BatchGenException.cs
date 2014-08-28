using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.Common
{
    public class BatchGenException : Exception
    {
        public BatchGenException()
            : base()
        {
        }

        public BatchGenException(string message)
            : base(message)
        {
        }

        public BatchGenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}