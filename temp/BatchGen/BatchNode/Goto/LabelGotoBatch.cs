using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Goto
{
    [Serializable]
    public class LabelGotoBatch : LabelBatchBase
    {
        public LabelGotoBatch(string label)
            : base(label)
        {
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}