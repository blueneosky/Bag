using System;
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
            result = new BlocMacro();
#else
                result = new BlocMacro();
#endif

            return result;
        }

        #endregion Debug

        private IFastBuildModel _fastBuildModel;

        public FastBuildBatchFile(IFastBuildModel fastBuildModel)
            : base(fastBuildModel.WithEchoOff)
        {
            _fastBuildModel = fastBuildModel;

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
                    _remSeparatorCache = new RemBatch(_fastBuildModel.LabelTextRemSeparator());
                return _remSeparatorCache;
            }
        }

        #region FastBuild Property wraper

        private IEnumerable<IEnumerable<IParamDescription>> AllParamDescriptions
        {
            get
            {
                yield return BaseParamDescriptions;
                yield return ParamDescriptionHeoModules;
                yield return ParamDescriptionHeoTargets;
            }
        }

        private IEnumerable<IParamDescription> BaseParamDescriptions
        {
            get { return Enumerable.Concat(ParamDescriptionCommons, ParamDescriptionHeo); }
        }

        private IEnumerable<IParamDescription> ParamDescriptionCommons
        {
            get { return _fastBuildModel.FastBuildParamModel.FastBuildParams; }
        }

        private IEnumerable<IParamDescription> ParamDescriptionHeo
        {
            get { return _fastBuildModel.FastBuildParamModel.FastBuildHeoParams; }
        }

        private IEnumerable<IParamDescriptionHeoModule> ParamDescriptionHeoModules
        {
            get { return _fastBuildModel.FastBuildParamModel.HeoModuleParams; }
        }

        private IEnumerable<IParamDescriptionHeoTarget> ParamDescriptionHeoTargets
        {
            get { return _fastBuildModel.FastBuildParamModel.HeoTargetParams; }
        }

        private IParamDescription GetParamDescription(string keyWord)
        {
            IParamDescription result = AllParamDescriptions
                .SelectMany(e => e)
                .First(pd => pd.Keyword == keyWord);

            return result;
        }

        #endregion FastBuild Property wraper

        #region Expressions

        #endregion Expressions

        #region Literal

        private LiteralBatch _literalConfigurationPathCache;
        private LiteralBatch _literalEnvSystemVcvarsallCheckStatusCache;
        private LiteralBatch _literalHeoForcedOutputDirPathCache;
        private LiteralBatch _literalHeoLanceurBinPathCache;
        private LiteralBatch _literalHeoLanceurPathCache;
        private Dictionary<string, LiteralBatch> _literalModuleMSBuildTargetByKeyWordsCache;
        private LiteralBatch _literalMSBuildCliWin32Cache;
        private LiteralBatch _literalMSBuildCliX86Cache;
        private LiteralBatch _literalMSBuildConfigurationCache;
        private LiteralBatch _literalMSBuildLogFileCache;
        private LiteralBatch _literalMSBuildPlatformValue;
        private LiteralBatch _literalMSBuildsWithTargetsCache;
        private LiteralBatch _literalMSBuildTryLoopCondCache;
        private LiteralBatch _literalMSBuildWin32NeedRunCache;
        private LiteralBatch _literalMSBuildWithWin32TargetsCache;
        private LiteralBatch _literalMSBuildWithX86TargetsCache;
        private LiteralBatch _literalMSBuildX86NeedRunCache;
        private Dictionary<string, LiteralBatch> _literalParamDescriptionByKeyWordsCache;
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
                    _literalConfigurationPathCache = new LiteralBatch(_fastBuildModel.LiteralConfigurationPath());
                return _literalConfigurationPathCache;
            }
        }

        public LiteralBatch LiteralEnvSystemVcvarsallCheckStatus
        {
            get
            {
                if (_literalEnvSystemVcvarsallCheckStatusCache == null)
                    _literalEnvSystemVcvarsallCheckStatusCache = new LiteralBatch(_fastBuildModel.LiteralEnvSystemVcvarsallCheckStatus());
                return _literalEnvSystemVcvarsallCheckStatusCache;
            }
        }

        public LiteralBatch LiteralHeoForcedOutputDirPath
        {
            get
            {
                if (_literalHeoForcedOutputDirPathCache == null)
                    _literalHeoForcedOutputDirPathCache = new LiteralBatch(_fastBuildModel.LiteralHeoForcedOutputDirPath());
                return _literalHeoForcedOutputDirPathCache;
            }
        }

        public LiteralBatch LiteralHeoLanceurBinPath
        {
            get
            {
                if (_literalHeoLanceurBinPathCache == null)
                    _literalHeoLanceurBinPathCache = new LiteralBatch(_fastBuildModel.LiteralHeoLanceurBinPath());
                return _literalHeoLanceurBinPathCache;
            }
        }

        public LiteralBatch LiteralHeoLanceurPath
        {
            get
            {
                if (_literalHeoLanceurPathCache == null)
                    _literalHeoLanceurPathCache = new LiteralBatch(_fastBuildModel.LiteralHeoLanceurPath());
                return _literalHeoLanceurPathCache;
            }
        }

        public Dictionary<string, LiteralBatch> LiteralModuleMSBuildTargetByKeyWords
        {
            get
            {
                if (_literalModuleMSBuildTargetByKeyWordsCache == null)
                {
                    _literalModuleMSBuildTargetByKeyWordsCache = ParamDescriptionHeoModules
                        .ToDictionary(
                            pd => pd.Keyword
                            , pd => new LiteralBatch(pd.VarName)
                        );
                }
                return _literalModuleMSBuildTargetByKeyWordsCache;
            }
        }

        public LiteralBatch LiteralMSBuildCliWin32
        {
            get
            {
                if (_literalMSBuildCliWin32Cache == null)
                    _literalMSBuildCliWin32Cache = new LiteralBatch(_fastBuildModel.LiteralMSBuildCliWin32());
                return _literalMSBuildCliWin32Cache;
            }
        }

        public LiteralBatch LiteralMSBuildCliX86
        {
            get
            {
                if (_literalMSBuildCliX86Cache == null)
                    _literalMSBuildCliX86Cache = new LiteralBatch(_fastBuildModel.LiteralMSBuildCliX86());
                return _literalMSBuildCliX86Cache;
            }
        }

        public LiteralBatch LiteralMSBuildConfiguration
        {
            get
            {
                if (_literalMSBuildConfigurationCache == null)
                    _literalMSBuildConfigurationCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildConfiguration());
                return _literalMSBuildConfigurationCache;
            }
        }

        public LiteralBatch LiteralMSBuildLogFile
        {
            get
            {
                if (_literalMSBuildLogFileCache == null)
                    _literalMSBuildLogFileCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildLogFile());
                return _literalMSBuildLogFileCache;
            }
        }

        public LiteralBatch LiteralMSBuildPlatform
        {
            get
            {
                if (_literalMSBuildPlatformValue == null)
                    _literalMSBuildPlatformValue = new LiteralBatch(_fastBuildModel.LiteralMSBuildPlatform());
                return _literalMSBuildPlatformValue;
            }
        }

        public LiteralBatch LiteralMSBuildsWithTargets
        {
            get
            {
                if (_literalMSBuildsWithTargetsCache == null)
                    _literalMSBuildsWithTargetsCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildsWithTargets());
                return _literalMSBuildsWithTargetsCache;
            }
        }

        public LiteralBatch LiteralMSBuildTryLoopCond
        {
            get
            {
                if (_literalMSBuildTryLoopCondCache == null)
                    _literalMSBuildTryLoopCondCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildTryLoopCond());
                return _literalMSBuildTryLoopCondCache;
            }
        }

        public LiteralBatch LiteralMSBuildWin32NeedRun
        {
            get
            {
                if (_literalMSBuildWin32NeedRunCache == null)
                    _literalMSBuildWin32NeedRunCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildWin32NeedRun());
                return _literalMSBuildWin32NeedRunCache;
            }
        }

        public LiteralBatch LiteralMSBuildWithWin32Targets
        {
            get
            {
                if (_literalMSBuildWithWin32TargetsCache == null)
                    _literalMSBuildWithWin32TargetsCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildWithWin32Targets());
                return _literalMSBuildWithWin32TargetsCache;
            }
        }

        public LiteralBatch LiteralMSBuildWithX86Targets
        {
            get
            {
                if (_literalMSBuildWithX86TargetsCache == null)
                    _literalMSBuildWithX86TargetsCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildWithX86Targets());
                return _literalMSBuildWithX86TargetsCache;
            }
        }

        public LiteralBatch LiteralMSBuildX86NeedRun
        {
            get
            {
                if (_literalMSBuildX86NeedRunCache == null)
                    _literalMSBuildX86NeedRunCache = new LiteralBatch(_fastBuildModel.LiteralMSBuildX86NeedRun());
                return _literalMSBuildX86NeedRunCache;
            }
        }

        public Dictionary<string, LiteralBatch> LiteralParamDescriptionByKeyWords
        {
            get
            {
                if (_literalParamDescriptionByKeyWordsCache == null)
                {
                    _literalParamDescriptionByKeyWordsCache = AllParamDescriptions
                        .SelectMany(e => e)
                        .ToDictionary(
                            pd => pd.Keyword
                            , pd => new LiteralBatch(pd.ParamVarName)
                        )
                        ;
                }
                return _literalParamDescriptionByKeyWordsCache;
            }
        }

        public LiteralBatch LiteralSGenPlusCli
        {
            get
            {
                if (_literalSGenPlusCliCache == null)
                    _literalSGenPlusCliCache = new LiteralBatch(_fastBuildModel.LiteralSGenPlusCli());
                return _literalSGenPlusCliCache;
            }
        }

        public LiteralBatch LiteralSGenPlusConfigFilePath
        {
            get
            {
                if (_literalSGenPlusConfigFilePathCache == null)
                    _literalSGenPlusConfigFilePathCache = new LiteralBatch(_fastBuildModel.LiteralSGenPlusConfigFilePath());
                return _literalSGenPlusConfigFilePathCache;
            }
        }

        public LiteralBatch LiteralSGenPlusNeedRun
        {
            get
            {
                if (_literalSGenPlusNeedRunCache == null)
                    _literalSGenPlusNeedRunCache = new LiteralBatch(_fastBuildModel.LiteralSGenPlusNeedRun());
                return _literalSGenPlusNeedRunCache;
            }
        }

        public LiteralBatch LiteralSGenPlusTargetBinaryPath
        {
            get
            {
                if (_literalSGenPlusTargetBinaryPathCache == null)
                    _literalSGenPlusTargetBinaryPathCache = new LiteralBatch(_fastBuildModel.LiteralSGenPlusTargetBinaryPath());
                return _literalSGenPlusTargetBinaryPathCache;
            }
        }

        public LiteralBatch LiteralStartTime
        {
            get
            {
                if (_literalStartTimeCache == null)
                    _literalStartTimeCache = new LiteralBatch(_fastBuildModel.LiteralStartTime());
                return _literalStartTimeCache;
            }
        }

        public LiteralBatch LiteralVersion
        {
            get
            {
                if (_literalVersionCache == null)
                    _literalVersionCache = new LiteralBatch(_fastBuildModel.LiteralVersionName());
                return _literalVersionCache;
            }
        }

        public LiteralBatch LiteralVersionNumber
        {
            get
            {
                if (_literalVersionNumberCache == null)
                    _literalVersionNumberCache = new LiteralBatch(_fastBuildModel.LiteralVersionNumberName());
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
                    _labelGotoEndCache = new LabelGotoBatch(_fastBuildModel.LabelGotoEnd());
                return _labelGotoEndCache;
            }
        }

        public LabelGotoBatch LabelGotoHelp
        {
            get
            {
                if (_labelGotoHelpCache == null)
                    _labelGotoHelpCache = new LabelGotoBatch(_fastBuildModel.LabelGotoHelp());
                return _labelGotoHelpCache;
            }
        }

        public LabelGotoBatch LabelGotoVersion
        {
            get
            {
                if (_labelGotoVersionCache == null)
                    _labelGotoVersionCache = new LabelGotoBatch(_fastBuildModel.LabelGotoVersion());
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
                    _labelSubKillHeoCache = new LabelSubBatch(_fastBuildModel.LabelSubKillHeo());
                return _labelSubKillHeoCache;
            }
        }

        public LabelSubBatch LabelSubKillHeoVsHost
        {
            get
            {
                if (_labelSubKillHeoVsHostCache == null)
                    _labelSubKillHeoVsHostCache = new LabelSubBatch(_fastBuildModel.LabelSubKillHeoVsHost());
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
                    IEnumerable<IParamDescription> baseParamDescriptions = BaseParamDescriptions.ToArray();
                    IEnumerable<IParamDescription> heoParamDescriptions = Enumerable.Concat<IParamDescription>(
                            ParamDescriptionHeoModules
                            , ParamDescriptionHeoTargets
                        )
                        .ToArray();
                    int padingBaseParamDescriptions = baseParamDescriptions.Any() ? baseParamDescriptions.Max(pd => pd.SwitchKeyword.Length) : 0;
                    int padingHeoParamDescriptions = heoParamDescriptions.Any() ? heoParamDescriptions.Max(pd => pd.SwitchKeyword.Length) : 0;
                    _helpLinesCache = baseParamDescriptions.Select(pd => TranslateParamDescriptionToHelpLine(pd, padingBaseParamDescriptions));
                    if (heoParamDescriptions.Any())
                    {
                        _helpLinesCache = _helpLinesCache
                            .Concat(new[]
                            {
                                String.Empty,       // line field
                                _fastBuildModel.LabelTextSectionHeoModules(),
                            })
                            .Concat(heoParamDescriptions.Select(pd => TranslateParamDescriptionToHelpLine(pd, padingHeoParamDescriptions)));
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

        private string TranslateParamDescriptionToHelpLine(IParamDescription paramDescription, int pading)
        {
            return String.Format("{0} : {1}", paramDescription.SwitchKeyword.PadRight(pading, ' '), paramDescription.HelpText);
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
                blocMacro.Add(new SetExpressionCmd(LiteralVersionNumber, new ValueExpression(_fastBuildModel.ValueVersionNumber())));
                blocMacro.Add(new SetExpressionCmd(
                    LiteralVersion
                    , new ComposedExpression(new ValueExpression(_fastBuildModel.LabelTextFastBuild()), new ValueExpression(" v"), LiteralVersionNumber.LiteralValue)
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
                foreach (var paramDescriptions in AllParamDescriptions)
                {
                    if (false == paramDescriptions.Any()) continue;
                    if (paramDescriptions.First() is ParamDescriptionHeoTarget) continue;

                    if (false == isFirst)
                    {
                        yield return Nop;
                    }
                    else
                    {
                        isFirst = false;
                    }

                    foreach (var paramDescription in paramDescriptions)
                    {
                        LiteralBatch literal = LiteralParamDescriptionByKeyWords[paramDescription.Keyword];
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
                    , _fastBuildModel.BaseLabelMacroParametersParsing()
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
                foreach (var paramDescriptions in this.AllParamDescriptions)
                {
                    if (paramDescriptions.Any())
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            blocMacro.Add(Nop);
                        }
                        foreach (var paramDescription in paramDescriptions)
                        {
                            bool isInnerFirst = true;
                            ParamDescriptionHeoTarget paramDescriptionHeoTarget = paramDescription as ParamDescriptionHeoTarget;
                            if (paramDescriptionHeoTarget == null)
                            {
                                blocMacro.Add(GetParametersAnalyseParsingCmd(paramDescription.SwitchKeyword, paramDescription, leftIfTest));
                            }
                            else
                            {
                                if (isInnerFirst)
                                {
                                    isInnerFirst = false;
                                }
                                else
                                {
                                    blocMacro.Add(Nop);
                                }
                                string switchKeyword = paramDescriptionHeoTarget.SwitchKeyword;
                                blocMacro.AddRange(paramDescriptionHeoTarget.Dependencies.Select(pd => GetParametersAnalyseParsingCmd(switchKeyword, pd, leftIfTest)));
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

        private BatchFileNodeBase GetParametersAnalyseParsingCmd(string switchKeyword, IParamDescription paramDescription, BatchExpressionBase leftIfTest)
        {
            BatchExpressionBase rightIfTest = new ValueExpression(switchKeyword);
            ConditionalCodeNode ifTest = new ConditionalCodeNode(leftIfTest, rightIfTest);
            LiteralBatch literal = LiteralParamDescriptionByKeyWords[paramDescription.Keyword];
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

                LiteralBooleanExpression literalValueHelp = LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchHelp].LiteralBoolean;
                LiteralBooleanExpression literalValueVer = LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchVer].LiteralBoolean;

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
                blocMacro.Add(new IfCmd(new IsTrueConditional(LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchWait]), new PauseCmd()));

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
                    , _fastBuildModel.BaseLabelMacroVcvarsall()
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
                    , _fastBuildModel.BaseLabelMacroVcvarsallX32X64()
                    , true
                );
                macroThenVcvarsall.Add(macroVcvarsallX32X64);

                macroElseVcvarsall.Add(new EchoCmd(_fastBuildModel.LabelTextVcvarsallAlreadyinMemory()));

                BatchExpressionBase baseLabelTitleVcvarsall = new ComposedExpression(
                    new ValueExpression(_fastBuildModel.LabelTextVcvarsall())
                    , new ValueExpression(" ")
                );

                string valueRelativePathVcvarsallBatchFile = _fastBuildModel.ValueRelativePathVcvarsallBatchFile();

                // rem System 32 bit
                // echo vcvarsall %ProgramFiles%
                // call "%ProgramFiles%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"
                macroThenVcvarsallX32X64.Add(new RemBatch("System 32 bit"));
                macroThenVcvarsallX32X64.Add(new EchoCmd(new ComposedExpression(baseLabelTitleVcvarsall, EnvSystemLiteral.ProgramFiles)));
                BatchExpressionBase vcvarsallBatchFileX32 = new ComposedExpression(
                    EnvSystemLiteral.ProgramFiles
                    , new ValueExpression(valueRelativePathVcvarsallBatchFile)
                );
                macroThenVcvarsallX32X64.Add(new CallBatchCmd(new QuotedValueExpression(vcvarsallBatchFileX32)));

                // rem System 64 bit
                // echo vcvarsall %ProgramFiles(x86)%
                // call "%ProgramFiles(x86)%\Microsoft Visual Studio 10.0\VC\vcvarsall.bat"
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
                PipeToFileCmd logPipeToFileCmd = new PipeToFileCmd(logEchoCmd, _fastBuildModel.ValuePathMeasureBuildLogFile());
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
                blocMacro.Add(new SetExpressionCmd(LiteralMSBuildLogFile, new ValueExpression(_fastBuildModel.ValueMSBuildLogFile())));
                blocMacro.Add(new SetExpressionCmd(LiteralSGenPlusConfigFilePath, new ValueExpression(_fastBuildModel.ValueSGenPlusConfigFilePath())));

                blocMacro.Add(Nop);

                // modules
                blocMacro.Add(new RemBatch("HEO Modules constants"));
                foreach (ParamDescriptionHeoModule paramDescription in ParamDescriptionHeoModules)
                {
                    blocMacro.Add(new SetExpressionCmd(LiteralModuleMSBuildTargetByKeyWords[paramDescription.Keyword], new ValueExpression(paramDescription.MSBuildTarget)));
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
                    new IsTrueConditional(LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchDsac])
                    , new SetExpressionCmd(LiteralMSBuildConfiguration, new ValueExpression(_fastBuildModel.ValueMSBuildConfigurationDsac()))
                    ));
                blocMacro.Add(new IfCmd(
                    new IsTrueConditional(LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchRelease])
                    , new SetExpressionCmd(LiteralMSBuildConfiguration, MSBuildCmd.ConstExpressionConfigurationRelease)
                    ));
                blocMacro.Add(Nop);

                // SET varfb_HeoLanceurPath=..\Lanceur\Heo.Lanceur
                // SET varfb_ConfigurationPath=bin\x86\%varfb_MSBuildConfiguration%
                // SET varfb_HeoLanceurBinPath=%varfb_HeoLanceurPath%\%varfb_ConfigurationPath%
                // set varfb_outputDir=.%HeoLanceurBinPath%
                blocMacro.Add(new SetExpressionCmd(LiteralHeoLanceurPath, new ValueExpression(_fastBuildModel.ValueHeoLanceurPath())));
                blocMacro.Add(new SetExpressionCmd(LiteralConfigurationPath, new ComposedExpression(
                    new ValueExpression(_fastBuildModel.ValuePathBin())
                    , new ValueExpression("\\")
                    , LiteralMSBuildConfiguration.LiteralValue
                    )));
                blocMacro.Add(new SetExpressionCmd(LiteralHeoLanceurBinPath, new ComposedExpression(
                    LiteralHeoLanceurPath.LiteralValue
                    , new ValueExpression("\\")
                    , LiteralConfigurationPath.LiteralValue
                    )));
                blocMacro.Add(new SetExpressionCmd(LiteralHeoForcedOutputDirPath, LiteralHeoLanceurBinPath.LiteralValue));
                blocMacro.Add(Nop);

                // MSBuild Cli Win32
                blocMacro.Add(new RemBatch("MSBuild configuration (win32)"));
                IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> win32Modules = ParamDescriptionHeoModules
                    .Where(pdm => pdm.Platform == BusinessModel.EnumPlatform.Win32)
                    .Select(pdm => new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                        LiteralParamDescriptionByKeyWords[pdm.Keyword].LiteralBoolean
                        , LiteralModuleMSBuildTargetByKeyWords[pdm.Keyword].LiteralValue
                        ))
                    .ToArray();
                MSBuildCliMacro msbuildCliMacroWin32 = GetFastBuildCoreMSBuildEnvInitMSBuildConfigMSBuildCliMacro(
                    LiteralMSBuildCliWin32
                    , MSBuildCmd.ConstExpressionByPlatforms[BatchGen.BatchNode.ExternCmd.EnumPlatform.Win32]
                    , win32Modules
                    );
                blocMacro.Add(msbuildCliMacroWin32);
                blocMacro.Add(Nop);
                blocMacro.Add(new SetFalseCmd(LiteralMSBuildWithWin32Targets));
                SetTrueCmd setTrueCmdWin32 = new SetTrueCmd(LiteralMSBuildWithWin32Targets);
                blocMacro.AddRange(win32Modules.Select(lv => new IfCmd(new IsTrueConditional(lv.Item1), setTrueCmdWin32)));
                blocMacro.Add(Nop);

                // MSBuild Cli X86
                blocMacro.Add(new RemBatch("MSBuild configuration (x86)"));
                IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> x86Modules = ParamDescriptionHeoModules
                    .Where(pdm => pdm.Platform == BusinessModel.EnumPlatform.X86)
                    .Select(pdm => new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                        LiteralParamDescriptionByKeyWords[pdm.Keyword].LiteralBoolean
                        , LiteralModuleMSBuildTargetByKeyWords[pdm.Keyword].LiteralValue
                        ))
                    .ToArray();
                MSBuildCliMacro msbuildCliMacroX86 = GetFastBuildCoreMSBuildEnvInitMSBuildConfigMSBuildCliMacro(
                    LiteralMSBuildCliX86
                    , LiteralMSBuildPlatform.LiteralValue
                    , x86Modules);
                blocMacro.Add(msbuildCliMacroX86);
                blocMacro.Add(Nop);
                blocMacro.Add(new SetFalseCmd(LiteralMSBuildWithX86Targets));
                SetTrueCmd setTrueCmdX86 = new SetTrueCmd(LiteralMSBuildWithX86Targets);
                blocMacro.AddRange(x86Modules.Select(lv => new IfCmd(new IsTrueConditional(lv.Item1), setTrueCmdX86)));
                blocMacro.Add(Nop);

                // MSBuild check execution
                blocMacro.Add(new RemBatch("MSBuild check execution"));
                // SET /A varfb_MSBuildsWithTargets="(%varfb_MSBuildWithWin32Targets%|%varfb_MSBuildWithX86Targets%)"
                blocMacro.Add(new SetBooleanCmd(
                    LiteralMSBuildsWithTargets
                    , new BooleanOperatorExpression(
                        EnumBooleanOperator.Or
                        , LiteralMSBuildWithWin32Targets.LiteralBoolean
                        , LiteralMSBuildWithX86Targets.LiteralBoolean
                        )
                    ));
                //SET /A varfb_MSBuildWin32NeedRun="((!%varfb_MSBuildsWithTargets%)|%varfb_MSBuildWithWin32Targets%)"
                blocMacro.Add(new SetBooleanCmd(
                    LiteralMSBuildWin32NeedRun
                    , new BooleanOperatorExpression(
                        EnumBooleanOperator.Or
                        , new NotBooleanExpression(LiteralMSBuildsWithTargets.LiteralBoolean)
                        , LiteralMSBuildWithWin32Targets.LiteralBoolean
                        )
                    ));
                blocMacro.Add(new SetBooleanCmd(
                    LiteralMSBuildX86NeedRun
                    , new BooleanOperatorExpression(
                        EnumBooleanOperator.Or
                        , new NotBooleanExpression(LiteralMSBuildsWithTargets.LiteralBoolean)
                        , LiteralMSBuildWithX86Targets.LiteralBoolean
                        )
                    ));

                return blocMacro;
            }
        }

        private MSBuildCliMacro GetFastBuildCoreMSBuildEnvInitMSBuildConfigMSBuildCliMacro(LiteralBatch literalMSBuildCli, BatchExpressionBase platform, IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> targets)
        {
            MSBuildCliMacro result = new MSBuildCliMacro(
                literalMSBuildCli
                , TrueValueExpression.ValueExpression
                , TrueValueExpression.ValueExpression
                , LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchPara].LiteralBoolean
                , LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchQuiet].LiteralBoolean
                , new QuotedValueExpression(LiteralMSBuildConfiguration.LiteralValue)
                , LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchFxcop].LiteralBoolean
                , LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchNowarn].LiteralBoolean
                , LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchRebuild].LiteralBoolean
                , platform
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                    FalseValueExpression.ValueExpression
                    , LiteralMSBuildLogFile.LiteralValue
                    )
                , new Tuple<BooleanExpressionBase, BatchExpressionBase>(
                    LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchForceOutputDirPath].LiteralBoolean
                    , LiteralHeoForcedOutputDirPath.LiteralValue
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

                // Win32 building
                blocMacro.Add(FastBuildCoreBuildWin32);
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

        public BlocMacroFileNode FastBuildCoreBuildWin32
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreBuildWin32"));

                BatchFileNodeBase fastBuildCoreBuildWin32Node = GetFastBuildCoreBuild(
                    LiteralMSBuildWin32NeedRun
                    , _fastBuildModel.BaseLabelMacroMSBuildWin32NeedRun()
                    , LiteralMSBuildCliWin32
                    , _fastBuildModel.BaseLabelMacroMSBuildWin32TryLoop());
                blocMacro.Add(fastBuildCoreBuildWin32Node);

                return blocMacro;
            }
        }

        public BlocMacroFileNode FastBuildCoreBuildX86
        {
            get
            {
                BlocMacroFileNode blocMacro = new BlocMacroFileNode();

                blocMacro.Add(GetDebugRem("FastBuildCoreBuildX86"));

                BatchFileNodeBase fastBuildCoreBuildWin32Node = GetFastBuildCoreBuild(
                    LiteralMSBuildX86NeedRun
                    , _fastBuildModel.BaseLAbelMacroMSBuildX86NeedRun()
                    , LiteralMSBuildCliX86
                    , _fastBuildModel.BaseLabelMacroMSBuildX86TryLoop());
                blocMacro.Add(fastBuildCoreBuildWin32Node);

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
                    new IsTrueConditional(LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchNosgp])
                    , new SetFalseCmd(LiteralSGenPlusNeedRun)
                    ));

                // IF (%varfb_param_nosgp%)
                BlocMacroFileNode blocMacroSGenPlus = new BlocMacroFileNode();
                blocMacro.Add(new IfGotoMacro(
                    new IsTrueConditional(LiteralSGenPlusNeedRun)
                    , blocMacroSGenPlus
                    , null
                    , _fastBuildModel.BaseLabelMacroSGenPlusNeedRun()
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
                blocMacroSGenPlus.Add(new IfGotoMacro(0, blocMacroThenSGenPlusStatus, blocMacroElseSGenPlusStatus, _fastBuildModel.BaseLabelMacroSGenPlusStatus()));
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

        private BatchFileNodeBase GetFastBuildCoreBuild(LiteralBatch literalMSBuildNeedRun, string baseLabelMacroMSBuildNeedRun, LiteralBatch literalMSBuildCli, string baseLabelMacroMSBuildTryLoop)
        {
            BlocMacroFileNode macroResult = new BlocMacroFileNode();

            // lunch MSBuild if needed
            // IF (%varfb_MSBuildWin32NeedRun%) == (1) [...]
            BlocMacroFileNode macroBlocRunMSBuild = new BlocMacroFileNode();
            IfGotoMacro ifGotoMacroNeedRun = new IfGotoMacro(
                new IsTrueConditional(literalMSBuildNeedRun)
                , macroBlocRunMSBuild
                , null
                , baseLabelMacroMSBuildNeedRun
                );
            macroResult.Add(ifGotoMacroNeedRun);

            macroBlocRunMSBuild.Add(GetFastBuildCoreBuildBody(literalMSBuildCli));

            return macroResult;
        }

        private BatchFileNodeBase GetFastBuildCoreBuildBody(LiteralBatch literalMSBuildCli)
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
                nodeThen.Add(new EchoCmd(_fastBuildModel.LabelTextKillHeoVsHost()));
                nodeThen.Add(new TaskKillCmd(new ValueExpression(_fastBuildModel.ValueHeoVsHostImageName()), force: true));
                nodeThen.Add(new EchoCmd(String.Empty));

                IfMacro ifMacro = new IfMacro(
                    new IsTrueConditional(LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchVshost])
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
                nodeThen.Add(new EchoCmd(_fastBuildModel.LabelTextKillHeo()));
                nodeThen.Add(new TaskKillCmd(new ValueExpression(_fastBuildModel.ValueHeoImageName()), force: true));
                nodeThen.Add(new EchoCmd(String.Empty));

                IfMacro ifMacro = new IfMacro(
                    new IsTrueConditional(LiteralParamDescriptionByKeyWords[FastBuildParamModel.ConstKeywordParamSwitchKillheo])
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