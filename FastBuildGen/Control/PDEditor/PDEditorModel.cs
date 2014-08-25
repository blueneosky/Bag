using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Control.PDEditor
{
    internal class PDEditorModel : INotifyPropertyChanged
    {
        private readonly IFastBuildParamModel _fastBuildParamModel;
        private IParamDescription _paramDescription;

        public PDEditorModel(IFastBuildParamModel fastBuildParamModel)
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