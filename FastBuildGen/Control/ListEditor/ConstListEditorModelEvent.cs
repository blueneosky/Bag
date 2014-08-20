using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Control.ListEditor
{
    internal static class ConstListEditorModelEvent
    {
        public const string ConstAddEnabled = ConstPrefix + "AddEnabled";
        public const string ConstElementSelected = ConstPrefix + "ElementSelected";
        public const string ConstRemoveEnabled = ConstPrefix + "RemoveEnabled";
        private const string ConstPrefix = "ListEditorModel_";
    }
}