using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Expression
{
    [Serializable]
    public class ValueExpression : BatchExpressionBase
    {
        private string _value;

        public ValueExpression(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}