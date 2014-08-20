using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.File
{
    public class WrongFBFileException : FBFileException
    {
        public WrongFBFileException()
            : base()
        {
        }

        public WrongFBFileException(string message)
            : base(message)
        {
        }

        public WrongFBFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}