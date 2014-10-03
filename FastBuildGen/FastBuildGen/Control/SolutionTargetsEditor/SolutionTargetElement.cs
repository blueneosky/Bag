using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class SolutionTargetElement : ListEditorElement, IDisposable
    {
        private readonly FBSolutionTarget _solutionTarget;

        public SolutionTargetElement(FBSolutionTarget SolutionTarget)
            : base(SolutionTarget)
        {
            _solutionTarget = SolutionTarget;

            _solutionTarget.PropertyChanged += _solutionTarget_PropertyChanged;

            UpdateText();
        }

        public FBSolutionTarget SolutionTarget
        {
            get { return _solutionTarget; }
        }

        private void _solutionTarget_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstFBTargetKeyword:
                    UpdateText();
                    break;

                default:
                    break;
            }
        }

        private void UpdateText()
        {
            Text = _solutionTarget.Keyword;
        }

        #region IDisposable Membres

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            _isDisposed = true;

            if (disposing)
            {
                // managed resources
                if (_solutionTarget != null)
                    _solutionTarget.PropertyChanged -= _solutionTarget_PropertyChanged;
            }
        }

        ~SolutionTargetElement()
        {
            Dispose(false);
        }

        #endregion IDisposable Membres
    }
}