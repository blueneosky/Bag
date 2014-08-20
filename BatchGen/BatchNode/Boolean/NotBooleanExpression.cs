using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class NotBooleanExpression : BooleanExpressionBase
    {
        private BooleanExpressionBase _booleanExpression;

        public NotBooleanExpression(BooleanExpressionBase booleanExpression)
        {
            _booleanExpression = booleanExpression;
        }

        public BooleanExpressionBase BooleanExpression
        {
            get { return _booleanExpression; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}