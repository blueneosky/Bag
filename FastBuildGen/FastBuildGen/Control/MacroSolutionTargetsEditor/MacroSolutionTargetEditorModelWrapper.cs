using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.Control.MacroSolutionTargetEditor;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal class MacroSolutionTargetEditorModelWrapper : MacroSolutionTargetEditorModel
    {
        private readonly MacroSolutionTargetsEditorModel _model;

        public MacroSolutionTargetEditorModelWrapper(MacroSolutionTargetsEditorModel model)
            : base(model.ApplicationModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstListEditorModelEvent.ConstElementSelected)
            {
                this.MacroSolutionTarget = _model.MacroSolutionTargetSelected;
            }
        }
    }
}