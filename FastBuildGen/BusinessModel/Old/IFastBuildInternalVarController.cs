using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    internal interface IFastBuildInternalVarController
    {
        void ResetToDefault();

        void SetValue(string keyword, string newValue);
    }
}