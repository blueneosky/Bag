using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Common.UI
{
    public abstract class UIModelBase : IUIModel
    {
        private readonly IUndoRedoManager _undoRedoManager;

        public UIModelBase(IUndoRedoManager undoRedoManager)
        {
            _undoRedoManager = undoRedoManager;
        }

        public event EventHandler<UIEnableViewRequestedEventArgs> UIEnableViewRequested;

        public IUndoRedoManager UndoRedoManager
        {
            get { return _undoRedoManager; }
        }

        public bool UIEnableView(object param)
        {
            UIEnableViewRequestedEventArgs e = new UIEnableViewRequestedEventArgs(param);
            OnUIEnableViewRequested(this, e);

            return !e.Canceled;
        }

        protected void OnUIEnableViewRequested(object sender, UIEnableViewRequestedEventArgs e)
        {
            UIEnableViewRequested.Notify(sender, e);
        }
    }
}