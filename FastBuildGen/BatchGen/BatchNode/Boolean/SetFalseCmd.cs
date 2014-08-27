using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class SetFalseCmd : SetBooleanCmd
    {
        public SetFalseCmd(LiteralBatch literal)
            : base(literal, FalseValueExpression.ValueExpression)
        {
        }
    }
}