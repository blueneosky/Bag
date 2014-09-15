using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBMacroSolutionTargetAll : FBMacroSolutionTarget
    {
        public FBMacroSolutionTargetAll(Guid id)
            : base(id, EnumFBTargetReadonly.MaskFBTarget)
        {
        }
    }
}