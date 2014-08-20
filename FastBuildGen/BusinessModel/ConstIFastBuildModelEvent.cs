using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal static class ConstIFastBuildModelEvent
    {
        public const string ConstDataChanged = ConstPrefix + "DataChanged";
        public const string ConstWithEchoOff = ConstPrefix + "WithEchoOff";
        private const string ConstPrefix = "IFastBuildModel_";
    }
}