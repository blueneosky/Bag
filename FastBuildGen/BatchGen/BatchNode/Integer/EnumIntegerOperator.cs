using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public enum EnumIntegerOperator : int
    {
        Aucun = 0,

        Mult,
        Div,
        Mod,
        Add,
        Sub,
        LeftDec,
        RightDec,
        BitAnd,
        BitOr,
        BitXor,
    }
}