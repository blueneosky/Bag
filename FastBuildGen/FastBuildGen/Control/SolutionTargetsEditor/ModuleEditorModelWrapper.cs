﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.Control.SolutionTargetEditor;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class ModuleEditorModelWrapper : SolutionTargetEditorModel
    {
        private readonly ModulesEditorModel _model;

        public ModuleEditorModelWrapper(ModulesEditorModel model)
            : base(model.FastBuildParamModel)
        {
            _model = model;

            _model.PropertyChanged += _model_PropertyChanged;
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ConstListEditorModelEvent.ConstElementSelected)
            {
                this.Module = _model.ModuleSelected;
            }
        }
    }
}