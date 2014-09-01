using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Control.SolutionTargetEditor
{
    internal class SolutionTargetEditorModel : INotifyPropertyChanged
    {
        private readonly ApplicationModel _applicationModel;
        private FBSolutionTarget _solutionTarget;

        public SolutionTargetEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        public FBSolutionTarget SolutionTarget
        {
            get { return _solutionTarget; }
            set
            {
                if (Object.Equals(_solutionTarget, value))
                    return;
                _solutionTarget = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstSolutionTargetEditorModelEvent.ConstSolutionTarget));
            }
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }
    }
}