﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.EnvSystem;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.ExternCmd;
using BatchGen.BatchNode.Goto;
using BatchGen.BatchNode.Macro;
using BatchGen.BatchNode.Sub;
using FastBuildGen.BusinessModel;
using FastBuildGen.BusinessModel.Extension;
using System.Diagnostics;

namespace FastBuildGen.BatchNode
{
    [Serializable]
    internal class FastBuildBatchFile : BatchFileBase
    {
        #region Debug

#if DEBUG
        private const bool ConstDebug = true;

#else
        private const bool ConstDebug = false;
#endif

        private BatchStatementNodeBase GetDebugRem(string comment)
        {
            BatchStatementNodeBase result;

#if DEBUG
            result = new RemBatch("FastBuildBatchFile." + comment);
#else
                result = new BlocMacro();
#endif

            return result;
        }

        #endregion Debug

        private FBModel _fbModel;

        public FastBuildBatchFile(FBModel fbModel)
            : base(fbModel.WithEchoOff)
        {
            _fbModel = fbModel;

            FileNodes = GetFileContent();
        }

        private FastBuildBatchFile()
        {
            // for Serialization
        }

        public IEnumerable<BatchFileNodeBase> FileNodes { get; private set; }

        public override IEnumerator<BatchFileNodeBase> GetEnumerator()
        {
            return FileNodes.GetEnumerator();
        }

        private IEnumerable<BatchFileNodeBase> GetFileContent()
        {
            // Help reminder (comment)
            yield return HelpSummerComment;
            yield return Nop;

            // Script Initialisation
            yield return ScriptInitialisation;
            yield return Nop;

            // Parameters analyse
            yield return ParametersAnalyse;
            yield return Nop;

            // FastBuild Core
            yield return FastBuildCore;
            yield return Nop;

            // Sub routine and macro
            yield return new GotoCmd(LabelGotoEnd);
            yield return Nop;

            // Help bloc
            yield return VersionBloc;
            yield return Nop;

            // Help bloc
            yield return HelpBloc;
            yield return Nop;

            // Sub Functions
            yield return RemSeparator;
            yield return Nop;
            yield return SubFunctions;
            yield return Nop;

            yield return RemSeparator;
            yield return Nop;
            yield return LabelGotoEnd;
            yield return new TitleCmd("Dos");

            yield return new GotoEofCmd();
        }

        #region Comon

        private NopBatch _nopCache;
        private RemBatch _remSeparatorCache;

        public NopBatch Nop
        {
            get
            {
                if (_nopCache == null)
                    _nopCache = new NopBatch();
                return _nopCache;
            }
        }

        public RemBatch RemSeparator
        {
            get
            {
                if (_remSeparatorCache == null)
                    _remSeparatorCache = new RemBatch(_fbModel.LabelTextRemSeparator());
                return _remSeparatorCache;
            }
        }

        #region FastBuild Property wraper

        private IEnumerable<IEnumerable<FBTarget>> AllTargets
        {
            get
            {
                yield return BaseTargets;
                yield return SolutionTargets;
                yield return MacroSolutionTargets;
            }
        }

        private IEnumerable<FBTarget> BaseTargets
        {
            get { return Enumerable.Concat(CommonTargets, HeoParamTargets); }
        }

        private IEnumerable<FBTarget> CommonTargets
        {
            get { return _fbModel.ParamTargets; }
        }

        private IEnumerable<FBTarget> HeoParamTargets
        {
            get { return _fbModel.HeoParamTargets; }
        }

        private IEnumerable<FBSolutionTarget> SolutionTargets
        {
            get { return _fbModel.SolutionTargets; }
        }

        private IEnumerable<FBMacroSolutionTarget> MacroSolutionTargets
        {
            get { return _fbModel.MacroSolutionTargets; }
        }

        private FBTarget GetFBTarget(string keyWord)
        {
            FBTarget result = AllTargets
                .SelectMany(e => e)
                .First(pd => pd.Keyword == keyWord);

            return result;
        }

        #endregion FastBuild Property wraper

        #region Literal

        private LiteralBatch _literalConfigurationPathCache;
        private LiteralBatch _literalEnvSystemVcvarsallCheckStatusCache;
        private LiteralBatch _literalHeoLanceurBinPathCache;
        private LiteralBatch _literalHeoLanceurPathCache;
        private Dictionary<string, LiteralBatch> _literalSolutionMSBuildTargetByKeyWordsCache;
        private LiteralBatch _literalMSBuildCliCache;
        private LiteralBatch _literalMSBuildConfigurationCache;
        private LiteralBatch _literalMSBuildLogFileCache;
        private LiteralBatch _literalMSBuildPlatformValue;
        private LiteralBatch _literalMSBuildWithTargetsCache;
        private LiteralBatch _literalMSBuildForcedAllCache;
        private LiteralBatch _literalMSBuildTryLoopCondCache;
        private Dictionary<string, LiteralBatch> _literalTargetByKeyWordsCache;
        private LiteralBatch _literalSGenPlusCliCache;

        private LiteralBatch _literalSGenPlusConfigFilePathCache;

        private LiteralBatch _literalSGenPlusNeedRunCache;

        private LiteralBatch _literalSGenPlusTargetBinaryPathCache;

        private LiteralBatch _literalStartTimeCache;

        private LiteralBatch _literalVersionCache;

        private LiteralBatch _literalVersionNumberCache;

        public LiteralBatch LiteralConfigurationPath
        {
            get
            {
                if (_literalConfigurationPathCache == null)
                    _literalConfigurationPathCache = new LiteralBatch(_fbModel.LiteralConfigurationPath());
                return _literalConfigurationPathCache;
            }
        }

        public LiteralBatch LiteralEnvSystemVcvarsallCheckStatus
        {
            get
            {
                if (_literalEnvSystemVcvarsallCheckStatusCache == null)
                    _literalEnvSystemVcvarsallCheckStatusCache = new LiteralBatch(_fbModel.LiteralEnvSystemVcvarsallCheckStatus());
                return _literalEnvSystemVcvarsallCheckStatusCache;
            }
        }

