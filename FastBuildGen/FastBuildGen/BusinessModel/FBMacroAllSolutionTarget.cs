using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBMacroAllSolutionTarget : FBMacroSolutionTarget
    {
        public FBMacroAllSolutionTarget(Guid id)
            : base(id, false)
        {
        }
    }
}