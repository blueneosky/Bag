using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Sub
{
    [Serializable]
    public class CallSubCmd : BatchCmdBase
    {
        private LabelSubBatch _label;

        public CallSubCmd(LabelSubBatch label)
        {
            _label = label;
        }

        public LabelSubBatch Label
        {
            get { return _label; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}