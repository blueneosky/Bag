using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBMacroSolutionTarget : FBBaseTarget
    {
        public FBMacroSolutionTarget(Guid id)
            : base(id)
        {
            TargetIds = new ObservableCollection<Guid> { };
        }

        public ObservableCollection<Guid> TargetIds { get; private set; }
    }
}