using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Control.InternalVarEditor
{
    internal static class ConstInternalVarEditorModelEvent
    {
        private const string ConstPrefix = "InternalVarEditorModel_";

        #region Properties

        public const string ConstKeyword = ConstPrefix + "Keyword";
#warning TODO - à voir si conservé
        public const string ConstUICurrentActiveField = ConstPrefix + "UICurrentActiveField";
        public const string ConstValue = ConstPrefix + "Value";

        #endregion Properties

#warning TODO - à voir si conservé

        #region Property values

        public const string ConstUICurrentActiveField_Keyword = ConstUICurrentActiveFieldPrefix + "Keyword";
        private const string ConstUICurrentActiveFieldPrefix = ConstUICurrentActiveField + "_Value_";

        #endregion Property values
    }
}