using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBMacroSolutionTarget : FBTarget
    {
        public FBMacroSolutionTarget(Guid id)
            : base(id)
        {
            SolutionTargetIds = new ObservableCollection<Guid> { };
        }

        public ObservableCollection<Guid> SolutionTargetIds { get; private set; }
    }
}