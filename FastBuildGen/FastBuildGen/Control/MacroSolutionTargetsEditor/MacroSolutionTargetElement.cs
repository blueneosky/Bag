using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetElement : ListEditorElement, IDisposable
    {
        private readonly FBMacroSolutionTarget _macroSolutionTarget;

        public MacroSolutionTargetElement(FBMacroSolutionTarget macroSolutionTarget)
            : base(macroSolutionTarget)
        {
            _macroSolutionTarget = macroSolutionTarget;

            _macroSolutionTarget.PropertyChanged += _target_PropertyChanged;

            UpdateText();
        }

        public FBMacroSolutionTarget MacroSolutionTarget
        {
            get { return _macroSolutionTarget; }
        }

        private void _target_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
            Text = _macroSolutionTarget.Keyword;
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
                if (_macroSolutionTarget != null)
                    _macroSolutionTarget.PropertyChanged -= _target_PropertyChanged;
            }
        }

        ~MacroSolutionTargetElement()
        {
            Dispose(false);
        }

        #endregion IDisposable Membres
    }
}