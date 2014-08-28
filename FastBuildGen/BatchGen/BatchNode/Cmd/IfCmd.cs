using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Expression;

namespace BatchGen.BatchNode.Cmd
{
    [Serializable]
    public class IfCmd : BatchCmdBase
    {
        private BatchCmdBase _cmd;
        private ConditionalCodeNode _conditionalCode;

        public IfCmd(ConditionalCodeNode conditionalCode, BatchCmdBase cmd)
        {
            _conditionalCode = conditionalCode;
            _cmd = cmd;
        }

        public IfCmd(int errorLevelNumber, BatchCmdBase cmd, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(errorLevelNumber, inverse);
            _cmd = cmd;
        }

        public IfCmd(BatchExpressionBase left, BatchExpressionBase right, BatchCmdBase cmd, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(left, right, inverse);
            _cmd = cmd;
        }

        public IfCmd(LiteralBatch left, BatchExpressionBase right, BatchCmdBase cmd, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(left, right, inverse);
            _cmd = cmd;
        }

        public IfCmd(LiteralBatch left, LiteralBatch right, BatchCmdBase cmd, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(left, right, inverse);
            _cmd = cmd;
        }

        public IfCmd(string fileName, BatchCmdBase cmd, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new ValueExpression(fileName), inverse);
            _cmd = cmd;
        }

        public IfCmd(BatchExpressionBase fileNameExpression, BatchCmdBase cmd, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(fileNameExpression, inverse);
            _cmd = cmd;
        }

        public BatchCmdBase Cmd
        {
            get { return _cmd; }
        }

        public ConditionalCodeNode ConditionalCode
        {
            get { return _conditionalCode; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}