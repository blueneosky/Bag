using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class TrueValueExpression : BooleanValueExpressionBase
    {
        private static TrueValueExpression _valueExpressionCache = new TrueValueExpression();

        private TrueValueExpression()
            : base(true)
        {
        }

        public static TrueValueExpression ValueExpression { get { return _valueExpressionCache; } }
    }
}