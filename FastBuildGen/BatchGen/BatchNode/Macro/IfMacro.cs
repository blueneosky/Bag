using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.Macro
{
    [Serializable]
    public class IfMacro : BatchStatementNodeBase
    {
        private ConditionalCodeNode _conditionalCode;
        private BatchFileNodeBase _nodeElse;
        private BatchFileNodeBase _nodeThen;

        public IfMacro(ConditionalCodeNode conditionalCode, BlocMacro nodeThen, BlocMacro nodeElse)
        {
            _conditionalCode = conditionalCode;
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public IfMacro(int errorLevelNumber, BlocMacro nodeThen, BlocMacro nodeElse, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(errorLevelNumber, inverse);
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public IfMacro(BatchExpressionBase left, BatchExpressionBase right, BlocMacro nodeThen, BlocMacro nodeElse, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(left, right, inverse);
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public IfMacro(LiteralBatch left, BatchExpressionBase right, BlocMacro nodeThen, BlocMacro nodeElse, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new LiteralValueExpression(left), right, inverse);
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public IfMacro(LiteralBatch left, LiteralBatch right, BlocMacro nodeThen, BlocMacro nodeElse, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new LiteralValueExpression(left), new LiteralValueExpression(right), inverse);
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public IfMacro(string fileName, BlocMacro nodeThen, BlocMacro nodeElse, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new ValueExpression(fileName), inverse);
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public IfMacro(BatchExpressionBase fileNameExpression, BlocMacro nodeThen, BlocMacro nodeElse, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(fileNameExpression, inverse);
            _nodeThen = Wrap(nodeThen);
            _nodeElse = Wrap(nodeElse);
        }

        public ConditionalCodeNode ConditionalCode
        {
            get { return _conditionalCode; }
        }

        public BatchFileNodeBase NodeElse
        {
            get { return _nodeElse; }
        }

        public BatchFileNodeBase NodeThen
        {
            get { return _nodeThen; }
        }

        public static BatchFileNodeBase Wrap(BatchFileNodeBase node)
        {
            if (node == null)
                return null;
            return BlocMacroFileNode.Wrap(node, true);
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}