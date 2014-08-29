using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal static class ConstTargetEditorModelEvent
    {
        public const string ConstAvailableModules = ConstPrefix + "AvailableModules";
        public const string ConstTarget = ConstPrefix + "Target";
        private const string ConstPrefix = "TargetEditorModel_";
    }
}