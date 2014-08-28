using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BatchGen.BatchNode;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.ExternCmd;
using BatchGen.BatchNode.Goto;
using BatchGen.BatchNode.Integer;
using BatchGen.BatchNode.Macro;
using BatchGen.BatchNode.Sub;
using BatchGen.Common;

namespace BatchGen.Gen
{
    internal class BatchCmdGenVisitor : IBatchNodeVisitor, IDisposable
    {
        private bool _echoEnabled;
        private int _nbTab;
        private Stream _stream;
        private bool _tabSpaceEnabled;
        private bool _withEchoOff;
        private StreamWriter _writer;

        public BatchCmdGenVisitor(Stream stream, int nbTab = 0, bool withEchoOff = true)
        {
            _stream = stream;
            _writer = new ProxyStreamWriter(_stream);
            _nbTab = nbTab;
            _withEchoOff = withEchoOff;
            EnableTab();
            EnableEcho();
        }

        public Stream BaseStream
        {
            get { return _stream; }
        }

        #region Gen helper

        private static Dictionary<EnumIntegerOperator, string> ConstOperatorTextByOperator = new Dictionary<EnumIntegerOperator, string>
        {
            { EnumIntegerOperator.Aucun, "" },
            { EnumIntegerOperator.Add, "+" },
            { EnumIntegerOperator.BitAnd, "&" },
            { EnumIntegerOperator.BitOr, "|" },
            { EnumIntegerOperator.BitXor, "^" },
            { EnumIntegerOperator.Div, "/" },
            { EnumIntegerOperator.LeftDec, "<<" },
            { EnumIntegerOperator.Mod, "%" },
            { EnumIntegerOperator.Mult, "*" },
            { EnumIntegerOperator.RightDec, ">>" },
            { EnumIntegerOperator.Sub, "-" },
        };

        private static Dictionary<EnumUnaryIntegerOperator, string> ConstUnaryOperatorTextByUnaryOperator = new Dictionary<EnumUnaryIntegerOperator, string>{
            { EnumUnaryIntegerOperator.Aucun, "" },
            { EnumUnaryIntegerOperator.Not, "!" },
            { EnumUnaryIntegerOperator.Neg, "-" },
            { EnumUnaryIntegerOperator.Comp1, "~" },
        };

        private void Append(string text)
        {
            _writer.Write(text);
        }

        private void Append(int nb)
        {
            _writer.Write(nb);
        }

        private void AppendBatchNodesBloc(IEnumerable<BatchFileNodeBase> nodesBloc)
        {
            nodesBloc = GetExpandedBlocMacro(nodesBloc as BatchFileNodeBase);

            string tabSpace = GetTab();
            bool isNotFirst = false;
            foreach (BatchFileNodeBase node in nodesBloc)
            {
                IncTabClass incTab = node as IncTabClass;
                if (incTab != null)
                {
                    _nbTab += incTab.TabValue;
                    tabSpace = GetTab();
                }
                else
                {
                    if (isNotFirst)
                    {
                        AppendLine();
                    }
                    else
                    {
                        isNotFirst = true;
                    }
                    Append(tabSpace);
                    node.Accept(this);
                }
            }
        }

        private void AppendCmd(string cmd)
        {
            Append((_withEchoOff || !_echoEnabled ? String.Empty : ConstBatchGen.ConstCmdSwitchEchoOffPrefix));
            Append(cmd);
        }

        private void AppendCmd(string cmd, string text, bool withSpace = true)
        {
            if (String.IsNullOrEmpty(text))
            {
                AppendCmd(cmd);
            }
            else
            {
                AppendCmd(cmd);
                Append((withSpace ? " " : String.Empty));
                Append(text);
            }
        }

        private void AppendCmdFormat(string cmd, string format, params object[] args)
        {
            if (String.IsNullOrEmpty(format))
            {
                AppendCmd(cmd);
            }
            else
            {
                AppendCmd(cmd);
                Append(" ");
                AppendFormat(format, args);
            }
        }

