using System;
using System.ComponentModel;

namespace FastBuildGen.BusinessModel
{
    internal class FBTarget : FBBaseTarget
    {
        private string _msBuildTarget;
        private bool _enabled;

        public FBTarget(Guid id)
            : base(id)
        {
        }

        public string MSBuildTarget
        {
            get { return _msBuildTarget; }
            set
            {
                if (_msBuildTarget == value) return;
                _msBuildTarget = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetMSBuildTarget));
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled == value) return;
                _enabled = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBTargetEnabled));
            }
        }
    }
}