using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    internal static class ConstIUndoRedoManagerEvent
    {
        public const string ConstCanRedo                = ConstPrefix + "CanRedo";
        public const string ConstCanUndo                = ConstPrefix + "CanUndo";
        public const string ConstRedoActions           = ConstPrefix + "RedoActions";
        public const string ConstUndoActions           = ConstPrefix + "UndoActions";
        public const string ConstRelativeTokenPosition = ConstPrefix + "RelativeTokenPosition";
        private const string ConstPrefix = "IUndoRedoManager_";
    }
}