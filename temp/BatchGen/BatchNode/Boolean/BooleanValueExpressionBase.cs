using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public abstract class BooleanValueExpressionBase : BooleanExpressionBase
    {
        private bool _booleanValue;

        protected BooleanValueExpressionBase(bool booleanValue)
        {
            _booleanValue = booleanValue;
        }

        public bool BooleanValue
        {
            get { return _booleanValue; }
        }

        public static BooleanValueExpressionBase GetExpressionValue(bool booleanValue)
        {
            if (booleanValue)
            {
                return TrueValueExpression.ValueExpression;
            }
            else
            {
                return FalseValueExpression.ValueExpression;
            }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}