﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public abstract class BatchCmdBase : BatchStatementNodeBase
    {
        protected BatchCmdBase()
        {
        }
    }
}