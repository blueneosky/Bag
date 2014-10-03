using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Control.TargetEditor;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal class TargetEditorModelWrapper : TargetEditorModel
    {
        private readonly MacroSolutionTargetEditorModel _model;

        public TargetEditorModelWrapper(MacroSolutionTargetEditorModel model)
            : base(model.ApplicationModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        public override FBTarget Target
        {
            get { return _model.MacroSolutionTarget; }
            set { throw new FastBuildGenException("Not permitted"); }
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstMacroSolutionTargetEditorModelEvent.ConstMacroSolutionTarget)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstTargetEditorModelEvent.ConstTarget));
        }
    }
}