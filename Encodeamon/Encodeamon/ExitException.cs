using System;

namespace Encodeamon
{
    public class ExitException : Exception
    {
        private readonly int _exitCode;

        public ExitException(string message, int exitCode = 0)
            : this(message, null, exitCode)
        {
        }

        public ExitException(string message, Exception innerException, int exitCode)
            : base(message, innerException)
        {
            _exitCode = exitCode;
        }

        public int ExitCode { get { return _exitCode; } }
    }
}