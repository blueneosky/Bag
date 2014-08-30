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
        private readonly MacroSolutionTargetEditorModel _model;

        public PDEditorModelWrapper(MacroSolutionTargetEditorModel model)
            : base(model.ApplicationModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        public override IParamDescription ParamDescription
        {
            get { return _model.MacroSolutionTarget; }
            set { throw new FastBuildGenException("Not permitted"); }
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstMacroSolutionTargetEditorModelEvent.ConstMacroSolutionTarget)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPDEditorModelEvent.ConstParamDescription));
        }
    }
}