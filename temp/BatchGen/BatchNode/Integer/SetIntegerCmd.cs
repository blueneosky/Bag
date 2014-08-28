using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public class SetIntegerCmd : BatchCmdBase
    {
        private IntegerExpressionBase _integerExpression;
        private LiteralBatch _literal;

        public SetIntegerCmd(LiteralBatch literal, IntegerExpressionBase integerExpression)
        {
            _literal = literal;
            _integerExpression = integerExpression;
        }

        public SetIntegerCmd(LiteralBatch literal, EnumIntegerOperator attributionOperator, IntegerExpressionBase integerExpression)
        {
            _literal = literal;
            _integerExpression = new IntegerOperatorExpression(attributionOperator, literal.LiteralInteger, integerExpression);
        }

        public IntegerExpressionBase IntegerExpression
        {
            get { return _integerExpression; }
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