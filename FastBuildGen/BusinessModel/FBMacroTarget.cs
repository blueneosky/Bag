using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal class FBMacroTarget : FBBaseTarget
    {
        public FBMacroTarget(Guid id)
            : base(id)
        {
            Ids = new ObservableCollection<Guid> { };
        }

        public ObservableCollection<Guid> Ids { get; private set; }
    }
}