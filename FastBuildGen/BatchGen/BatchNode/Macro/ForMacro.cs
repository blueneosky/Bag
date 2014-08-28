using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.Goto;
using BatchGen.Gen;

namespace BatchGen.BatchNode.Macro
{
    [Serializable]
    public class ForMacro : BlocMacroFileNodeBase
    {
        private string _gotoBaseLabel;
        private string _labelGotoEndForPrefix;
        private string _labelGotoForPrefix;
        private BatchFileNodeBase _loopBody;
        private BatchStatementNodeBase _loopInit;
        private BatchStatementNodeBase _loopStatement;
        private ConditionalCodeNode _loopTest;
        private IEnumerable<BatchFileNodeBase> _macroForCache;

        public ForMacro(BatchStatementNodeBase loopInit, ConditionalCodeNode loopTest, BatchStatementNodeBase loopStatement, BatchFileNodeBase loopBody, string gotoBaseLabel)
        {
            _loopInit = loopInit;
            _loopTest = loopTest;
            _loopStatement = loopStatement;
            _loopBody = BlocMacroFileNode.Wrap(loopBody, true);
            _gotoBaseLabel = gotoBaseLabel;

            _labelGotoForPrefix = ConstBatchGen.ConstGotoForPrefix;
            _labelGotoEndForPrefix = ConstBatchGen.ConstGotoEndForPrefix;
        }

        public ForMacro(LiteralBatch initLiteral, BatchExpressionBase initExpression, BooleanExpressionBase loopTest, BatchStatementNodeBase loopStatement, BatchFileNodeBase loopBody, string gotoBaseLabel)
        {
            _loopInit = new SetExpressionCmd(initLiteral, initExpression);
            _loopTest = new IsTrueConditional(loopTest);
            _loopStatement = loopStatement;
            _loopBody = BlocMacroFileNode.Wrap(loopBody, true);
            _gotoBaseLabel = gotoBaseLabel;

            _labelGotoForPrefix = ConstBatchGen.ConstGotoForPrefix;
            _labelGotoEndForPrefix = ConstBatchGen.ConstGotoEndForPrefix;
        }

        protected ForMacro(BatchStatementNodeBase loopInit, ConditionalCodeNode loopTest, BatchStatementNodeBase loopStatement, BatchFileNodeBase loopBody, string gotoBaseLabel, string labelGotoForPrefix = ConstBatchGen.ConstGotoForPrefix, string labelGotoEndForPrefix = ConstBatchGen.ConstGotoEndForPrefix)
        {
            _loopInit = loopInit;
            _loopTest = loopTest;
            _loopStatement = loopStatement;
            _loopBody = BlocMacroFileNode.Wrap(loopBody, true);
            _gotoBaseLabel = gotoBaseLabel;

            _labelGotoForPrefix = labelGotoForPrefix;
            _labelGotoEndForPrefix = labelGotoEndForPrefix;
        }

        public string GotoBaseLabel
        {
            get { return _gotoBaseLabel; }
        }

        public BatchFileNodeBase LoopBody
        {
            get { return _loopBody; }
        }

        public BatchStatementNodeBase LoopInit
        {
            get { return _loopInit; }
        }

        public BatchStatementNodeBase LoopStatement
        {
            get { return _loopStatement; }
        }

        public ConditionalCodeNode LoopTest
        {
            get { return _loopTest; }
        }

        public IEnumerable<BatchFileNodeBase> MacroFor
        {
            get
            {
                if (_macroForCache == null)
                    _macroForCache = GetMacroFor();
                return _macroForCache;
            }
        }

        public override IEnumerator<BatchFileNodeBase> GetEnumerator()
        {
            return MacroFor.GetEnumerator();
        }

        private IEnumerable<BatchFileNodeBase> GetMacroFor()
        {
            List<BatchFileNodeBase> macro = new List<BatchFileNodeBase>();

            bool withInitNode = LoopInit != null;
            bool withConditional = LoopTest != null;
            bool withLoopStatement = LoopStatement != null;

            string gotoBaseLabel = GotoBaseLabel;
            string labelGotoFor = _labelGotoForPrefix + gotoBaseLabel;
            string labelGotoEndFor = _labelGotoEndForPrefix + gotoBaseLabel;
            LabelGotoBatch labelFor = new LabelGotoBatch(labelGotoFor);
            LabelGotoBatch labelEndFor = new LabelGotoBatch(labelGotoEndFor);
            GotoCmd gotoFor = new GotoCmd(labelFor);
            GotoCmd gotoEndFor = new GotoCmd(labelEndFor);

            // <init>
            if (withInitNode)
                macro.Add(LoopInit);

            // :For label
            macro.Add(labelFor);

            // <test> => goto EndFor
            if (withConditional)
            {
                ConditionalCodeNode forIfTest = new NotConditionalCodeNode(LoopTest);
                IfCmd forIfLine = new IfCmd(forIfTest, gotoEndFor);
                macro.Add(forIfLine);
            }

            // <macro>
            macro.Add(LoopBody);

            // <loopStatement>
            if (withLoopStatement)
                macro.Add(LoopStatement);

            // goto For
            macro.Add(gotoFor);

            // :EndFor label
            if (withConditional)
                macro.Add(labelEndFor);

            return macro;
        }
    }
}