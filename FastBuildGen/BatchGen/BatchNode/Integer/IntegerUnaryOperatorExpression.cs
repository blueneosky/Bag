using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public class IntegerUnaryOperatorExpression : IntegerExpressionBase
    {
        private IntegerExpressionBase _integerExpression;
        private EnumUnaryIntegerOperator _unaryOperator;

        public IntegerUnaryOperatorExpression(EnumUnaryIntegerOperator unaryOperator, IntegerExpressionBase integerExpression)
        {
            _unaryOperator = unaryOperator;
            _integerExpression = integerExpression;
        }

        public IntegerExpressionBase IntegerExpression
        {
            get { return _integerExpression; }
        }

        public EnumUnaryIntegerOperator UnaryOperator
        {
            get { return _unaryOperator; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}