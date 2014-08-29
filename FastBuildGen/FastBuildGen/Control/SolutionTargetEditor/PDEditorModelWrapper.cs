using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Control.PDEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.SolutionTargetEditor
{
    internal class PDEditorModelWrapper : PDEditorModel
    {
        private readonly SolutionTargetEditorModel _model;

        public PDEditorModelWrapper(SolutionTargetEditorModel model)
            : base(model.FastBuildParamModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        public override IParamDescription ParamDescription
        {
            get { return _model.Module; }
            set { throw new FastBuildGenException("Not permitted"); }
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstSolutionTargetEditorModelEvent.ConstModule)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPDEditorModelEvent.ConstParamDescription));
        }
    }
}