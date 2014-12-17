using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;

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

        public bool SameAs(SolutionTargetItem item)
        {
            if (item == null)
                return false;

            FBSolutionTarget value = item.Value;
            Debug.Assert(value != null);

            bool result = Value.SameAs(value);

            return result;
        }
    }
}