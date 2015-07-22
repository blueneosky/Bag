using System;
using System.Collections.Generic;
using System.Linq;

namespace SkyrimUserSwitch.Common
{
    public class SkusException : Exception
    {
        public SkusException()
        {
        }

        public SkusException(string message)
            : base(message)
        {
        }

        public SkusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}