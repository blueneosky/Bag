using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UI
{
    public class UIException : Exception
    {
        public UIException()
            : base()
        {
        }

        public UIException(string message)
            : base(message)
        {
        }

        public UIException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}