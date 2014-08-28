using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.Sub;

namespace BatchGen.BatchNode.Macro
{
    [Serializable]
    public class SubMacro : BatchFileNodeBase
    {
        private LabelSubBatch _label;
        private BlocMacroFileNode _macroSubCache;
        private ExitCmd _returnCmd;
        private BatchStatementNodeBase _subBody;

        public SubMacro(string label, BatchStatementNodeBase subBody, BatchExpressionBase returnValue = null)
        {
            _label = new LabelSubBatch(label);
            _subBody = BlocMacro.Wrap(subBody, true);
            _returnCmd = new ExitCmd(returnValue, true);
        }

        public SubMacro(LabelSubBatch label, BatchStatementNodeBase subBody, BatchExpressionBase returnValue = null)
        {
            _label = label;
            _subBody = BlocMacro.Wrap(subBody, true);
            _returnCmd = new ExitCmd(returnValue, true);
        }

        public SubMacro(string label, BatchExpressionBase returnValue = null)
        {
            _label = new LabelSubBatch(label);
        }

        public LabelSubBatch Label
        {
            get { return _label; }
        }

        public BlocMacroFileNode MacroSub
        {
            get
            {
                if (_macroSubCache == null)
                    _macroSubCache = GetMacroSub();
                return _macroSubCache;
            }
        }

        public ExitCmd ReturnCmd
        {
            get { return _returnCmd; }
        }

        public BatchStatementNodeBase SubBody
        {
            get { return _subBody; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }

        private BlocMacroFileNode GetMacroSub()
        {
            BlocMacroFileNode macro = new BlocMacroFileNode();

            macro.Add(Label);
            macro.Add(SubBody);
            macro.Add(ReturnCmd);

            return macro;
        }
    }
}