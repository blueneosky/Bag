using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VerMgr
{
    public class VMException : Exception
    {
        public VMException()
        {
        }

        public VMException(string message)
            : base(message)
        {
        }

        public VMException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}