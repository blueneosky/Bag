using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Control.TargetEditor
{
    internal class TargetEditorModel : INotifyPropertyChanged
    {
        private readonly ApplicationModel _applicationModel;
        private FBTarget _target;

        public TargetEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        public virtual FBTarget Target
        {
            get { return _target; }
            set
            {
                if (Object.Equals(_target, value))
                    return;
                _target = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstTargetEditorModelEvent.ConstTarget));
            }
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }
    }
}