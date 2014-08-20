using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Integer
{
    [Serializable]
    public class IntegerValueExpression : IntegerExpressionBase
    {
        private int _integerValue;

        public IntegerValueExpression(int value)
        {
            _integerValue = value;
        }

        public int IntegerValue
        {
            get { return _integerValue; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}