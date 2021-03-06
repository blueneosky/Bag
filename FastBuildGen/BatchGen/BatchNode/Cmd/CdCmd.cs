﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.Cmd
{
    [Serializable]
    public class CdCmd : BatchCmdBase
    {
        private BatchExpressionBase _pathExpression;

        public CdCmd(string path)
        {
            _pathExpression = new ValueExpression(path);
        }

        public CdCmd(BatchExpressionBase pathExpression)
        {
            _pathExpression = pathExpression;
        }

        public BatchExpressionBase PathExpression
        {
            get { return _pathExpression; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}