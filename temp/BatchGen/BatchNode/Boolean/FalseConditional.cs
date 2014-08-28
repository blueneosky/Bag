using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class FalseConditional : BooleanConditionalBase
    {
        private static FalseConditional _valueExpressionCache;

        private FalseConditional()
            : base(FalseValueExpression.ValueExpression, false, true)
        {
        }

        public static FalseConditional ValueExpression
        {
            get
            {
                if (_valueExpressionCache == null)
                    _valueExpressionCache = new FalseConditional();
                return _valueExpressionCache;
            }
        }
    }
}