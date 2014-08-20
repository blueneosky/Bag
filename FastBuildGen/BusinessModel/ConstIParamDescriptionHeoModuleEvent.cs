using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal static class ConstIParamDescriptionHeoModuleEvent
    {
        public const string ConstHelpText = ConstIParamDescriptionEvent.ConstHelpText;
        public const string ConstKeyword = ConstIParamDescriptionEvent.ConstKeyword;
        public const string ConstMSBuildTarget = ConstPrefix + "Target";
        public const string ConstName = ConstIParamDescriptionEvent.ConstName;
        public const string ConstPlatform = ConstPrefix + "Platform";
        private const string ConstPrefix = "IParamDescriptionHeoModule_";
    }
}