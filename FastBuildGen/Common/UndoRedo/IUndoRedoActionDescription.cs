using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    public interface IUndoRedoActionDescription
    {
        string Name { get; }

        string Title { get; }
    }
}