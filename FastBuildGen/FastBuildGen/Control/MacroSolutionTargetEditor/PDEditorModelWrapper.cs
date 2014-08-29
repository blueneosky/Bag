using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Control.PDEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal class PDEditorModelWrapper : PDEditorModel
    {
        private readonly TargetEditorModel _model;

        public PDEditorModelWrapper(TargetEditorModel model)
            : base(model.FastBuildParamModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        public override IParamDescription ParamDescription
        {
            get { return _model.Target; }
            set { throw new FastBuildGenException("Not permitted"); }
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstTargetEditorModelEvent.ConstTarget)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPDEditorModelEvent.ConstParamDescription));
        }
    }
}