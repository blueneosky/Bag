using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.Gen;

namespace BatchGen.BatchNode.Macro
{
    public class WhileMacro : ForMacro
    {
        public WhileMacro(ConditionalCodeNode whileTest, BatchFileNodeBase loopBody, string gotoBaseLabel)
            : base(null, whileTest, null, loopBody, gotoBaseLabel, ConstBatchGen.ConstGotoWhilePrefix, ConstBatchGen.ConstGotoEndWhilePrefix)
        {
        }
    }
}