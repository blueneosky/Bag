using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Integer;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class SetBooleanCmd : SetIntegerCmd
    {
        public SetBooleanCmd(LiteralBatch literal, BooleanExpressionBase valueExpression)
            : base(literal, valueExpression)
        {
        }

        public SetBooleanCmd(LiteralBatch literal, EnumBooleanOperator attributionOperator, BooleanExpressionBase valueExpression)
            : base(literal, (EnumIntegerOperator)attributionOperator, valueExpression)
        {
        }
    }
}