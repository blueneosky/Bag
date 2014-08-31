using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Common.Control
{
    internal class SolutionTargetItem : ListBoxItem<FBSolutionTarget>
    {
        public SolutionTargetItem(FBSolutionTarget value)
            : base(value)
        {
        }

        public override string ToString()
        {
            return Value.Keyword;
        }
    }
}