        private void AppendFormat(string format, object arg0)
        {
            _writer.Write(format, arg0);
        }

        private void AppendFormat(string format, params object[] args)
        {
            _writer.Write(format, args);
        }

        private void AppendGotoLabel(string label)
        {
            Append(ConstBatchGen.ConstGotoLabelPrefix);
            Append(label);
        }

        private void AppendLine()
        {
            _writer.WriteLine();
        }

        private void DisableEcho()
        {
            _echoEnabled = false;
        }

        private void DisableTab()
        {
            _tabSpaceEnabled = false;
        }

        private void EnableEcho()
        {
            _echoEnabled = true;
        }

        private void EnableTab()
        {
            _tabSpaceEnabled = true;
        }

        private IEnumerable<BatchFileNodeBase> GetExpandedBlocMacro(BatchFileNodeBase node)
        {
            BlocMacro blocMacro = node as BlocMacro;
            bool withIncTab = (blocMacro != null) && blocMacro.WithIncTab;
            IEnumerable<BatchFileNodeBase> blocNodes = blocMacro;

            if (blocNodes == null)
            {
                BlocMacroFileNodeBase blocMacroFileNodeBase = node as BlocMacroFileNodeBase;
                withIncTab = (blocMacroFileNodeBase != null) && blocMacroFileNodeBase.WithIncTab;
                blocNodes = blocMacroFileNodeBase;
            }

            if (blocNodes != null)
            {
                if (withIncTab)
                    yield return new IncTabClass(+1);

                foreach (BatchFileNodeBase subNode in blocNodes.SelectMany(n => GetExpandedBlocMacro(n)))
                {
                    yield return subNode;
                }

                if (withIncTab)
                    yield return new IncTabClass(-1);
            }
            else
            {
                yield return node;
            }
        }

        private string GetIfTestValue(int errorLevelNumber, bool inverse)
        {
            string result = (inverse) ? (ConstBatchGen.ConstKeywordNot + " ") : String.Empty;
            result += ConstBatchGen.ConstKeywordErrorLevel + " " + errorLevelNumber;

            return result;
        }

        private string GetIfTestValue(BatchExpressionBase fileNameExpression, bool inverse)
        {
            string result = (inverse) ? (ConstBatchGen.ConstKeywordNot + " ") : String.Empty;
            result += ConstBatchGen.ConstKeywordExist + " " + GetValue(fileNameExpression);

            return result;
        }

        private string GetIfTestValue(BatchExpressionBase left, BatchExpressionBase right, bool inverse)
        {
            string result = (inverse) ? (ConstBatchGen.ConstKeywordNot + " ") : String.Empty;
            result += String.Format("({0}) {1} ({2})"
                , GetValue(left)
                , ConstBatchGen.ConstKeywordEquality
                , GetValue(right)
            );

            return result;
        }

        private string GetTab()
        {
            string result;
            if (_tabSpaceEnabled)
            {
                result = Tools.GetTab(_nbTab);
            }
            else
            {
                result = String.Empty;
            }

            return result;
        }

        private string GetValue(BatchCodeNodeBase node)
        {
            object value = node.Accept(this);
            string result = value as string;

            return result;
        }

        private class IncTabClass : BatchFileNodeBase
        {
            public IncTabClass(int tabValue)
            {
                TabValue = tabValue;
            }

            public int TabValue { get; private set; }

            public override object Accept(IBatchNodeVisitor visitor)
            {
                throw new BatchGenException("Not permitted");
            }
        }

        #endregion Gen helper

        #region IBatchCmdVisitor

        public object Visit(BatchFileBase batchFileBase)
        {
            // echo off first line
            _withEchoOff = batchFileBase.WithEchoOff;
            if (_withEchoOff)
            {
                AppendCmd(ConstBatchGen.ConstCmdEchoParamOff, null);
                AppendLine();
                AppendLine();
            }

