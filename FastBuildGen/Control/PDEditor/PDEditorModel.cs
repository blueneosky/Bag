﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Common.UI;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Control.PDEditor
{
    internal class PDEditorModel : UIModelBase, INotifyPropertyChanged
    {
        private readonly IFastBuildParamModel _fastBuildParamModel;
        private IParamDescription _paramDescription;

        public PDEditorModel(IFastBuildParamModel fastBuildParamModel, IUndoRedoManager undoRedoManager)
            : base(undoRedoManager)
        {
            _fastBuildParamModel = fastBuildParamModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IFastBuildParamModel FastBuildParamModel
        {
            get { return _fastBuildParamModel; }
        }

        public virtual IParamDescription ParamDescription
        {
            get { return _paramDescription; }
            set
            {
                if (Object.Equals(_paramDescription, value))
                    return;
                _paramDescription = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstPDEditorModelEvent.ConstParamDescription));
            }
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }
    }
}