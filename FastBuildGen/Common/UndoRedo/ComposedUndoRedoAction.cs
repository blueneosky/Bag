using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    [DebuggerDisplay("Name = {Name}; Title = {Title}; Count = {Count}"), DebuggerTypeProxy(typeof(System_ComposedUndoRedoActionDebugView))]
    internal class ComposedUndoRedoAction : IEnumerable<IUndoRedoAction>, IUndoRedoAction
    {
        private readonly IList<IUndoRedoAction> _actions;
        private string _name;
        private string _title;

        public ComposedUndoRedoAction(string name, string title, IEnumerable<IUndoRedoAction> actions)
        {
            _name = name;
            _title = title;
            _actions = new List<IUndoRedoAction>(actions);
        }

        public ComposedUndoRedoAction(string name, string title)
            : this(name, title, new IUndoRedoAction[0])
        {
        }

        public ComposedUndoRedoAction(IEnumerable<IUndoRedoAction> actions)
            : this(null, null, actions)
        {
        }

        public int Count { get { return _actions.Count; } }

        public void Add(IUndoRedoAction action)
        {
            _actions.Add(action);
        }

        #region IEnumerable

        public IEnumerator<IUndoRedoAction> GetEnumerator()
        {
            IEnumerator<IUndoRedoAction> result = _actions
                .OfType<IUndoRedoAction>()
                .GetEnumerator();

            return result;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            System.Collections.IEnumerator result = _actions.GetEnumerator();

            return result;
        }

        #endregion IEnumerable

        #region IUndoRedoAction

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public void Do()
        {
            IEnumerable<IUndoRedoAction> actions = _actions;
            foreach (IUndoRedoAction action in actions)
            {
                action.Do();
            }
        }

        public void Redo()
        {
            IEnumerable<IUndoRedoAction> actions = _actions;
            foreach (IUndoRedoAction action in actions)
            {
                action.Redo();
            }
        }

        public void Undo()
        {
            IEnumerable<IUndoRedoAction> actions = _actions.Reverse();
            foreach (IUndoRedoAction action in actions)
            {
                action.Undo();
            }
        }

        #endregion IUndoRedoAction

        #region Debugger

        internal sealed class System_ComposedUndoRedoActionDebugView
        {
            private ComposedUndoRedoAction composedUndoRedoAction;

            public System_ComposedUndoRedoActionDebugView(ComposedUndoRedoAction stack)
            {
                if (stack == null)
                {
                    throw new ArgumentNullException("stack");
                }
                this.composedUndoRedoAction = stack;
            }

            [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
            public IUndoRedoAction[] Items
            {
                get
                {
                    return this.composedUndoRedoAction.ToArray();
                }
            }
        }

        #endregion Debugger
    }
}