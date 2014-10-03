using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal static class ConstFBEvent
    {
        private const string ConstApplicationPrefix = "ApplicationModel";
        public const string ConstApplicationModelFBModel = ConstApplicationPrefix + "FBModel";
        public const string ConstApplicationModelDataChanged = ConstApplicationPrefix + "DataChanged";

        private const string ConstFBModelPrefix = "FBModel_";
        public const string ConstFBModelWithEchoOff = ConstFBModelPrefix + "WithEchoOff";

        private const string ConstFBTargetPrefix = "FBTarget_";
        public const string ConstFBTargetHelpText = "HelpText";
        public const string ConstFBTargetKeyword = ConstFBTargetPrefix + "Keyword";
        public const string ConstFBTargetParamVarName = ConstFBTargetPrefix + "ParamVarName";
        public const string ConstFBTargetSwitchKeyword = ConstFBTargetPrefix + "SwitchKeyword";
        public const string ConstFBTargetVarName = ConstFBTargetPrefix + "VarName";
        public const string ConstFBTargetMSBuildTarget = ConstFBTargetPrefix + "MSBuildTarget";
        public const string ConstFBTargetEnabled = ConstFBTargetPrefix + "Enabled";
    }
}