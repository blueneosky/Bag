using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Control.PDEditor;

namespace FastBuildGen.Control.ModuleEditor
{
    internal class PDEditorModelWrapper : PDEditorModel
    {
        private readonly ModuleEditorModel _model;

        public PDEditorModelWrapper(ModuleEditorModel model)
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
            if (e.PropertyName == ConstModuleEditorModelEvent.ConstModule)
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPDEditorModelEvent.ConstParamDescription));
        }
    }
}