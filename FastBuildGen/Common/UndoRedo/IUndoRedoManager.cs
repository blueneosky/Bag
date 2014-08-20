using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    public interface IUndoRedoManager : INotifyPropertyChanged
    {
        bool CanRedo { get; }

        bool CanUndo { get; }

        /// <summary>
        /// First redoable to last redoable.
        /// </summary>
        IEnumerable<IUndoRedoActionDescription> RedoActions { get; }

        int? RelativeTokenPosition { get; }

        /// <summary>
        /// First undoable to last undoable.
        /// </summary>
        IEnumerable<IUndoRedoActionDescription> UndoActions { get; }

        void Cancel();

        void Cancel(string message);

        void Cancel(string message, Exception innerException);

        void Clear();

        void Do(IUndoRedoAction action);

        void Do(string name, string title, Action doAction, Action undoAction);

        void Do(string name, string title, Action doAction, Action redoAction, Action undoAction);

        UndoRedoActionMacroBloc NewUndoRedoActionMacroBloc(string name, string title);

        void Redo();

        void ResetToken();

        void SetToken();

        void StartMacroBloc(string name, string title);

        void StopMacroBloc(string name);

        void Undo();
    }
}