        public LiteralBatch LiteralHeoLanceurBinPath
        {
            get
            {
                if (_literalHeoLanceurBinPathCache == null)
                    _literalHeoLanceurBinPathCache = new LiteralBatch(_fbModel.LiteralHeoLanceurBinPath());
                return _literalHeoLanceurBinPathCache;
            }
        }

        public LiteralBatch LiteralHeoLanceurPath
        {
            get
            {
                if (_literalHeoLanceurPathCache == null)
                    _literalHeoLanceurPathCache = new LiteralBatch(_fbModel.LiteralHeoLanceurPath());
                return _literalHeoLanceurPathCache;
            }
        }

        public Dictionary<string, LiteralBatch> LiteralSolutionMSBuildTargetByKeyWords
        {
            get
            {
                if (_literalSolutionMSBuildTargetByKeyWordsCache == null)
                {
                    _literalSolutionMSBuildTargetByKeyWordsCache = SolutionTargets
                        .ToDictionary(
                            pd => pd.Keyword
                            , pd => new LiteralBatch(pd.VarName)
                        );
                }
                return _literalSolutionMSBuildTargetByKeyWordsCache;
            }
        }

        public LiteralBatch LiteralMSBuildCli
        {
            get
            {
                if (_literalMSBuildCliCache == null)
                    _literalMSBuildCliCache = new LiteralBatch(_fbModel.LiteralMSBuildCli());
                return _literalMSBuildCliCache;
            }
        }

        public LiteralBatch LiteralMSBuildConfiguration
        {
            get
            {
                if (_literalMSBuildConfigurationCache == null)
                    _literalMSBuildConfigurationCache = new LiteralBatch(_fbModel.LiteralMSBuildConfiguration());
                return _literalMSBuildConfigurationCache;
            }
        }

        public LiteralBatch LiteralMSBuildLogFile
        {
            get
            {
                if (_literalMSBuildLogFileCache == null)
                    _literalMSBuildLogFileCache = new LiteralBatch(_fbModel.LiteralMSBuildLogFile());
                return _literalMSBuildLogFileCache;
            }
        }

        public LiteralBatch LiteralMSBuildPlatform
        {
            get
            {
                if (_literalMSBuildPlatformValue == null)
                    _literalMSBuildPlatformValue = new LiteralBatch(_fbModel.LiteralMSBuildPlatform());
                return _literalMSBuildPlatformValue;
            }
        }

        public LiteralBatch LiteralMSBuildWithTargets
        {
            get
            {
                if (_literalMSBuildWithTargetsCache == null)
                    _literalMSBuildWithTargetsCache = new LiteralBatch(_fbModel.LiteralMSBuildWithTargets());
                return _literalMSBuildWithTargetsCache;
            }
        }

        public LiteralBatch LiteralMSBuildForcedAll
        {
            get
            {
                if (_literalMSBuildForcedAllCache == null)
                    _literalMSBuildForcedAllCache = new LiteralBatch(_fbModel.LiteralMSBuildForcedAll());
                return _literalMSBuildForcedAllCache;
            }
        }

        public LiteralBatch LiteralMSBuildTryLoopCond
        {
            get
            {
                if (_literalMSBuildTryLoopCondCache == null)
                    _literalMSBuildTryLoopCondCache = new LiteralBatch(_fbModel.LiteralMSBuildTryLoopCond());
                return _literalMSBuildTryLoopCondCache;
            }
        }

        public Dictionary<string, LiteralBatch> LiteralTargetByKeyWords
        {
            get
            {
                if (_literalTargetByKeyWordsCache == null)
                {
                    _literalTargetByKeyWordsCache = AllTargets
                        .SelectMany(e => e)
                        .ToDictionary(
                            pd => pd.Keyword
                            , pd => new LiteralBatch(pd.ParamVarName)
                        )
                        ;
                }
                return _literalTargetByKeyWordsCache;
            }
        }

        public LiteralBatch LiteralSGenPlusCli
        {
            get
            {
                if (_literalSGenPlusCliCache == null)
                    _literalSGenPlusCliCache = new LiteralBatch(_fbModel.LiteralSGenPlusCli());
                return _literalSGenPlusCliCache;
            }
        }

        public LiteralBatch LiteralSGenPlusConfigFilePath
        {
            get
            {
                if (_literalSGenPlusConfigFilePathCache == null)
                    _literalSGenPlusConfigFilePathCache = new LiteralBatch(_fbModel.LiteralSGenPlusConfigFilePath());
                return _literalSGenPlusConfigFilePathCache;
            }
        }

        public LiteralBatch LiteralSGenPlusNeedRun
        {
            get
            {
                if (_literalSGenPlusNeedRunCache == null)
                    _literalSGenPlusNeedRunCache = new LiteralBatch(_fbModel.LiteralSGenPlusNeedRun());
                return _literalSGenPlusNeedRunCache;
            }
        }

        public LiteralBatch LiteralSGenPlusTargetBinaryPath
        {
            get
            {
                if (_literalSGenPlusTargetBinaryPathCache == null)
                    _literalSGenPlusTargetBinaryPathCache = new LiteralBatch(_fbModel.LiteralSGenPlusTargetBinaryPath());
                return _literalSGenPlusTargetBinaryPathCache;
            }
        }

        public LiteralBatch LiteralStartTime
        {
            get
            {
                if (_literalStartTimeCache == null)
                    _literalStartTimeCache = new LiteralBatch(_fbModel.LiteralStartTime());
                return _literalStartTimeCache;
            }
        }

        public LiteralBatch LiteralVersion
        {
            get
            {
                if (_literalVersionCache == null)
                    _literalVersionCache = new LiteralBatch(_fbModel.LiteralVersionName());
                return _literalVersionCache;
            }
        }

        public LiteralBatch LiteralVersionNumber
        {
            get
            {
                if (_literalVersionNumberCache == null)
                    _literalVersionNumberCache = new LiteralBatch(_fbModel.LiteralVersionNumberName());
                return _literalVersionNumberCache;
            }
        }

        #endregion Literal

        #region Label Goto

