using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Expression
{
    [Serializable]
    public class LiteralValueExpression : BatchExpressionBase
    {
        private LiteralBatch _literal;

        public LiteralValueExpression(LiteralBatch literal)
        {
            _literal = literal;
        }

        public LiteralBatch Literal
        {
            get { return _literal; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}