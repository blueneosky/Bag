using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.Config
{
    [Flags]
    public enum EnumArgumentSwitchChar
    {
        Undefined = 0,

        SlashChar = 1,

        DashChar = 2,


        //--------- alias --------------

        WindowsStyle = SlashChar,
        UnixStyle = DashChar,

        All = SlashChar | DashChar,

    }
}
