using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatchGen.BatchNode.Boolean;
using BatchGen.BatchNode.Cmd;
using BatchGen.BatchNode.Expression;
using BatchGen.BatchNode.Integer;
using BatchGen.BatchNode.Macro;

namespace BatchGen.BatchNode.ExternCmd
{
    public class MSBuildCliMacro : BatchStatementNodeBase
    {
        private const string ConstFormatParmaPlatform = "/p:Platform=";
        private Tuple<BooleanExpressionBase, BatchExpressionBase> _baseOutputPath;
        private BatchExpressionBase _configuration;
        private Tuple<BooleanExpressionBase, BatchExpressionBase> _errorsLogFile;
        private LiteralBatch _literalMSBuildCli;
        private BlocMacro _macroMSBuildCache;
        private BooleanExpressionBase _multiprocessor;
        private BooleanExpressionBase _noLogo;
        private BooleanExpressionBase _noReuseMSBuild;
        private BooleanExpressionBase _noWarn;
        private BatchExpressionBase _platform;
        private BooleanExpressionBase _quiet;
        private BooleanExpressionBase _rebuild;
        private BooleanExpressionBase _runCodeAnalysis;
        private IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> _targets;

        public MSBuildCliMacro(
              LiteralBatch literalMSBuildCli
            , BooleanExpressionBase noLogo
            , BooleanExpressionBase noReuseMSBuild
            , BooleanExpressionBase multiprocessor
            , BooleanExpressionBase quiet
            , BatchExpressionBase configuration
            , BooleanExpressionBase runCodeAnalysis
            , BooleanExpressionBase noWarn
            , BooleanExpressionBase rebuild
            , EnumPlatform platform
            , Tuple<BooleanExpressionBase, BatchExpressionBase> errorsLogFile
            , Tuple<BooleanExpressionBase, BatchExpressionBase> baseOutputPath
            , IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> targets
            )
        {
            _literalMSBuildCli = literalMSBuildCli;
            _noLogo = noLogo;
            _noReuseMSBuild = noReuseMSBuild;
            _multiprocessor = multiprocessor;
            _quiet = quiet;
            _configuration = configuration;
            _runCodeAnalysis = runCodeAnalysis;
            _noWarn = noWarn;
            _rebuild = rebuild;
            _platform = MSBuildCmd.ConstExpressionByPlatforms[platform];
            _errorsLogFile = errorsLogFile;
            _baseOutputPath = baseOutputPath;
            _targets = targets != null ? targets.ToArray() : new Tuple<BooleanExpressionBase, BatchExpressionBase>[] { };
        }

        public MSBuildCliMacro(
              LiteralBatch literalMSBuildCli
            , BooleanExpressionBase noLogo
            , BooleanExpressionBase noReuseMSBuild
            , BooleanExpressionBase multiprocessor
            , BooleanExpressionBase quiet
            , BatchExpressionBase configuration
            , BooleanExpressionBase runCodeAnalysis
            , BooleanExpressionBase noWarn
            , BooleanExpressionBase rebuild
            , BatchExpressionBase platform
            , Tuple<BooleanExpressionBase, BatchExpressionBase> errorsLogFile
            , Tuple<BooleanExpressionBase, BatchExpressionBase> baseOutputPath
            , IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> targets
            )
        {
            _literalMSBuildCli = literalMSBuildCli;
            _noLogo = noLogo;
            _noReuseMSBuild = noReuseMSBuild;
            _multiprocessor = multiprocessor;
            _quiet = quiet;
            _configuration = configuration;
            _runCodeAnalysis = runCodeAnalysis;
            _noWarn = noWarn;
            _rebuild = rebuild;
            _platform = platform;
            _errorsLogFile = errorsLogFile;
            _baseOutputPath = baseOutputPath;
            _targets = targets != null ? targets.ToArray() : new Tuple<BooleanExpressionBase, BatchExpressionBase>[] { };
        }

