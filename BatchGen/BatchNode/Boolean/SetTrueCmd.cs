using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public class SetTrueCmd : SetBooleanCmd
    {
        public SetTrueCmd(LiteralBatch literal)
            : base(literal, TrueValueExpression.ValueExpression)
        {
        }
    }
}