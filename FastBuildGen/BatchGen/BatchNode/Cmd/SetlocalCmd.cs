﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Cmd
{
    [Serializable]
    public class SetlocalCmd : BatchCmdBase
    {
        public SetlocalCmd()
        {
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}