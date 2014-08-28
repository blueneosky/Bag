using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.Cmd
{
    [Serializable]
    public class EchoCmd : BatchCmdBase
    {
        private BatchExpressionBase _expression;

        public EchoCmd(string text)
        {
            _expression = new ValueExpression(text);
        }

        public EchoCmd(BatchExpressionBase expression)
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