using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.Cmd
{
    [Serializable]
    public class TitleCmd : BatchCmdBase
    {
        private BatchExpressionBase _expression;

        public TitleCmd(string title)
        {
            _expression = new ValueExpression(title);
        }

        public TitleCmd(BatchExpressionBase expression)
        {
            _expression = expression;
        }

        public BatchExpressionBase Expression
        {
            get { return _expression; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}