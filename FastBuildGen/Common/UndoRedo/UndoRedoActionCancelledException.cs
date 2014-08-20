using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    internal class UndoRedoActionCancelledException : FastBuildGenException
    {
        public UndoRedoActionCancelledException()
            : base()
        {
        }

        public UndoRedoActionCancelledException(string message)
            : base(message)
        {
        }

        public UndoRedoActionCancelledException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}