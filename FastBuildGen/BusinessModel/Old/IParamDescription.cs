using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    public interface IParamDescription : INotifyPropertyChanged
    {
        string HelpText { get; set; }

        string Keyword { get; set; }

        string Name { get; set; }

        string ParamVarName { get; }

        string SwitchKeyword { get; }

        string VarName { get; }

        bool SameAs(object obj);
    }
}