using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class BooleanOperatorExpression : BooleanExpressionBase
    {
        private IEnumerable<BooleanExpressionBase> _booleanExpressions;
        private EnumBooleanOperator _operator;

        public BooleanOperatorExpression(EnumBooleanOperator enumOperator, params BooleanExpressionBase[] booleanExpressions)
        {
            _operator = enumOperator;
            _booleanExpressions = booleanExpressions;
        }

        public IEnumerable<BooleanExpressionBase> BooleanExpressions
        {
            get { return _booleanExpressions; }
        }

        public EnumBooleanOperator Operator
        {
            get { return _operator; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}