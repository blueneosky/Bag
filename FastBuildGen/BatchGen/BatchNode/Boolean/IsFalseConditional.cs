using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    public class IsFalseConditional : BooleanConditionalBase
    {
        public IsFalseConditional(BooleanExpressionBase expression)
            : base(expression, false, false)
        {
        }

        public IsFalseConditional(LiteralBatch literal)
            : base(literal, false, false)
        {
        }
    }
}