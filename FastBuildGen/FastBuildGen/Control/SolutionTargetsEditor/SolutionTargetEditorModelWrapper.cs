using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.Control.SolutionTargetEditor;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class SolutionTargetEditorModelWrapper : SolutionTargetEditorModel
    {
        private readonly SolutionTargetsEditorModel _model;

        public SolutionTargetEditorModelWrapper(SolutionTargetsEditorModel model)
            : base(model.ApplicationModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstListEditorModelEvent.ConstElementSelected)
            {
                this.Module = _model.SolutionTargetSelected;
            }
        }
    }
}