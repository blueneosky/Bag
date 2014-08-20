using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public enum EnumUnaryIntegerOperator : int
    {
        Aucun = 0,

        Not,
        Comp1,
        Neg,

        Comp2 = Neg,
    }
}