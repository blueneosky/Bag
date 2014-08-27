using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    public abstract class BooleanConditionalBase : ConditionalCodeNode
    {
        protected BooleanConditionalBase(BooleanExpressionBase expressionComparedToFalse, bool comparedValue, bool inverse)
            : base(expressionComparedToFalse, BooleanValueExpressionBase.GetExpressionValue(comparedValue), inverse)
        {
        }

        protected BooleanConditionalBase(LiteralBatch literalComparedToFalse, bool comparedValue, bool inverse)
            : base(literalComparedToFalse, BooleanValueExpressionBase.GetExpressionValue(comparedValue), inverse)
        {
        }

        public static BooleanConditionalBase GetConditionalValue(bool value)
        {
            if (value)
            {
                return TrueConditional.ValueExpression;
            }
            else
            {
                return FalseConditional.ValueExpression;
            }
        }
    }
}