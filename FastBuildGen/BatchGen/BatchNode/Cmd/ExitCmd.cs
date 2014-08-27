using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.Cmd
{
    [Serializable]
    public class ExitCmd : BatchCmdBase
    {
        private bool _exitScript;
        private BatchExpressionBase _returnExpression;

        public ExitCmd(int? code = null, bool exitScript = false)
        {
            _returnExpression = code.HasValue ? new ValueExpression(code.ToString()) : null;
            _exitScript = exitScript;
        }

        public ExitCmd(BatchExpressionBase expression, bool exitScript = false)
        {
            _returnExpression = expression;
            _exitScript = exitScript;
        }

        public bool ExitScript
        {
            get { return _exitScript; }
        }

        public BatchExpressionBase ReturnExpression
        {
            get { return _returnExpression; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}