            IEnumerable<BatchFileNodeBase> nodes = batchFileBase;
            string tabSpace = GetTab();
            foreach (BatchFileNodeBase node in nodes)
            {
                Append(tabSpace);
                node.Accept(this);
                AppendLine();
            }

            return null;
        }

        public object Visit(EchoCmd cmdEcho)
        {
            string text = GetValue(cmdEcho.Expression);
            if (String.IsNullOrEmpty(text))
            {
                AppendCmd(ConstBatchGen.ConstCmdEcho, ConstBatchGen.ConstCmdEchoParamEmptyText, false);
            }
            else if (text.TrimStart().StartsWith("/?"))
            {
                AppendCmd(ConstBatchGen.ConstCmdEcho + ".", text, false);
            }
            else
            {
                AppendCmd(ConstBatchGen.ConstCmdEcho, text);
            }

            return null;
        }

        public object Visit(RemBatch cmdRem)
        {
            Append(ConstBatchGen.ConstCmdRem);
            string comment = cmdRem.Comment;
            if (false == String.IsNullOrEmpty(comment))
            {
                Append(" ");
                Append(comment);
            }

            return null;
        }

        public object Visit(SetExpressionCmd cmdSet)
        {
            string literal = GetValue(cmdSet.Literal);
            string value = GetValue(cmdSet.Expression);
            AppendCmdFormat(ConstBatchGen.ConstCmdSet, "{0}={1}", literal, value);

            return null;
        }

        public object Visit(TitleCmd cmdTitle)
        {
            string text = GetValue(cmdTitle.Expression);
            AppendCmd(ConstBatchGen.ConstCmdTitle, text);

            return null;
        }

        public object Visit(CdCmd cmdCd)
        {
            string text = GetValue(cmdCd.PathExpression);
            AppendCmd(ConstBatchGen.ConstCmdCd, text);

            return null;
        }

        public object Visit(BlocMacro macroBloc)
        {
            AppendBatchNodesBloc(macroBloc);

            return null;
        }

        public object Visit(BlocMacroFileNodeBase macroBloc)
        {
            AppendBatchNodesBloc(macroBloc);

            return null;
        }

        public object Visit(IfMacro macroIf)
        {
            string text = GetValue(macroIf.ConditionalCode);
            AppendCmd(ConstBatchGen.ConstCmdIf, text);

            // then bloc
            Append(" (");
            AppendLine();
            macroIf.NodeThen.Accept(this);
            AppendLine();
            AppendFormat("{0})", GetTab());

            // else bloc
            if (macroIf.NodeElse != null)
            {
                Append(ConstBatchGen.ConstKeywordElse);
                Append("(");
                AppendLine();
                macroIf.NodeElse.Accept(this);
                AppendLine();
                AppendFormat("{0})", GetTab());
            }

            return null;
        }

        public object Visit(IfCmd cmdIf)
        {
            string text = GetValue(cmdIf.ConditionalCode);
            AppendCmd(ConstBatchGen.ConstCmdIf, text);

            Append(" (");
            DisableTab();
            DisableEcho();
            cmdIf.Cmd.Accept(this);
            EnableEcho();
            EnableTab();
            Append(")");

            return null;
        }

        public object Visit(NopBatch batchNop)
        {
            return null;
        }

        public object Visit(GotoCmd cmdGoto)
        {
            AppendCmd(ConstBatchGen.ConstCmdGoto, cmdGoto.Label.Label);

            return null;
        }

        public object Visit(GotoEofCmd gotoEofCmd)
        {
            AppendCmd(ConstBatchGen.ConstCmdGoto, ConstBatchGen.ConstCmdLabelGotoEof);

            return null;
        }

        public object Visit(CallSubCmd callCmd)
        {
            string text = String.Format(":{0}", callCmd.Label.Label);
            AppendCmd(ConstBatchGen.ConstCmdCall, text);

