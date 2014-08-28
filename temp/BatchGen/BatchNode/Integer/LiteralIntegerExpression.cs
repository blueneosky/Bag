using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public class LiteralIntegerExpression : IntegerExpressionBase
    {
        private LiteralBatch _literal;

        public LiteralIntegerExpression(LiteralBatch literal)
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