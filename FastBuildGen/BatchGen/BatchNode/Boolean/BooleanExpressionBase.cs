using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Integer;

namespace BatchGen.BatchNode.Boolean
{
    [Serializable]
    public abstract class BooleanExpressionBase : IntegerExpressionBase
    {
        protected BooleanExpressionBase()
            : base()
        {
        }
    }
}