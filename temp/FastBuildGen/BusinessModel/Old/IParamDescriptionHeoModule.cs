using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    public interface IParamDescriptionHeoModule : IParamDescription
    {
        string MSBuildTarget { get; set; }

        EnumPlatform Platform { get; set; }
    }
}