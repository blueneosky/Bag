using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBTarget : MSBuildBaseTarget
    {
        private string _msBuildTarget;

        public FBTarget(Guid id)
            : base(id)
        {
        }

        public string MSBuildTarget
        {
            get { return _msBuildTarget; }
            set
            {
                _msBuildTarget = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBBuisnessModelEvent.ConstFBTargetMSBuildTarget));
            }
        }
    }
}