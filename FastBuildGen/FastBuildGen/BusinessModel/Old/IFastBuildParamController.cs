using System;
using System.Collections.Generic;
using System.Linq;

namespace FastBuildGen.BusinessModel.Old
{
    internal interface IFastBuildParamController
    {
        IParamDescriptionHeoModule AddModule(string name, string keyword);

        IParamDescriptionHeoTarget AddTarget(string name, string keyword);

        bool DeleteModule(string name);

        bool DeleteTarget(string name);

        string GetUniqKeyword(string baseKeyword);

        string GetUniqName(string baseName);

        IParamDescriptionHeoModule NewModule(string baseName);

        IParamDescriptionHeoTarget NewTarget(string baseName);
    }
}