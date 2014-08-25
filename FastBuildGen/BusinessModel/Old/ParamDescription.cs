using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel.Old
{
    internal class ParamDescription : IParamDescription
    {
        private string _helpText;
        private string _keyword;
        private string _name;

        public ParamDescription(string name)
        {
            _name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string HelpText
        {
            get { return _helpText; }
            set
            {
                if (_helpText == value)
                    return;
                _helpText = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionEvent.ConstHelpText));
            }
        }

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                if (_keyword == value)
                    return;
                _keyword = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionEvent.ConstKeyword));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionEvent.ConstSwitchKeyword));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionEvent.ConstName));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionEvent.ConstParamVarName));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionEvent.ConstVarName));
            }
        }

        public string ParamVarName
        {
            get { return ConstModel.ConstParamVarNamePrefix + _name.Replace("?", "help"); }
        }

        public string SwitchKeyword
        {
            get { return ConstModel.ContParamSwitchPrefix + _keyword; }
        }

        public string VarName
        {
            get { return ConstModel.ConstMSBuildTargetVarNamePrefix + _name.Replace("?", "help"); }
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        public bool SameAs(object obj)
        {
            if (this == obj)
                return true;

            IParamDescription paramDescription = obj as IParamDescription;

            // based on Name
            bool result = (paramDescription != null)
                && (paramDescription.Name == Name);

            return result;
        }
    }
}