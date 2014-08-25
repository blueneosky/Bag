using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Control.InternalVarEditor;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.InternalVarsEditor
{
    internal class InternalVarEditorModelWrapper : InternalVarEditorModel
    {
        private readonly InternalVarsEditorModel _model;

        public InternalVarEditorModelWrapper(InternalVarsEditorModel model)
            : base(model.FastBuildInternalVarModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstListEditorModelEvent.ConstElementSelected)
            {
                this.Keyword = _model.KeywordSelected;
            }
        }
    }
}