            return null;
        }

        public object Visit(PauseCmd pauseCmd)
        {
            AppendCmd(ConstBatchGen.ConstCmdPause);

            return null;
        }

        public object Visit(LiteralBatch variableBatch)
        {
            return variableBatch.Name;
        }

        public object Visit(LiteralValueExpression literalValueBatch)
        {
            return String.Format("%{0}%", GetValue(literalValueBatch.Literal));
        }

        public object Visit(LiteralIntegerExpression literalIntegerExpression)
        {
            return String.Format("%{0}%", GetValue(literalIntegerExpression.Literal));
        }

        public object Visit(LiteralBooleanExpression literalBooleanExpression)
        {
            return String.Format("%{0}%", GetValue(literalBooleanExpression.Literal));
        }

        public object Visit(ValueExpression valueExpression)
        {
            return valueExpression.Value;
        }

        public object Visit(IntegerValueExpression integerValueExpression)
        {
            return integerValueExpression.IntegerValue.ToString();
        }

        public object Visit(ShiftCmd shiftCmd)
        {
            AppendCmd(ConstBatchGen.ConstCmdShift);

            return null;
        }

        public object Visit(SetlocalCmd setlocalCmd)
        {
            AppendCmd(ConstBatchGen.ConstCmdSetlocal);

            return null;
        }

        public object Visit(ExitCmd exitCmd)
        {
            AppendCmd(ConstBatchGen.ConstCmdExit);

            if (exitCmd.ExitScript)
                Append(ConstBatchGen.ConstCmdExitParamExitScript);

            BatchExpressionBase returnExpression = exitCmd.ReturnExpression;
            if (returnExpression != null)
            {
                Append(" ");
                Append(GetValue(returnExpression));
            }

            return null;
        }

        public object Visit(SubMacro subMacro)
        {
            subMacro.MacroSub.Accept(this);

            return null;
        }

        public object Visit(LabelSubBatch labelSubBatch)
        {
            AppendGotoLabel(labelSubBatch.Label);

            return null;
        }

        public object Visit(LabelGotoBatch labelGotoBatch)
        {
            AppendGotoLabel(labelGotoBatch.Label);

            return null;
        }

        public object Visit(ComposedExpression composedExpression)
        {
            return String.Concat(composedExpression.Expressions.Select(e => e.Accept(this).ToString()));
        }

        public object Visit(CallBatchCmd callBatchCmd)
        {
            string text = GetValue(callBatchCmd.PathExpression);
            AppendCmd(ConstBatchGen.ConstCmdCall, text);

            return null;
        }

        public object Visit(ConditionalCodeNode conditionalCodeNode)
        {
            string result;

            if (conditionalCodeNode.IsErrorLevelTest)
            {
                result = GetIfTestValue(conditionalCodeNode.ErrorLevelNumber, conditionalCodeNode.Inverse);
            }
            else if (conditionalCodeNode.IsFileNameTest)
            {
                result = GetIfTestValue(conditionalCodeNode.FileNameExpression, conditionalCodeNode.Inverse);
            }
            else
            {
                result = GetIfTestValue(conditionalCodeNode.Left, conditionalCodeNode.Right, conditionalCodeNode.Inverse);
            }

            return result;
        }

        public object Visit(PipeToFileCmd pipeToFileCmd)
        {
            pipeToFileCmd.Cmd.Accept(this);
            AppendFormat(" >> {0}", pipeToFileCmd.FilePath);

            return null;
        }

        public object Visit(BatchNode.ExternCmd.MSBuildCliMacro mSBuildCmd)
        {
            mSBuildCmd.MacroMSBuild.Accept(this);

            return null;
        }

        public object Visit(BatchNode.ExternCmd.MSBuildCmd mSBuildCmd)
        {
            string cli = GetValue(mSBuildCmd.CliExpression);
            AppendCmd(ConstBatchGen.ConstCmdMSBuild, cli);

