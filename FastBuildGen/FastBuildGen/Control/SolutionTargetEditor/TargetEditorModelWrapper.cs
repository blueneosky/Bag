using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Control.TargetEditor;

namespace FastBuildGen.Control.SolutionTargetEditor
{
    internal class TargetEditorModelWrapper : TargetEditorModel
    {
        private readonly SolutionTargetEditorModel _model;

        public TargetEditorModelWrapper(SolutionTargetEditorModel model)
            : base(model.ApplicationModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        public override FBTarget Target
        {
            get { return _model.SolutionTarget; }
            set { throw new FastBuildGenException("Not permitted"); }
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstSolutionTargetEditorModelEvent.ConstSolutionTarget)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstTargetEditorModelEvent.ConstTarget));
        }
    }
}