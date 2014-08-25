using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    public interface IFastBuildModel : INotifyPropertyChanged
    {
        bool DataChanged { get; }

        IFastBuildInternalVarModel FastBuildInternalVarModel { get; }

        IFastBuildParamModel FastBuildParamModel { get; }

        bool WithEchoOff { get; set; }

        void Initialize();

        void ResetDataChanged();
    }
}