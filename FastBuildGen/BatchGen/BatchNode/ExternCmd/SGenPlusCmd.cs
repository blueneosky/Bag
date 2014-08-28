using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.ExternCmd
{
    [Serializable]
    public class SGenPlusCmd : BatchCmdBase
    {
        private BatchExpressionBase _cliExpression;

        public SGenPlusCmd(BatchExpressionBase cliExpression)
        {
            _cliExpression = cliExpression;
        }

        public BatchExpressionBase CliExpression
        {
            get { return _cliExpression; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}