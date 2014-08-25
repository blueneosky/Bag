using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    internal class ParamDescriptionHeoModule : ParamDescription, IParamDescriptionHeoModule
    {
        private string _msBuildTarget;
        private EnumPlatform _platform;

        public ParamDescriptionHeoModule(string name)
            : base(name)
        {
        }

        public string MSBuildTarget
        {
            get { return _msBuildTarget; }
            set
            {
                if (_msBuildTarget == value)
                    return;
                _msBuildTarget = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionHeoModuleEvent.ConstMSBuildTarget));
            }
        }

        public EnumPlatform Platform
        {
            get { return _platform; }
            set
            {
                if (_platform == value)
                    return;
                _platform = value;
                if ((_platform != EnumPlatform.Win32) && (_platform != EnumPlatform.X86))
                    _platform = EnumPlatform.X86;   // x86 by default if no valide value
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIParamDescriptionHeoModuleEvent.ConstPlatform));
            }
        }
    }
}