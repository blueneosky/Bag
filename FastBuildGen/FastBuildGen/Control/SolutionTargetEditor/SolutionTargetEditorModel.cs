using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.SolutionTargetEditor
{
    internal class ModuleEditorModel : INotifyPropertyChanged
    {
        private readonly IFastBuildParamModel _fastBuildParamModel;
        private IParamDescriptionHeoModule _module;

        public ModuleEditorModel(IFastBuildParamModel fastBuildParamModel)
        {
            _fastBuildParamModel = fastBuildParamModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public IFastBuildParamModel FastBuildParamModel
        {
            get { return _fastBuildParamModel; }
        }

        public IParamDescriptionHeoModule Module
        {
            get { return _module; }
            set
            {
                if (Object.Equals(_module, value))
                    return;
                _module = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstModuleEditorModelEvent.ConstModule));
            }
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }
    }
}