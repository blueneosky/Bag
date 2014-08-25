using System;
using System.ComponentModel;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class FBModel : INotifyPropertyChanged
    {
        public FBModel()
        {
            Targets = new ObservableDictionary<Guid, FBTarget> { };
            MacroTargets = new ObservableDictionary<Guid, FBMacroTarget> { };
            InternalVars = new ObservableDictionary<string, string> { };
        }

        public ObservableDictionary<Guid, FBTarget> Targets { get; private set; }

        public ObservableDictionary<Guid, FBMacroTarget> MacroTargets { get; private set; }

        public ObservableDictionary<string, string> InternalVars { get; private set; }

        private bool _withEchoOff;

        public bool WithEchoOff
        {
            get { return _withEchoOff; }
            set
            {
                if (_withEchoOff == value) return;
                _withEchoOff = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBModelWithEchoOff));
            }
        }

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}