        public Tuple<BooleanExpressionBase, BatchExpressionBase> BaseOutputPath
        {
            get { return _baseOutputPath; }
        }

        public BatchExpressionBase Configuration
        {
            get { return _configuration; }
        }

        public Tuple<BooleanExpressionBase, BatchExpressionBase> ErrorsLogFile
        {
            get { return _errorsLogFile; }
        }

        public LiteralBatch LiteralMSBuildCli
        {
            get { return _literalMSBuildCli; }
        }

        public BlocMacro MacroMSBuild
        {
            get
            {
                if (_macroMSBuildCache == null)
                    _macroMSBuildCache = GetMacroMSBuild();
                return _macroMSBuildCache;
            }
        }

        public BooleanExpressionBase Multiprocessor
        {
            get { return _multiprocessor; }
        }

        public BooleanExpressionBase NoLogo
        {
            get { return _noLogo; }
        }

        public BooleanExpressionBase NoReuseMSBuild
        {
            get { return _noReuseMSBuild; }
        }

        public BooleanExpressionBase NoWarn
        {
            get { return _noWarn; }
        }

        public BatchExpressionBase Platform
        {
            get { return _platform; }
        }

        public BooleanExpressionBase Quiet
        {
            get { return _quiet; }
        }

        public BooleanExpressionBase Rebuild
        {
            get { return _rebuild; }
        }

        public BooleanExpressionBase RunCodeAnalysis
        {
            get { return _runCodeAnalysis; }
        }

