using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    public interface IFastBuildController
    {
        bool LoadFastBuildConfig(string configFilePath);

        void SaveFastBuildConfig(string configFilePath);
    }
}