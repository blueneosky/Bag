using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    public static class ExtensionIUndoRedoManager
    {

        public static IUndoRedoActionDescription FirstRedoAction(this IUndoRedoManager manager)
        {
            return manager.RedoActions.FirstOrDefault();
        }

        public static IUndoRedoActionDescription FirstUndoAction(this IUndoRedoManager manager)
        {
            return manager.UndoActions.FirstOrDefault();
        }


    }
}
