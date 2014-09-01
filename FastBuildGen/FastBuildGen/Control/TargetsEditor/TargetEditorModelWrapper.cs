﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.Control.TargetEditor;

namespace FastBuildGen.Control.TargetsEditor
{
    internal class TargetEditorModelWrapper : TargetEditorModel
    {
        private readonly TargetsEditorModel _model;

        public TargetEditorModelWrapper(TargetsEditorModel model)
            : base(model.FastBuildParamModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstListEditorModelEvent.ConstElementSelected)
            {
                this.Target = _model.TargetSelected;
            }
        }
    }
}