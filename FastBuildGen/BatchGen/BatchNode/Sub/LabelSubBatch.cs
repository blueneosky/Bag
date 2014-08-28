using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Sub
{
    [Serializable]
    public class LabelSubBatch : LabelBatchBase
    {
        public LabelSubBatch(string label)
            : base(label)
        {
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}