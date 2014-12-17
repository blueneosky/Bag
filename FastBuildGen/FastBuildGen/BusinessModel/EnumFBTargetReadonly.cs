using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    [Flags]
    internal enum EnumFBTargetReadonly
    {
        None = 0,

        Keyword,
        HelpText,
        MSBuildTarget,
        Enabled,
        SolutionTargetIds,

        MaskFBTarget = Keyword | HelpText,
        MaskFBSolutionTarget = MaskFBTarget | MSBuildTarget | Enabled,
        MaskFBMacroSolutionTarget = MaskFBTarget | SolutionTargetIds,
        MaskAll = Keyword | HelpText | MSBuildTarget | Enabled | SolutionTargetIds,
    }
}