using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public class RemBatch : BatchStatementNodeBase
    {
        private string _comment;

        public RemBatch(string comment)
        {
            _comment = comment;
        }

        public string Comment
        {
            get { return _comment; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}