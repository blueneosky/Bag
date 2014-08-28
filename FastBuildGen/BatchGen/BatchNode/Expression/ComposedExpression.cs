using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Expression
{
    [Serializable]
    public class ComposedExpression : BatchExpressionBase
    {
        private IEnumerable<BatchExpressionBase> _expressions;

        public ComposedExpression(params BatchExpressionBase[] args)
        {
            _expressions = args.ToArray();
        }

        public IEnumerable<BatchExpressionBase> Expressions
        {
            get { return _expressions; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}