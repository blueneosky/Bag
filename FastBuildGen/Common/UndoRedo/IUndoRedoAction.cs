using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    public interface IUndoRedoAction : IUndoRedoActionDescription
    {
        void Do();

        void Redo();

        void Undo();
    }
}