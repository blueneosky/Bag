using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.Config
{

    [Flags]
    public enum EnumArgumentMultiValueDelimiter
    {

        Undefined = 0,

        EqualsChar = 1,

        ColonChar = 2,

        //--------- alias --------------

        All = EqualsChar | ColonChar,

    }
}
