using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public class IntegerOperatorExpression : IntegerExpressionBase
    {
        private IEnumerable<IntegerExpressionBase> _integerExpressions;
        private EnumIntegerOperator _operator;

        public IntegerOperatorExpression(EnumIntegerOperator enumOperator, params IntegerExpressionBase[] integerExpressions)
        {
            _operator = enumOperator;
            _integerExpressions = integerExpressions;
        }

        public IEnumerable<IntegerExpressionBase> IntegerExpressions
        {
            get { return _integerExpressions; }
        }

        public EnumIntegerOperator Operator
        {
            get { return _operator; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}