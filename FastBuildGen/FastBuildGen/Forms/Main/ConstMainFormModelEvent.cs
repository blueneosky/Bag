using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Forms.Main
{
    internal static class ConstMainFormModelEvent
    {
        public const string ConstFastBuildDataChanged = ConstPrefix + "FastBuildDataChanged";
        public const string ConstApplicationModelFilePath = ConstPrefix + "FilePath";
        public const string ConstFBModelChanged = ConstPrefix + "FBModel";
        private const string ConstPrefix = "MainFormModel_";
    }
}