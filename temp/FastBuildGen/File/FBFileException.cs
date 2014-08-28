using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.File
{
    public class FBFileException : FastBuildGenException
    {
        public FBFileException()
            : base()
        {
        }

        public FBFileException(string message)
            : base(message)
        {
        }

        public FBFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}