using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    public interface IFastBuildParamModel : INotifyPropertyChanged
    {
        event NotifyCollectionChangedEventHandler HeoModuleParamsChanged;

        event NotifyCollectionChangedEventHandler HeoTargetParamsChanged;

        bool DataChanged { get; }

        IEnumerable<IParamDescription> FastBuildHeoParams { get; }

        IEnumerable<IParamDescription> FastBuildParams { get; }

        IEnumerable<IParamDescriptionHeoModule> HeoModuleParams { get; }

        IEnumerable<IParamDescriptionHeoTarget> HeoTargetParams { get; }

        IParamDescriptionHeoModule AddHeoModuleParam(string name, string keyword);

        IParamDescriptionHeoTarget AddHeoTargetParam(string name, string keyword);

        void ClearHeoModuleParams();

        void ClearHeoTargetParams();

        void Initialize();

        bool IsKeywordUsed(string keyword);

        bool IsNameUsed(string name);

        bool RemoveHeoModuleParam(string name);

        bool RemoveHeoTargetParam(string name);

        void ResetDataChanged();
    }
}