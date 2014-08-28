using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Expression
{
    [Serializable]
    public class QuotedValueExpression : ComposedExpression
    {
        private static ValueExpression quoteCache = new ValueExpression("\"");

        public QuotedValueExpression(BatchExpressionBase expression)
            : base(quoteCache, expression, quoteCache)
        {
        }
    }
}