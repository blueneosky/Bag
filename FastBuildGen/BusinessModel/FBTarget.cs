using System;
using System.ComponentModel;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class FBTarget : INotifyPropertyChanged
    {
        private readonly Guid _id;
        private string _helpText;
        private string _keyword;

        public FBTarget(Guid id)
        {
            _id = id;
        }

        #region IFBTarget Membres

        public string HelpText
        {
            get { return _helpText; }
            set
            {
                if (_helpText == value) return;
                _helpText = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetHelpText));
            }
        }

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                if (_keyword == value) return;
                _keyword = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetKeyword));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetSwitchKeyword));
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetName));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetParamVarName));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetVarName));
            }
        }

        public Guid Id
        {
            get { return _id; }
        }

        public string ParamVarName
        {
            get { return ConstFBModel.ConstParamVarNamePrefix + _name.Replace("?", "help"); }
        }

        public string SwitchKeyword
        {
            get { return ConstFBModel.ContParamSwitchPrefix + _keyword; }
        }

        public string VarName
        {
            get { return ConstFBModel.ConstMSBuildTargetVarNamePrefix + _name.Replace("?", "help"); }
        }

        #endregion IFBTarget Membres

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}