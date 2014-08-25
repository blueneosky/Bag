using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FastBuildGen.BusinessModel
{
    internal interface IMSBuildTarget : INotifyPropertyChanged
    {
        string HelpText { get; set; }

        string Keyword { get; set; }

        string Name { get; set; }

        Guid Id { get; }

        string ParamVarName { get; }

        string SwitchKeyword { get; }

        string VarName { get; }
    }
}