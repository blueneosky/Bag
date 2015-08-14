using System;
using System.Collections.Generic;

using System.Linq;

using System.Text;

namespace RPTimeline.Model
{
    public static class ConstPropertyChanged
    {
        private const string ConstPropertyChangedPrefix = "ConstPropertyChanged.";

        public static class IDateTime
        {
            private const string ConstIDateTimePrefix = ConstPropertyChangedPrefix + "IDateTime.";

            public const string Components = ConstIDateTimePrefix + "Components";
        }
    }
}