        public IEnumerable<Tuple<BooleanExpressionBase, BatchExpressionBase>> Targets
        {
            get { return _targets; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }

        private static void AddMSBuildCli(BlocMacro macro, List<BatchExpressionBase> staticClis, LiteralBatch literalMSBuildCli, BatchExpressionBase expression, BatchExpressionBase trueCli, BatchExpressionBase falseCli)
        {
            if (expression is LiteralIntegerExpression
                || expression is LiteralBooleanExpression
                || expression is LiteralValueExpression)
            {
                ConditionalCodeNode conditional = new ConditionalCodeNode(expression, new ValueExpression(String.Empty), true);
                AddMSBuildCli(macro, staticClis, literalMSBuildCli, conditional, trueCli, falseCli);
            }
            else
            {
                AddMSBuildCli(macro, staticClis, literalMSBuildCli, TrueValueExpression.ValueExpression, new ComposedExpression(new ValueExpression("/p:BaseOutputPath="), expression), null);
            }
        }

        private static void AddMSBuildCli(BlocMacro macro, List<BatchExpressionBase> staticClis, LiteralBatch literalMSBuildCli, BooleanExpressionBase expression, BatchExpressionBase trueCli, BatchExpressionBase falseCli)
        {
            if (expression == null)
                expression = FalseValueExpression.ValueExpression;

            BooleanValueExpressionBase booleanValue = expression as BooleanValueExpressionBase;
            if (booleanValue != null)
            {
                bool withTrueCli = null != trueCli;
                bool withFalseCli = null != falseCli;

                // static cli
                bool isTrue = (booleanValue != FalseValueExpression.ValueExpression);
                if (isTrue)
                {
                    if (withTrueCli)
                        staticClis.Add(trueCli);
                }
                else
                {
                    if (withFalseCli)
                        staticClis.Add(falseCli);
                }
            }
            else
            {
                ConditionalCodeNode conditional = new IsTrueConditional(expression);
                AddMSBuildCli(macro, staticClis, literalMSBuildCli, conditional, trueCli, falseCli);
            }
        }

        private static void AddMSBuildCli(BlocMacro macro, List<BatchExpressionBase> staticClis, LiteralBatch literalMSBuildCli, ConditionalCodeNode conditional, BatchExpressionBase trueCli, BatchExpressionBase falseCli)
        {
            bool withTrueCli = null != trueCli;
            bool withFalseCli = null != falseCli;

            // dynamic cli
            bool withTrueAndFalseCli = withTrueCli && withFalseCli;
            trueCli = withTrueCli ? new ComposedExpression(literalMSBuildCli.LiteralValue, new ValueExpression(" "), trueCli) : null;
            falseCli = withFalseCli ? new ComposedExpression(literalMSBuildCli.LiteralValue, new ValueExpression(" "), falseCli) : null;
            if (withTrueAndFalseCli)
            {
                macro.Add(new IfMacro(
                    conditional
                    , new BlocMacro { new SetExpressionCmd(literalMSBuildCli, trueCli) }
                    , new BlocMacro { new SetExpressionCmd(literalMSBuildCli, falseCli) }
                    ));
            }
            else if (withTrueCli)
            {
                macro.Add(new IfCmd(
                    conditional
                    , new SetExpressionCmd(literalMSBuildCli, trueCli)
                    ));
            }
            else if (withFalseCli)
            {
                macro.Add(new IfCmd(
                    new NotConditionalCodeNode(conditional)
                    , new SetExpressionCmd(literalMSBuildCli, falseCli)
                    ));
            }
        }

        private static BatchExpressionBase FormatParmaPlatform(BatchExpressionBase platform)
        {
            return new ComposedExpression(new ValueExpression(ConstFormatParmaPlatform), platform);
        }

        private BlocMacro GetMacroMSBuild()
        {
            BlocMacro macro = new BlocMacro();
            BlocMacro setsMacro = new BlocMacro();

            List<BatchExpressionBase> staticClis = new List<BatchExpressionBase> { };

            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, NoLogo, new ValueExpression("/nologo"), null);
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, NoReuseMSBuild, new ValueExpression("/nr:false"), null);
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, Multiprocessor, new ValueExpression("/m"), null);
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, Quiet, new ValueExpression("/v:q"), new ValueExpression("/v:m"));
            if (Configuration != null)
                AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, TrueValueExpression.ValueExpression, new ComposedExpression(new ValueExpression("/p:Configuration="), Configuration), null);
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, RunCodeAnalysis, null, new ValueExpression("/p:RunCodeAnalysis=false"));
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, NoWarn, new ValueExpression("/p:WarningLevel=0"), null);
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, Rebuild, new ValueExpression("/t:rebuild"), null);
            AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, BooleanValueExpressionBase.GetExpressionValue(null != Platform), FormatParmaPlatform(Platform), null);
            if (ErrorsLogFile != null)
                AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, ErrorsLogFile.Item1, new ComposedExpression(new ValueExpression("/fl /flp:errorsonly;logfile="), ErrorsLogFile.Item2), null);
            if (BaseOutputPath != null)
                AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, BaseOutputPath.Item1, new ComposedExpression(new ValueExpression("/p:BaseOutputPath="), BaseOutputPath.Item2), null);

            if (Targets.Any())
            {
                foreach (Tuple<BooleanExpressionBase, BatchExpressionBase> target in Targets)
                {
                    AddMSBuildCli(setsMacro, staticClis, LiteralMSBuildCli, target.Item1, new ComposedExpression(new ValueExpression("/t:"), target.Item2), null);
                }
            }

            BatchExpressionBase staticCli;
            if (staticClis.Any())
            {
                staticClis = Enumerable.Concat(
                        staticClis.Take(1)
                        , staticClis.Skip(1).Aggregate(Enumerable.Empty<BatchExpressionBase>(), (acc, e) => acc.Concat(new BatchExpressionBase[] { new ValueExpression(" "), e }))
                    )
                .ToList();
                staticCli = new ComposedExpression(staticClis.ToArray());
            }
            else
            {
                staticCli = new ValueExpression(String.Empty);
            }
            macro.Add(new SetExpressionCmd(LiteralMSBuildCli, staticCli));
            macro.Add(setsMacro);

            return macro;
        }
    }
}