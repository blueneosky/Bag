using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.Goto;
using BatchGen.Gen;

namespace BatchGen.BatchNode.Macro
{
    [Serializable]
    public class IfGotoMacro : BlocMacroFileNodeBase
    {
        private ConditionalCodeNode _conditionalCode;
        private string _gotoBaseLabel;
        private IEnumerable<BatchFileNodeBase> _macroIfGotoCache;
        private BatchFileNodeBase _nodeElse;
        private BatchFileNodeBase _nodeThen;

        public IfGotoMacro(ConditionalCodeNode conditionalCode, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel)
        {
            _conditionalCode = conditionalCode;
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public IfGotoMacro(int errorLevelNumber, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(errorLevelNumber, inverse);
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public IfGotoMacro(BatchExpressionBase left, BatchExpressionBase right, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(left, right, inverse);
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public IfGotoMacro(LiteralBatch left, BatchExpressionBase right, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new LiteralValueExpression(left), right, inverse);
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public IfGotoMacro(LiteralBatch left, LiteralBatch right, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new LiteralValueExpression(left), new LiteralValueExpression(right), inverse);
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public IfGotoMacro(string fileName, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(new ValueExpression(fileName), inverse);
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public IfGotoMacro(BatchExpressionBase fileNameExpression, BatchFileNodeBase nodeThen, BatchFileNodeBase nodeElse, string gotoBaseLabel, bool inverse = false)
        {
            _conditionalCode = new ConditionalCodeNode(fileNameExpression, inverse);
            _nodeThen = nodeThen;
            _nodeElse = nodeElse;
            _gotoBaseLabel = gotoBaseLabel;
        }

        public ConditionalCodeNode ConditionalCode
        {
            get { return _conditionalCode; }
        }

        public string GotoBaseLabel
        {
            get { return _gotoBaseLabel; }
        }

        public IEnumerable<BatchFileNodeBase> MacroIfGoto
        {
            get
            {
                if (_macroIfGotoCache == null)
                    _macroIfGotoCache = GetMacroIfGoto();
                return _macroIfGotoCache;
            }
        }

        public BatchFileNodeBase NodeElse
        {
            get { return _nodeElse; }
        }

        public BatchFileNodeBase NodeThen
        {
            get { return _nodeThen; }
        }

        public override IEnumerator<BatchFileNodeBase> GetEnumerator()
        {
            return MacroIfGoto.GetEnumerator();
        }

        private IEnumerable<BatchFileNodeBase> GetMacroIfGoto()
        {
            List<BatchFileNodeBase> macro = new List<BatchFileNodeBase>();

            bool withNodeElse = NodeElse != null;
            string labelGotoEndif = ConstBatchGen.ConstGotoBlocEndIfPrefix + GotoBaseLabel;
            string labelGotoElse = ConstBatchGen.ConstGotoBlocElsePrefix + GotoBaseLabel;
            LabelGotoBatch labelEndif = new LabelGotoBatch(labelGotoEndif);
            LabelGotoBatch labelElse = new LabelGotoBatch(labelGotoElse);

            // if line
            GotoCmd gotoIfCmd = new GotoCmd(withNodeElse ? labelElse : labelEndif);
            macro.Add(new IfCmd(new NotConditionalCodeNode(ConditionalCode), gotoIfCmd));

            // then bloc
            macro.Add(BlocMacroFileNode.Wrap(NodeThen, true));

            // else bloc
            if (withNodeElse)
            {
                // jump blof if to endif to skip else
                macro.Add(new GotoCmd(labelEndif));

                // label else
                macro.Add(labelElse);

                macro.Add(BlocMacroFileNode.Wrap(NodeElse, true));
            }

            // endif line
            macro.Add(labelEndif);

            return macro;
        }
    }
}