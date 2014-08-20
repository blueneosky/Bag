using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class FalseValueExpression : BooleanValueExpressionBase
    {
        private static FalseValueExpression _valueExpressionCache = new FalseValueExpression();

        private FalseValueExpression()
            : base(false)
        {
        }

        public static FalseValueExpression ValueExpression { get { return _valueExpressionCache; } }
    }
}