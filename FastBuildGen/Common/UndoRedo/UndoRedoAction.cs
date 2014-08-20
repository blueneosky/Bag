using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    [DebuggerDisplay("Name = {Name}; Title = {Title}")]
    internal class UndoRedoAction : IUndoRedoAction
    {
        #region Members

        private readonly Action _doAction;
        private readonly Action _redoAction;
        private readonly Action _undoAction;

        #endregion Members

        #region ctor

        public UndoRedoAction(string name, string title, Action doAction, Action undoAction)
            : this(name, title, doAction, doAction, undoAction)
        {
        }

        public UndoRedoAction(string name, string title, Action doAction, Action redoAction, Action undoAction)
        {
            Name = name;
            Title = title;
            _doAction = doAction ?? EmptyAction;
            _redoAction = redoAction ?? EmptyAction;
            _undoAction = undoAction ?? EmptyAction;
        }

        #endregion ctor

        #region IUndoRedoAction

        public string Name { get; private set; }

        public string Title { get; private set; }

        public void Do()
        {
            _doAction();
        }

        public void Redo()
        {
            _redoAction();
        }

        public void Undo()
        {
            _undoAction();
        }

        #endregion IUndoRedoAction

        private void EmptyAction()
        {
        }
    }
}