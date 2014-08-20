using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Goto
{
    [Serializable]
    public class GotoCmd : BatchCmdBase
    {
        private LabelGotoBatch _label;

        public GotoCmd(LabelGotoBatch label)
        {
            _label = label;
        }

        public LabelGotoBatch Label
        {
            get { return _label; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}