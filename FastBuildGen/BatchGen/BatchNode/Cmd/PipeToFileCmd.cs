using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Cmd
{
    public class PipeToFileCmd : BatchStatementNodeBase
    {
        private BatchCmdBase _cmd;
        private string _filePath;

        public PipeToFileCmd(BatchCmdBase cmd, string filePath)
        {
            _cmd = cmd;
            _filePath = filePath;
        }

        public BatchCmdBase Cmd
        {
            get { return _cmd; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}