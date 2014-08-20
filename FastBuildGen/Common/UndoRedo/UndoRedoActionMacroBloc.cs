using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common.UndoRedo
{
    public class UndoRedoActionMacroBloc : IDisposable
    {
        private readonly IUndoRedoManager _manager;

        private bool _isStarted;

        private bool _isStopped;

        private string _name;

        private string _title;

        public UndoRedoActionMacroBloc(IUndoRedoManager manager, string name, string title)
        {
            _manager = manager;
            _name = name;
            _title = title;

            Start();
        }

        public void Do(IUndoRedoAction action)
        {
            _manager.Do(action);
        }

        public void Start()
        {
            if (_isStarted)
                return;

            _isStarted = true;
            _manager.StartMacroBloc(_name, _title);
        }

        public void Stop()
        {
            if (false == _isStarted)
                throw new FastBuildGenException("Unexpected state for " + this.GetType().Name);

            if (_isStopped)
                return;

            _isStopped = true;
            _manager.StopMacroBloc(_name);
        }

        #region IDisposable

        // Track whether Dispose has been called.
        private bool _disposed = false;

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~UndoRedoActionMacroBloc()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    // Note : nothing yet
                }

                Stop(); // always stop at disposing

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                // Note : nothing yet

                // Note disposing has been done.
                _disposed = true;
            }
        }

        #endregion IDisposable
    }
}