        private LabelGotoBatch _labelGotoEndCache;
        private LabelGotoBatch _labelGotoHelpCache;
        private LabelGotoBatch _labelGotoVersionCache;

        public LabelGotoBatch LabelGotoEnd
        {
            get
            {
                if (_labelGotoEndCache == null)
                    _labelGotoEndCache = new LabelGotoBatch(_fbModel.LabelGotoEnd());
                return _labelGotoEndCache;
            }
        }

        public LabelGotoBatch LabelGotoHelp
        {
            get
            {
                if (_labelGotoHelpCache == null)
                    _labelGotoHelpCache = new LabelGotoBatch(_fbModel.LabelGotoHelp());
                return _labelGotoHelpCache;
            }
        }

        public LabelGotoBatch LabelGotoVersion
        {
            get
            {
                if (_labelGotoVersionCache == null)
                    _labelGotoVersionCache = new LabelGotoBatch(_fbModel.LabelGotoVersion());
                return _labelGotoVersionCache;
            }
        }

        #endregion Label Goto

        #region Goto

        private GotoCmd _gotoEndCache;

        public GotoCmd GotoEnd
        {
            get
            {
                if (_gotoEndCache == null)
                    _gotoEndCache = new GotoCmd(LabelGotoEnd);
                return _gotoEndCache;
            }
        }

        #endregion Goto

        #region Label Sub

        private LabelSubBatch _labelSubKillHeoCache;
        private LabelSubBatch _labelSubKillHeoVsHostCache;

        public LabelSubBatch LabelSubKillHeo
        {
            get
            {
                if (_labelSubKillHeoCache == null)
                    _labelSubKillHeoCache = new LabelSubBatch(_fbModel.LabelSubKillHeo());
                return _labelSubKillHeoCache;
            }
        }

        public LabelSubBatch LabelSubKillHeoVsHost
        {
            get
            {
                if (_labelSubKillHeoVsHostCache == null)
                    _labelSubKillHeoVsHostCache = new LabelSubBatch(_fbModel.LabelSubKillHeoVsHost());
                return _labelSubKillHeoVsHostCache;
            }
        }

        #endregion Label Sub

        #region Call Sub

        private CallSubCmd _callSubKillHeoCache;
        private CallSubCmd _callSubKillHeoVsHostCache;

        public CallSubCmd CallSubKillHeo
        {
            get
            {
                if (_callSubKillHeoCache == null)
                    _callSubKillHeoCache = new CallSubCmd(LabelSubKillHeo);
                return _callSubKillHeoCache;
            }
        }

        public CallSubCmd CallSubKillHeoVsHost
        {
            get
            {
                if (_callSubKillHeoVsHostCache == null)
                    _callSubKillHeoVsHostCache = new CallSubCmd(LabelSubKillHeoVsHost);
                return _callSubKillHeoVsHostCache;
            }
        }

        #endregion Call Sub

        #endregion Comon

        #region Help summer comment

        private IEnumerable<string> _helpLinesCache;

        public IEnumerable<string> HelpLines
        {
            get
            {
                if (_helpLinesCache == null)
                {
                    IEnumerable<FBTarget> baseTargets = BaseTargets.ToArray();
                    IEnumerable<FBTarget> heoTarget = Enumerable.Concat<FBTarget>(
                            SolutionTargets.Where(st => st.Enabled)
                            , MacroSolutionTargets
                        )
                        .ToArray();
                    int padingBaseTargets = baseTargets.Any() ? baseTargets.Max(pd => pd.SwitchKeyword.Length) : 0;
                    int padingHeoTargets = heoTarget.Any() ? heoTarget.Max(pd => pd.SwitchKeyword.Length) : 0;
                    _helpLinesCache = baseTargets.Select(pd => TranslateTargetToHelpLine(pd, padingBaseTargets));
                    if (heoTarget.Any())
                    {
                        _helpLinesCache = _helpLinesCache
                            .Concat(new[]
                            {
                                String.Empty,       // line field
                                _fbModel.LabelTextSectionHeoModules(),
                            })
                            .Concat(heoTarget.Select(pd => TranslateTargetToHelpLine(pd, padingHeoTargets)));
                    }
                    _helpLinesCache = _helpLinesCache.ToArray();
                }

                return _helpLinesCache;
            }
        }

