using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.Integer;

namespace BatchGen.BatchNode
{
    [Serializable]
    public class LiteralBatch : BatchCodeNodeBase
    {
        private LiteralBooleanExpression _literalBooleanCache;
        private LiteralIntegerExpression _literalIntegerCache;
        private LiteralValueExpression _literalValueCache;
        private string _name;

        public LiteralBatch(string name)
        {
            _name = name;
        }

        public LiteralBooleanExpression LiteralBoolean
        {
            get
            {
                if (_literalBooleanCache == null)
                    _literalBooleanCache = new LiteralBooleanExpression(this);
                return _literalBooleanCache;
            }
        }

        public virtual LiteralIntegerExpression LiteralInteger
        {
            get
            {
                if (_literalIntegerCache == null)
                    _literalIntegerCache = new LiteralIntegerExpression(this);
                return _literalIntegerCache;
            }
        }

        public virtual LiteralValueExpression LiteralValue
        {
            get
            {
                if (_literalValueCache == null)
                    _literalValueCache = new LiteralValueExpression(this);
                return _literalValueCache;
            }
        }

        public string Name
        {
            get { return _name; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}