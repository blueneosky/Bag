using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Expression
{
    [Serializable]
    public class SetExpressionCmd : BatchCmdBase
    {
        private BatchExpressionBase _expression;
        private LiteralBatch _literal;

        public SetExpressionCmd(LiteralBatch literal, BatchExpressionBase value)
        {
            _literal = literal;
            _expression = value;
        }

        public BatchExpressionBase Expression
        {
            get { return _expression; }
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