        public BlocMacroFileNode HelpSummerComment
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();
                blocMacro.Add(GetDebugRem("HelpSummerComment"));
                blocMacro.AddRange(HelpLines.Select(t => new RemBatch(t)));
                return blocMacro;
            }
        }

        private string TranslateTargetToHelpLine(FBTarget target, int pading)
        {
            return String.Format("{0} : {1}", target.SwitchKeyword.PadRight(pading, ' '), target.HelpText);
        }

        #endregion Help summer comment

        #region Script Initialisation

        public BlocMacroFileNode ScriptInitialisation
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ScriptInitialisation"));
                blocMacro.Add(ScriptInitialisationVersion);
                blocMacro.Add(Nop);
                blocMacro.Add(ScriptInitialisationLocalEnv);

                return blocMacro;
            }
        }

        #region ScriptInitialisationVersion

        private BlocMacroFileNode ScriptInitialisationVersion
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ScriptInitialisationVersion"));

                // set LiteralVersionNumber={ValueVersionNumber}
                // set LiteralVersion={LabelFastBuild} v%LiteralValueVersionNumber%
                // <line field>
                blocMacro.Add(new SetExpressionCmd(LiteralVersionNumber, new ValueExpression(_fbModel.ValueVersionNumber())));
                blocMacro.Add(new SetExpressionCmd(
                    LiteralVersion
                    , new ComposedExpression(new ValueExpression(_fbModel.LabelTextFastBuild()), new ValueExpression(" v"), LiteralVersionNumber.LiteralValue)
                ));
                blocMacro.Add(Nop);

                // title %LiteralValueVersion%
                // echo %varfb_version%
                // echo.
                // cd
                // echo.
                blocMacro.Add(new TitleCmd(LiteralVersion.LiteralValue));
                blocMacro.Add(new EchoCmd(LiteralVersion.LiteralValue));
                blocMacro.Add(new EchoCmd((string)null));
                blocMacro.Add(new CdCmd((string)null));
                blocMacro.Add(new EchoCmd((string)null));
                return blocMacro;
            }
        }

        #endregion ScriptInitialisationVersion

        #region ScriptInitialisationLocalEnv

        public BlocMacroFileNode ScriptInitialisationLocalEnv
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ScriptInitialisationLocalEnv"));

                // setlocal
                // set PATH=%PATH%;%~dp0
                blocMacro.Add(new SetlocalCmd());
                BatchExpressionBase path = new ComposedExpression(
                    EnvSystemLiteral.Path.LiteralValue
                    , new ValueExpression(";")
                    , EnvSystemLiteral.BatchFilePath
                );
                blocMacro.Add(new SetExpressionCmd(EnvSystemLiteral.Path, path));

                return blocMacro;
            }
        }

        #endregion ScriptInitialisationLocalEnv

        #endregion Script Initialisation

        #region Parameters analyse

        public BlocMacroFileNode ParametersAnalyse
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ParametersAnalyse"));

                blocMacro.Add(ParametersAnalyseInitialisation);
                blocMacro.Add(Nop);

                blocMacro.Add(ParametersAnalyseParsing);
                blocMacro.Add(Nop);

                blocMacro.Add(ParametersAnalyseUsageAndVer);

                return blocMacro;
            }
        }

        #region Parameters initalisation

        public BlocMacroFileNode ParametersAnalyseInitialisation
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ParametersAnalyseInitialisation"));

                // Initialisation Cmds
                blocMacro.AddRange(ParametersAnalyseInitialisationCmds);

                return blocMacro;
            }
        }

        private IEnumerable<BatchStatementNodeBase> ParametersAnalyseInitialisationCmds
        {
            get
            {
                bool isFirst = true;
                foreach (var targets in AllTargets)
                {
                    if (false == targets.Any()) continue;
                    if (targets.First() is FBMacroSolutionTarget) continue;

                    if (false == isFirst)
                    {
                        yield return Nop;
                    }
                    else
                    {
                        isFirst = false;
                    }

                    foreach (var target in targets)
                    {
                        LiteralBatch literal = LiteralTargetByKeyWords[target.Keyword];
                        SetFalseCmd setFalseCmd = new SetFalseCmd(literal);
                        yield return setFalseCmd;
                    }
                }
            }
        }

        #endregion Parameters initalisation

        #region Parameters parsing

        public BlocMacroFileNode ParametersAnalyseParsing
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ParametersAnalyseParsing"));

                ConditionalCodeNode whileTest = new ConditionalCodeNode(EnvSystemLiteral.BatchParam1, new ValueExpression(String.Empty), true);
                WhileMacro whileMacro = new WhileMacro(
                    whileTest
                    , ParametersAnalyseParsingCmds
                    , _fbModel.BaseLabelMacroParametersParsing()
                );
                blocMacro.Add(whileMacro);

                return blocMacro;
            }
        }

        private BlocMacroFileNode ParametersAnalyseParsingCmds
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                BatchExpressionBase leftIfTest = EnvSystemLiteral.BatchParam1;

                bool isFirst = true;
                foreach (var targets in this.AllTargets)
                {
                    if (targets.Any())
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            blocMacro.Add(Nop);
                        }
                        foreach (var target in targets)
                        {
                            bool isInnerFirst = true;
                            FBMacroSolutionTarget macroSolutionTarget = target as FBMacroSolutionTarget;
                            FBSolutionTarget solutionTarget = target as FBSolutionTarget;
                            if (macroSolutionTarget != null)
                            {
                                if (isInnerFirst)
                                {
                                    isInnerFirst = false;
                                }
                                else
                                {
                                    blocMacro.Add(Nop);
                                }
                                string switchKeyword = macroSolutionTarget.SwitchKeyword;
                                blocMacro.AddRange(macroSolutionTarget.SolutionTargetIds
                                    .Join(SolutionTargets, id => id, st => st.Id, (id, st) => st)
                                    .Select(pd => GetParametersAnalyseParsingCmd(switchKeyword, pd, leftIfTest)));
                            }
                            else if (solutionTarget != null)
                            {
                                if (solutionTarget.Enabled)
                                    blocMacro.Add(GetParametersAnalyseParsingCmd(target.SwitchKeyword, target, leftIfTest));
                            }
                            else
                            {
                                blocMacro.Add(GetParametersAnalyseParsingCmd(target.SwitchKeyword, target, leftIfTest));
                            }
                        }
                    }
                }

                blocMacro.Add(Nop);
                blocMacro.Add(new ShiftCmd());
                blocMacro.Add(Nop);

                return blocMacro;
            }
        }

        private BatchFileNodeBase GetParametersAnalyseParsingCmd(string switchKeyword, FBTarget target, BatchExpressionBase leftIfTest)
        {
            BatchExpressionBase rightIfTest = new ValueExpression(switchKeyword);
            ConditionalCodeNode ifTest = new ConditionalCodeNode(leftIfTest, rightIfTest);
            LiteralBatch literal = LiteralTargetByKeyWords[target.Keyword];
            SetTrueCmd setTrueCmd = new SetTrueCmd(literal);
            IfCmd ifCmd = new IfCmd(ifTest, setTrueCmd);

            return ifCmd;
        }

        #endregion Parameters parsing

        #region Parameters Usage And Ver goto

        public BlocMacroFileNode ParametersAnalyseUsageAndVer
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("ParametersAnalyseUsageAndVer"));

                LiteralBooleanExpression literalValueHelp = LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchHelp].LiteralBoolean;
                LiteralBooleanExpression literalValueVer = LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchVer].LiteralBoolean;

                blocMacro.Add(new IfCmd(new IsTrueConditional(literalValueHelp), new GotoCmd(LabelGotoHelp)));
                blocMacro.Add(new IfCmd(new IsTrueConditional(literalValueVer), new GotoCmd(LabelGotoVersion)));

                return blocMacro;
            }
        }

        #endregion Parameters Usage And Ver goto

        #endregion Parameters analyse

        #region Fast Build Core

        public BlocMacroFileNode FastBuildCore
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCore"));

                blocMacro.Add(FastBuildCoreVcvarsall);
                blocMacro.Add(Nop);

                blocMacro.Add(FastBuildCoreTimeLoggerBeginSection);
                blocMacro.Add(Nop);

                blocMacro.Add(FastBuildCoreMSBuildEnvInit);
                blocMacro.Add(Nop);

                blocMacro.Add(FastBuildCoreShowConfig);
                blocMacro.Add(Nop);

                blocMacro.Add(FastBuildCoreBuild);
                blocMacro.Add(Nop);

                blocMacro.Add(FastBuildCoreTimeLoggerEndSection);
                blocMacro.Add(Nop);

                //if NOT (%para_wait%) == (0) ( pause )
                blocMacro.Add(new IfCmd(new IsTrueConditional(LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchWait]), new PauseCmd()));

                return blocMacro;
            }
        }

        #region FastBuildCoreVcvarsall

        private BlocMacroFileNode FastBuildCoreVcvarsall
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreVcvarsall"));

                BlocMacroFileNode macroThenVcvarsall = new BlocMacroFileNode();
                BlocMacroFileNode macroElseVcvarsall = new BlocMacroFileNode();
                // if NOT EXIST "%DevEnvDir%" ( ... )
                IfGotoMacro macroVcvarsall = new IfGotoMacro(
                    new QuotedValueExpression(LiteralEnvSystemVcvarsallCheckStatus.LiteralValue)
                    , macroThenVcvarsall
                    , macroElseVcvarsall
                    , _fbModel.BaseLabelMacroVcvarsall()
                    , true
                );
                blocMacro.Add(macroVcvarsall);

                BlocMacroFileNode macroThenVcvarsallX32X64 = new BlocMacroFileNode();
                BlocMacroFileNode macroElseVcvarsallX32X64 = new BlocMacroFileNode();
                // if not exist "%ProgramW6432%" ( ... 32 bit )
                IfGotoMacro macroVcvarsallX32X64 = new IfGotoMacro(
                    new QuotedValueExpression(EnvSystemLiteral.ProgramW6432)
                    , macroThenVcvarsallX32X64
                    , macroElseVcvarsallX32X64
                    , _fbModel.BaseLabelMacroVcvarsallX32X64()
                    , true
                );
                macroThenVcvarsall.Add(macroVcvarsallX32X64);

                macroElseVcvarsall.Add(new EchoCmd(_fbModel.LabelTextVcvarsallAlreadyinMemory()));

                BatchExpressionBase baseLabelTitleVcvarsall = new ComposedExpression(
                    new ValueExpression(_fbModel.LabelTextVcvarsall())
                    , new ValueExpression(" ")
                );

                string valueRelativePathVcvarsallBatchFile = _fbModel.ValueRelativePathVcvarsallBatchFile();

                // rem System 32 bit
                // echo vcvarsall %ProgramFiles%
                // call "%ProgramFiles%\Microsoft Visual Studio 12.0\VC\vcvarsall.bat"
                macroThenVcvarsallX32X64.Add(new RemBatch("System 32 bit"));
                macroThenVcvarsallX32X64.Add(new EchoCmd(new ComposedExpression(baseLabelTitleVcvarsall, EnvSystemLiteral.ProgramFiles)));
                BatchExpressionBase vcvarsallBatchFileX32 = new ComposedExpression(
                    EnvSystemLiteral.ProgramFiles
                    , new ValueExpression(valueRelativePathVcvarsallBatchFile)
                );
                macroThenVcvarsallX32X64.Add(new CallBatchCmd(new QuotedValueExpression(vcvarsallBatchFileX32)));

                // rem System 64 bit
                // echo vcvarsall %ProgramFiles(x86)%
                // call "%ProgramFiles(x86)%\Microsoft Visual Studio 12.0\VC\vcvarsall.bat"
                macroElseVcvarsallX32X64.Add(new RemBatch("System 64 bit"));
                macroElseVcvarsallX32X64.Add(new EchoCmd(new ComposedExpression(baseLabelTitleVcvarsall, EnvSystemLiteral.ProgramFilesX86)));
                BatchExpressionBase vcvarsallBatchFileX64 = new ComposedExpression(
                    new ValueExpression("\"")
                    , EnvSystemLiteral.ProgramFilesX86
                    , new ValueExpression(valueRelativePathVcvarsallBatchFile)
                    , new ValueExpression("\"")
                );
                macroElseVcvarsallX32X64.Add(new CallBatchCmd(vcvarsallBatchFileX64));

                blocMacro.Add(new EchoCmd(String.Empty));

                return blocMacro;
            }
        }

        #endregion FastBuildCoreVcvarsall

        #region Fast build core time logger

        public BlocMacroFileNode FastBuildCoreTimeLoggerBeginSection
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreTimeLoggerBeginSection"));

                // set varfb_startTime=%TIME%
                blocMacro.Add(new SetExpressionCmd(LiteralStartTime, EnvSystemLiteral.Time));

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreTimeLoggerEndSection
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreTimeLoggerEndSection"));

                //echo %DATE%	%varfb_startTime%	%TIME% >> measure_build.log.txt
                BatchExpressionBase logExpression = new ComposedExpression(
                    EnvSystemLiteral.Date
                    , new ValueExpression("\t")
                    , LiteralStartTime.LiteralValue
                    , new ValueExpression("\t")
                    , EnvSystemLiteral.Time
                    );
                EchoCmd logEchoCmd = new EchoCmd(logExpression);
                PipeToFileCmd logPipeToFileCmd = new PipeToFileCmd(logEchoCmd, _fbModel.ValuePathMeasureBuildLogFile());
                blocMacro.Add(logPipeToFileCmd);

                return blocMacro;
            }
        }

        #endregion Fast build core time logger

        #region Fast build core msbuild env init

        public BlocMacroFileNode FastBuildCoreMSBuildEnvInit
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreMSBuildEnvInit"));

                blocMacro.Add(FastBuildCoreMSBuildEnvInitConst);
                blocMacro.Add(Nop);

                blocMacro.Add(FastBuildCoreMSBuildEnvInitMSBuildConfig);
                blocMacro.Add(Nop);

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreMSBuildEnvInitConst
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreMSBuildEnvInitConst"));

                blocMacro.Add(RemSeparator);
                blocMacro.Add(Nop);

                blocMacro.Add(new RemBatch("MSBuild constants"));

                // set varfb_logfile=fastbuild.log
                // set varfb_fichierConfigurationSGenPlus=SgenPlusListeExclusion.txt
                blocMacro.Add(new SetExpressionCmd(LiteralMSBuildLogFile, new ValueExpression(_fbModel.ValueMSBuildLogFile())));
                blocMacro.Add(new SetExpressionCmd(LiteralSGenPlusConfigFilePath, new ValueExpression(_fbModel.ValueSGenPlusConfigFilePath())));

                blocMacro.Add(Nop);

                // modules
                blocMacro.Add(new RemBatch("HEO Modules constants"));
                foreach (FBSolutionTarget solutionTarget in SolutionTargets)
                {
                    blocMacro.Add(new SetExpressionCmd(LiteralSolutionMSBuildTargetByKeyWords[solutionTarget.Keyword], new ValueExpression(solutionTarget.MSBuildTarget)));
                }

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreMSBuildEnvInitMSBuildConfig
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreMSBuildEnvInitMSBuildConfig"));

                blocMacro.Add(new RemBatch("MSBuild configuration"));

                // Build Platform (x86)
                blocMacro.Add(new SetExpressionCmd(LiteralMSBuildPlatform, MSBuildCmd.ConstExpressionByPlatforms[BatchGen.BatchNode.ExternCmd.EnumPlatform.X86]));

                // Build Configuration (Debug, Release, ...)
                blocMacro.Add(new SetExpressionCmd(LiteralMSBuildConfiguration, MSBuildCmd.ConstExpressionConfigurationDebug));
                blocMacro.Add(new IfCmd(
                    new IsTrueConditional(LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchDsac])
                    , new SetExpressionCmd(LiteralMSBuildConfiguration, new ValueExpression(_fbModel.ValueMSBuildConfigurationDsac()))
                    ));
                blocMacro.Add(new IfCmd(
                    new IsTrueConditional(LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchRelease])
                    , new SetExpressionCmd(LiteralMSBuildConfiguration, MSBuildCmd.ConstExpressionConfigurationRelease)
                    ));
                blocMacro.Add(Nop);

                // SET varfb_HeoLanceurPath=..\Lanceur\Heo.Lanceur
                // SET varfb_ConfigurationPath=bin\x86\%varfb_MSBuildConfiguration%
                // SET varfb_HeoLanceurBinPath=%varfb_HeoLanceurPath%\%varfb_ConfigurationPath%
                blocMacro.Add(new SetExpressionCmd(LiteralHeoLanceurPath, new ValueExpression(_fbModel.ValueHeoLanceurPath())));
                blocMacro.Add(new SetExpressionCmd(LiteralConfigurationPath, new ComposedExpression(
                    new ValueExpression(_fbModel.ValuePathBin())
                    , new ValueExpression("\\")
                    , LiteralMSBuildConfiguration.LiteralValue
                    )));
                blocMacro.Add(new SetExpressionCmd(LiteralHeoLanceurBinPath, new ComposedExpression(
                    LiteralHeoLanceurPath.LiteralValue
                    , new ValueExpression("\\")
                    , LiteralConfigurationPath.LiteralValue
                    )));
                blocMacro.Add(Nop);

                IEnumerable<Tuple<LiteralBooleanExpression, LiteralValueExpression, FBSolutionTarget>> solutionTargets = SolutionTargets
                    .Select(st => Tuple.Create(
                        LiteralTargetByKeyWords[st.Keyword].LiteralBoolean
                        , LiteralSolutionMSBuildTargetByKeyWords[st.Keyword].LiteralValue
                        , st
                        ))
                    .ToArray();

                // MSBuild Check target
                blocMacro.Add(new RemBatch("MSBuild check target"));
                BooleanExpressionBase booleanExpressionMSBuildWithX86Targets = new BooleanOperatorExpression(
                    EnumBooleanOperator.Or
                    , solutionTargets.Select(lv => lv.Item1).ToArray());
                SetBooleanCmd setBooleanMSBuildWithTargets = new SetBooleanCmd(LiteralMSBuildWithTargets, booleanExpressionMSBuildWithX86Targets);
                blocMacro.Add(setBooleanMSBuildWithTargets);
                blocMacro.Add(new SetBooleanCmd(LiteralMSBuildForcedAll, new NotBooleanExpression(LiteralMSBuildWithTargets.LiteralBoolean)));
                BlocMacro blocMacroIfForcedAllThen = new BlocMacro();
                FBMacroSolutionTargetAll macroSolutionTargetAll =  MacroSolutionTargets
                    .OfType<FBMacroSolutionTargetAll>()
                    .FirstOrDefault();
                Debug.Assert(macroSolutionTargetAll!=null);
                blocMacroIfForcedAllThen.AddRange(
                    macroSolutionTargetAll.SolutionTargetIds
                        .Join(solutionTargets, id => id, t => t.Item3.Id, (id, t) => t)
                        .Select(t => new SetBooleanCmd(LiteralTargetByKeyWords[t.Item3.Keyword], TrueValueExpression.ValueExpression))
                    );

                IfMacro ifMacroForcedAll = new IfMacro(
                    new IsTrueConditional(LiteralMSBuildForcedAll.LiteralBoolean)
                    , blocMacroIfForcedAllThen
                    , null);
                blocMacro.Add(ifMacroForcedAll);
                blocMacro.Add(Nop);

                // MSBuild Cli X86
                blocMacro.Add(new RemBatch("MSBuild configuration (x86)"));
                MSBuildCliMacro msbuildCliMacroX86 = GetFastBuildCoreMSBuildEnvInitMSBuildConfigMSBuildCliMacro(
                    LiteralMSBuildCli
                    , LiteralMSBuildPlatform.LiteralValue
                    , solutionTargets.Select(t => new Tuple<BooleanExpressionBase, BatchExpressionBase>(t.Item1, t.Item2)) );
                blocMacro.Add(msbuildCliMacroX86);

                return blocMacro;
            }
        }

        private MSBuildCliMacro GetFastBuildCoreMSBuildEnvInitMSBuildConfigMSBuildCliMacro(LiteralBatch literalMSBuildCli, BatchExpressionBase platform, IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> targets)
        {
            MSBuildCliMacro result = new MSBuildCliMacro(
                literalMSBuildCli
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchPara].LiteralBoolean
                , LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchQuiet].LiteralBoolean
                , new QuotedValueExpression(LiteralMSBuildConfiguration.LiteralValue)
                , LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchFxcop].LiteralBoolean
                , LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchNowarn].LiteralBoolean
                , LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchRebuild].LiteralBoolean
                , platform
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                    FalseValueExpression.ValueExpression
                    , LiteralMSBuildLogFile.LiteralValue
                    )
                , targets
                );

            return result;
        }

        #endregion Fast build core msbuild env init

        #region Fast build core show config

        public BlocMacroFileNode FastBuildCoreShowConfig
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreShowConfig"));

                blocMacro.Add(new RemBatch("show build configuration"));
                // ECHO %varfb_MSBuildConfiguration%^|%varfb_MSBuildPlatform%
                blocMacro.Add(new EchoCmd(new ComposedExpression(
                    LiteralMSBuildConfiguration.LiteralValue
                    , new ValueExpression("^|")
                    , LiteralMSBuildPlatform.LiteralValue
                    )));
                blocMacro.Add(new EchoCmd(String.Empty));

                return blocMacro;
            }
        }

        #endregion Fast build core show config

        #region build core build

        public BlocMacroFileNode FastBuildCoreBuild
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreBuild"));

                // :: building
                blocMacro.Add(new RemBatch("building"));
                blocMacro.Add(Nop);

                // call :fctKillVsHost
                // call :fctKillHeo
                blocMacro.Add(CallSubKillHeoVsHost);
                blocMacro.Add(CallSubKillHeo);
                blocMacro.Add(Nop);

                // x86 building
                blocMacro.Add(FastBuildCoreBuildX86);
                blocMacro.Add(Nop);

                // SGenPlus
                blocMacro.Add(FastBuildCoreSGenPlus);
                blocMacro.Add(Nop);

                // call :fctKillVsHost
                blocMacro.Add(CallSubKillHeoVsHost);
                blocMacro.Add(Nop);

                // end status
                blocMacro.Add(FastBuildCoreStatus);

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreBuildX86
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreBuildX86"));

                BatchFileNodeBase fastBuildCoreBuildX86Node = GetFastBuildCoreBuild(LiteralMSBuildCli);
                blocMacro.Add(fastBuildCoreBuildX86Node);

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreSGenPlus
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreSGenPlus"));

                blocMacro.Add(new RemBatch("SGenPlus"));

                // SET /A varfb_SGenPlusNeedRun="1"
                // IF NOT EXIST %varfb_SGenPlusConfigFilePath% (SET /A varfb_SGenPlusNeedRun&="0")
                // IF (%varfb_param_nosgp%) == (1) (SET /A varfb_SGenPlusNeedRun&="0")
                blocMacro.Add(new SetTrueCmd(LiteralSGenPlusNeedRun));
                blocMacro.Add(new IfCmd(
                    new ConditionalCodeNode(LiteralSGenPlusConfigFilePath.LiteralValue, true)
                    , new SetFalseCmd(LiteralSGenPlusNeedRun)
                    ));
                blocMacro.Add(new IfCmd(
                    new IsTrueConditional(LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchNosgp])
                    , new SetFalseCmd(LiteralSGenPlusNeedRun)
                    ));

                // IF (%varfb_param_nosgp%)
                BlocMacroFileNode blocMacroSGenPlus = new BlocMacroFileNode();
                blocMacro.Add(new IfGotoMacro(
                    new IsTrueConditional(LiteralSGenPlusNeedRun)
                    , blocMacroSGenPlus
                    , null
                    , _fbModel.BaseLabelMacroSGenPlusNeedRun()
                    ));
                // SET varfb_SGenPlusTargetBinaryPath=%varfb_HeoLanceurBinPath%
                // SET varfb_SGenPlusCli=/cur:"%varfb_SGenPlusTargetBinaryPath%" /opt:"%varfb_SGenPlusConfigFilePath%"
                blocMacroSGenPlus.Add(new SetExpressionCmd(LiteralSGenPlusTargetBinaryPath, LiteralHeoLanceurBinPath.LiteralValue));
                blocMacroSGenPlus.Add(new SetExpressionCmd(LiteralSGenPlusCli, new ComposedExpression(
                    new ComposedExpression(new ValueExpression("/cur:"), new QuotedValueExpression(LiteralSGenPlusTargetBinaryPath.LiteralValue))
                    , new ValueExpression(" ")
                    , new ComposedExpression(new ValueExpression("/opt:"), new QuotedValueExpression(LiteralSGenPlusConfigFilePath.LiteralValue))
                    )));

                // ECHO SGenPlus with parameters : %varfb_SGenPlusCli%
                // SGenPlus %varfb_SGenPlusCli%
                blocMacroSGenPlus.Add(new EchoCmd(new ComposedExpression(
                    new ValueExpression("SGenPlus with parameters : ")
                    , LiteralSGenPlusCli.LiteralValue
                    )));
                blocMacroSGenPlus.Add(new SGenPlusCmd(LiteralSGenPlusCli.LiteralValue));
                blocMacroSGenPlus.Add(Nop);

                // SGenPlus status
                // :: SGenPlus status
                blocMacroSGenPlus.Add(new RemBatch("SGenPlus status"));
                BlocMacro blocMacroThenSGenPlusStatus = new BlocMacro();
                BlocMacro blocMacroElseSGenPlusStatus = new BlocMacro();
                // IF NOT ERRORLEVEL 0 (...) ELSE (...)
                blocMacroSGenPlus.Add(new IfGotoMacro(0, blocMacroThenSGenPlusStatus, blocMacroElseSGenPlusStatus, _fbModel.BaseLabelMacroSGenPlusStatus()));
                // :: SGenPlus success
                // ECHO SGenPlus success
                blocMacroThenSGenPlusStatus.Add(new RemBatch("SGenPlus success"));
                blocMacroThenSGenPlusStatus.Add(new EchoCmd("SGenPlus success"));
                // :: SGenPlus fail
                // ECHO SGenPlus fail
                blocMacroElseSGenPlusStatus.Add(new RemBatch("SGenPlus fail"));
                blocMacroElseSGenPlusStatus.Add(new EchoCmd("SGenPlus fail"));

                blocMacroSGenPlus.Add(new EchoCmd(String.Empty));

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreStatus
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreStatus"));

                blocMacro.Add(new EchoCmd(new ComposedExpression(
                    new ValueExpression("Done")
                    , new ValueExpression("\t")
                    , EnvSystemLiteral.Time
                    )));
                blocMacro.Add(new EchoCmd(String.Empty));

                return blocMacro;
            }
        }

        private BatchFileNodeBase GetFastBuildCoreBuild(LiteralBatch literalMSBuildCli)
        {
            BlocMacro macroResult = new BlocMacro();

            // msbuild
            // :: MSBuild cmd
            // ECHO Building(%platform%) with cli parameters %varfb_MSBuild_Cli_Win32%
            // SET /B varfb_ExitStatus=0
            // MSBuild %varfb_MSBuild_Cli_Win32%
            // ECHO.
            macroResult.Add(new RemBatch("MSBuild cmd"));
            macroResult.Add(new EchoCmd(new ComposedExpression(
                new ComposedExpression(
                    new ValueExpression("Building(")
                    , LiteralMSBuildPlatform.LiteralValue
                    , new ValueExpression(") with cli parameters ")
                )
                , literalMSBuildCli.LiteralValue
                )));
            macroResult.Add(new MSBuildCmd(literalMSBuildCli));
            macroResult.Add(new EchoCmd(String.Empty));

            return macroResult;
        }

        #endregion build core build

        #endregion Fast Build Core

        #region Version bloc

        public BlocMacroFileNode VersionBloc
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("VersionBloc"));

                blocMacro.Add(RemSeparator);
                blocMacro.Add(Nop);
                blocMacro.Add(LabelGotoVersion);

                // echo %varfb_version%
                // echo.
                blocMacro.Add(new EchoCmd(this.LiteralVersion.LiteralValue));
                blocMacro.Add(new EchoCmd(String.Empty));

                blocMacro.Add(GotoEnd);

                return blocMacro;
            }
        }

        #endregion Version bloc

        #region Help bloc

        public BlocMacroFileNode HelpBloc
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("HelpBloc"));

                blocMacro.Add(RemSeparator);
                blocMacro.Add(Nop);
                blocMacro.Add(LabelGotoHelp);
                blocMacro.Add(new EchoCmd(String.Empty));
                blocMacro.AddRange(HelpLines.Select(t => new EchoCmd(t)));
                blocMacro.Add(new EchoCmd(String.Empty));
                blocMacro.Add(Nop);
                blocMacro.Add(GotoEnd);

                return blocMacro;
            }
        }

        #endregion Help bloc

        #region SubFunctions bloc

        public BlocMacroFileNode SubFunctions
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("SubFunctions"));

                blocMacro.Add(SubFunctionsKillHeoVsHost);
                blocMacro.Add(Nop);
                blocMacro.Add(SubFunctionsKillHeo);

                return blocMacro;
            }
        }

        #region Sub Kill Heo VsHost

        public BlocMacroFileNode SubFunctionsKillHeoVsHost
        {
            get
            {
                // if (%varfb_param_vshost%) == (1) (
                //   echo.
                //   echo Stop vshost binaries
                // 	 taskkill /F /IM heo.lanceur.vshost.*
                // 	 echo.
                // )

                BlocMacro nodeThen = new BlocMacro();
                nodeThen.Add(new EchoCmd(String.Empty));
                nodeThen.Add(new EchoCmd(_fbModel.LabelTextKillHeoVsHost()));
                nodeThen.Add(new TaskKillCmd(new ValueExpression(_fbModel.ValueHeoVsHostImageName()), force: true));
                nodeThen.Add(new EchoCmd(String.Empty));

                IfMacro ifMacro = new IfMacro(
                    new IsTrueConditional(LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchVshost])
                    , nodeThen
                    , null
                );

                BlocMacroFileNode blocMacro = new BlocMacroFileNode();
                blocMacro.Add(GetDebugRem("SubFunctionsKillHeoVsHost"));
                blocMacro.Add(new SubMacro(LabelSubKillHeoVsHost, ifMacro));

                return blocMacro;
            }
        }

        #endregion Sub Kill Heo VsHost

        #region Sub Kill Heo

        public BlocMacroFileNode SubFunctionsKillHeo
        {
            get
            {
                // if (%varfb_param_killheo%) == (1) (
                // 	 echo.
                // 	 echo Kill Heo.exe
                // 	 taskkill /F /IM heo.lanceur.exe
                // 	 echo.
                // )

                BlocMacro nodeThen = new BlocMacro();
                nodeThen.Add(new EchoCmd(String.Empty));
                nodeThen.Add(new EchoCmd(_fbModel.LabelTextKillHeo()));
                nodeThen.Add(new TaskKillCmd(new ValueExpression(_fbModel.ValueHeoImageName()), force: true));
                nodeThen.Add(new EchoCmd(String.Empty));

                IfMacro ifMacro = new IfMacro(
                    new IsTrueConditional(LiteralTargetByKeyWords[FBModel.ConstKeywordParamSwitchKillheo])
                    , nodeThen
                    , null
                );

                BlocMacroFileNode blocMacro = new BlocMacroFileNode();
                blocMacro.Add(GetDebugRem("SubFunctionsKillHeo"));
                blocMacro.Add(new SubMacro(LabelSubKillHeo, ifMacro));

                return blocMacro;
            }
        }

        #endregion Sub Kill Heo

        #endregion SubFunctions bloc
    }
}