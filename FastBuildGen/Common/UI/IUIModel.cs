using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Common.UI
{
    public interface IUIModel
    {
        event EventHandler<UIEnableViewRequestedEventArgs> UIEnableViewRequested;

        IUndoRedoManager UndoRedoManager { get; }

        bool UIEnableView(object param);
    }
}