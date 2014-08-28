using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public class NotConditionalCodeNode : ConditionalCodeNode
    {
        public NotConditionalCodeNode(ConditionalCodeNode conditionalCodeNode)
            : base(
                 conditionalCodeNode.ErrorLevelNumberRaw
                , conditionalCodeNode.LeftRaw
                , conditionalCodeNode.RightRaw
                , conditionalCodeNode.FileNameExpressionRaw
                , !(conditionalCodeNode.InverseRaw)
            )
        {
        }
    }
}