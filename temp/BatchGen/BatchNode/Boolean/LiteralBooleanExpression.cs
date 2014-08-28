using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class LiteralBooleanExpression : BooleanExpressionBase
    {
        private LiteralBatch _literal;

        public LiteralBooleanExpression(LiteralBatch literal)
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