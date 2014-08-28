using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class IsTrueConditional : BooleanConditionalBase
    {
        public IsTrueConditional(BooleanExpressionBase expression)
            : base(expression, true, false)
        {
        }

        public IsTrueConditional(LiteralBatch literal)
            : base(literal, true, false)
        {
        }
    }
}