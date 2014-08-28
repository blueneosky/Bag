using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class TrueConditional : BooleanConditionalBase
    {
        private static TrueConditional _valueExpressionCache;

        private TrueConditional()
            : base(TrueValueExpression.ValueExpression, true, false)
        {
        }

        public static TrueConditional ValueExpression
        {
            get
            {
                if (_valueExpressionCache == null)
                    _valueExpressionCache = new TrueConditional();
                return _valueExpressionCache;
            }
        }
    }
}