            return null;
        }

        public object Visit(TaskKillCmd taskKillCmd)
        {
            List<string> clis = new List<string> { };
            if (taskKillCmd.System != null)
                clis.Add(String.Format("/S {0}", GetValue(taskKillCmd.System)));
            if (taskKillCmd.User != null)
                clis.Add(String.Format("/U {0}", GetValue(taskKillCmd.User)));
            if (taskKillCmd.PassWord != null)
                clis.Add(String.Format("/P {0}", GetValue(taskKillCmd.PassWord)));
            if (taskKillCmd.WithChildProcess != null)
                clis.Add("/T");
            if (taskKillCmd.Force != null)
                clis.Add("/F");
            clis.Add(String.Format("/IM {0}", GetValue(taskKillCmd.ImageName)));
            string cli = clis.Aggregate((seed, acc) => seed + " " + acc);
            AppendCmd(ConstBatchGen.ConstCmdTaskKill, cli);

            return null;
        }

        public object Visit(IntegerUnaryOperatorExpression integerUnaryOperatorExpression)
        {
            string operatorText = ConstUnaryOperatorTextByUnaryOperator[integerUnaryOperatorExpression.UnaryOperator];
            string expressionText = GetValue(integerUnaryOperatorExpression.IntegerExpression);
            string result = String.Format("({0}{1})", operatorText, expressionText);

            return result;
        }

        public object Visit(IntegerOperatorExpression integerOperatorExpression)
        {
            string operatorText = ConstOperatorTextByOperator[integerOperatorExpression.Operator];
            string expresionText = integerOperatorExpression.IntegerExpressions
                .Select(ie => GetValue(ie))
                .Aggregate((acc, e) => String.Concat(acc, operatorText, e));
            string result = String.Format("({0})", expresionText);

            return result;
        }

        public object Visit(BooleanOperatorExpression booleanOperatorExpression)
        {
            string operatorText = ConstOperatorTextByOperator[(EnumIntegerOperator)booleanOperatorExpression.Operator];
            string expresionText = booleanOperatorExpression.BooleanExpressions
                .Select(ie => GetValue(ie))
                .Aggregate((acc, e) => String.Concat(acc, operatorText, e));
            string result = String.Format("({0})", expresionText);

            return result;
        }

        public object Visit(NotBooleanExpression notBooleanExpression)
        {
            string operatorText = ConstUnaryOperatorTextByUnaryOperator[EnumUnaryIntegerOperator.Not];
            string expresionText = GetValue(notBooleanExpression.BooleanExpression);
            string result = String.Format("({0}{1})", operatorText, expresionText);

            return result;
        }

        public object Visit(SetIntegerCmd setIntegerCmd)
        {
            string literal = GetValue(setIntegerCmd.Literal);
            string expresionText = GetValue(setIntegerCmd.IntegerExpression);

            AppendCmdFormat(ConstBatchGen.ConstCmdSet, "/A {0}=\"{1}\"", literal, expresionText);

            return null;
        }

        public object Visit(BooleanValueExpressionBase booleanValueExpressionBase)
        {
            return booleanValueExpressionBase.BooleanValue ? ConstBatchGen.ConstKeywordTrueValue : ConstBatchGen.ConstKeywordFalseValue;
        }

        public object Visit(SGenPlusCmd sGenPlusCmd)
        {
            string cliText = GetValue(sGenPlusCmd.CliExpression);
            AppendCmd(ConstBatchGen.ConstCmdSGenPlus, cliText);

            return null;
        }

        #endregion IBatchCmdVisitor

        #region IDisposable

        ~BatchCmdGenVisitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_stream != null)
            {
                if (disposing)
                {
                    try
                    {
                        _writer.Dispose();
                    }
                    finally
                    {
                        _writer = null;
                    }
                    _stream = null;
                }
            }
        }

        #endregion IDisposable
    }
}