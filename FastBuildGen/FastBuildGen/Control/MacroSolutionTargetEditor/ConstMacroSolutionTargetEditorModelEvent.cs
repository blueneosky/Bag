using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal static class ConstMacroSolutionTargetEditorModelEvent
    {
        public const string ConstAvailableSolutionTargets = ConstPrefix + "AvailableSolutionTargets";
        public const string ConstMacroSolutionTarget = ConstPrefix + "MacroSolutionTarget";
        private const string ConstPrefix = "MacroSolutionTargetEditorModel_";
    }
}