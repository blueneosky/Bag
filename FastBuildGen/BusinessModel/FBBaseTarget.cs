using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal abstract class MSBuildBaseTarget : INotifyPropertyChanged
    {
        private readonly Guid _id;
        private string _helpText;
        private string _keyword;

        public MSBuildBaseTarget(Guid id)
        {
            _id = id;
        }

        #region IFBTarget Membres

        public string HelpText
        {
            get { return _helpText; }
            set
            {
                _helpText = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetHelpText));
            }
        }

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                _keyword = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetKeyword));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetSwitchKeyword));
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetName));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetParamVarName));
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetVarName));
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