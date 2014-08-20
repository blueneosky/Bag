using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    internal class UndoRedoUncatchedException : FastBuildGenException
    {
        public UndoRedoUncatchedException(Exception innerException)
            : base("UndoRedoUncatchedException", innerException)
        {
        }
    }
}