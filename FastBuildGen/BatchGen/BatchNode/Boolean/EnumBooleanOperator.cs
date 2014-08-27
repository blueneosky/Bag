using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Integer;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public enum EnumBooleanOperator : int
    {
        Auncun = EnumIntegerOperator.Aucun,

        Or = EnumIntegerOperator.BitOr,
        And = EnumIntegerOperator.BitAnd,
        Xor = EnumIntegerOperator.BitXor,
    }
}