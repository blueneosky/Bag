using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal static class ConstFBModel
    {
        public const string ConstMSBuildTargetVarNamePrefix = ConstVarNamePrefix + "msbuild_target_";
        public const string ConstParamVarNamePrefix = ConstVarNamePrefix + "param_";
        public const string ContParamSwitchPrefix = "/";

        private const string ConstLabelCallPrefix = "labelCall";
        private const string ConstLabelGotoPrefix = "labelGoto";
        private const string ConstVarNamePrefix = "varfb_";

        #region Default value

        #region ConstLabelGoto

        public const string ConstLabelGotoEndDefaultValue = ConstLabelGotoPrefix + "End";
        public const string ConstLabelGotoHelpDefaultValue = ConstLabelGotoPrefix + "Usage";
        public const string ConstLabelGotoVersionDefaultValue = ConstLabelGotoPrefix + "Version";

        #endregion ConstLabelGoto

        #region ConstBaseLabelMacro

        public const string ConstBaseLabelMacroMSBuildWin32NeedRunDefaultValue = "MSBuildWin32NeedRun";
        public const string ConstBaseLabelMacroMSBuildWin32TryLoopDefaultValue = "BaseLabelMacroMSBuildWin32TryLoop";
        public const string ConstBaseLAbelMacroMSBuildX86NeedRunDefaultValue = "MSBuildX86NeedRun";
        public const string ConstBaseLabelMacroMSBuildX86TryLoopDefaultValue = "BaseLabelMacroMSBuildX86TryLoop";
        public const string ConstBaseLabelMacroParametersParsingDefaultValue = "ParametersParsing";
        public const string ConstBaseLabelMacroSGenPlusNeedRunDefaultValue = "MacroSGenPlusNeedRun";
        public const string ConstBaseLabelMacroSGenPlusStatusDefaultValue = "MacroSGenPlusStatus";
        public const string ConstBaseLabelMacroVcvarsallDefaultValue = "Vcvarsall";
        public const string ConstBaseLabelMacroVcvarsallX32X64DefaultValue = "VcvarsallX32X64";

        #endregion ConstBaseLabelMacro

        #region ConstLiteral

        public const string ConstLiteralConfigurationPathDefaultValue = ConstVarNamePrefix + "ConfigurationPath";
        public const string ConstLiteralEnvSystemVcvarsallCheckStatusDefaultValue = "DevEnvDir";
        public const string ConstLiteralHeoForcedOutputDirPathDefaultValue = ConstVarNamePrefix + "HeoForcedOutputDirPath";
        public const string ConstLiteralHeoLanceurBinPathDefaultValue = ConstVarNamePrefix + "HeoLanceurBinPath";
        public const string ConstLiteralHeoLanceurPathDefaultValue = ConstVarNamePrefix + "HeoLanceurPath";
        public const string ConstLiteralMSBuildCliWin32DefaultValue = ConstVarNamePrefix + "MSBuild_Cli_Win32";
        public const string ConstLiteralMSBuildCliX86DefaultValue = ConstVarNamePrefix + "MSBuild_Cli_X86";
        public const string ConstLiteralMSBuildConfigurationDefaultValue = ConstVarNamePrefix + "MSBuildConfiguration";
        public const string ConstLiteralMSBuildLogFileDefaultValue = ConstVarNamePrefix + "MSBuildLogFile";
        public const string ConstLiteralMSBuildPlatformDefaultValue = ConstVarNamePrefix + "MSBuildPlatform";
        public const string ConstLiteralMSBuildsWithTargetsDefaultValue = ConstVarNamePrefix + "MSBuildsWithTargets";
        public const string ConstLiteralMSBuildTryLoopCondDefaultValue = ConstVarNamePrefix + "LiteralMSBuildTryLoopCond";
        public const string ConstLiteralMSBuildWin32NeedRunDefaultValue = ConstVarNamePrefix + "MSBuildWin32NeedRun";
        public const string ConstLiteralMSBuildWithWin32TargetsDefaultValue = ConstVarNamePrefix + "MSBuildWithWin32Targets";
        public const string ConstLiteralMSBuildWithX86TargetsDefaultValue = ConstVarNamePrefix + "MSBuildWithX86Targets";
        public const string ConstLiteralMSBuildX86NeedRunDefaultValue = ConstVarNamePrefix + "MSBuildX86NeedRun";
        public const string ConstLiteralSGenPlusCliDefaultValue = ConstVarNamePrefix + "SGenPlusCli";
        public const string ConstLiteralSGenPlusConfigFilePathDefaultValue = ConstVarNamePrefix + "SGenPlusConfigFilePath";
        public const string ConstLiteralSGenPlusNeedRunDefaultValue = ConstVarNamePrefix + "SGenPlusNeedRun";
        public const string ConstLiteralSGenPlusTargetBinaryPathDefaultValue = ConstVarNamePrefix + "SGenPlusTargetBinaryPath";
        public const string ConstLiteralStartTimeDefaultValue = ConstVarNamePrefix + "StartTime";
        public const string ConstLiteralVersionNameDefaultValue = ConstVarNamePrefix + "Version";
        public const string ConstLiteralVersionNumberNameDefaultValue = ConstVarNamePrefix + "VersionNumber";

        #endregion ConstLiteral

        #region ConstLabelText

        public const string ConstLabelTextFastBuildDefaultValue = "FastBuild";
        public const string ConstLabelTextKillHeoDefaultValue = "Kill Heo.exe";
        public const string ConstLabelTextKillHeoVsHostDefaultValue = "Stop vshost binaries";
        public const string ConstLabelTextRemSeparatorDefaultValue = "-----------------------------------------------------------------------";
        public const string ConstLabelTextSectionHeoModulesDefaultValue = "Compilation de modules séparés";
        public const string ConstLabelTextVcvarsallAlreadyinMemoryDefaultValue = "Dev env variable already in memory";
        public const string ConstLabelTextVcvarsallDefaultValue = "vcvarsall";

        #endregion ConstLabelText

        #region ConstValue

        public const string ConstValueHeoImageNameDefaultValue = "heo.lanceur.exe";
        public const string ConstValueHeoLanceurPathDefaultValue = @"..\Lanceur\Heo.Lanceur";
        public const string ConstValueHeoVsHostImageNameDefaultValue = "heo.lanceur.vshost.*";
        public const string ConstValueMSBuildConfigurationDsacDefaultValue = "Debug sans analyse de code";
        public const string ConstValueMSBuildLogFileDefaultValue = "fastbuild.log";
        public const string ConstValuePathBinDefaultValue = @"bin\x86";
        public const string ConstValuePathMeasureBuildLogFileDefaultValue = @"measure_build.log.txt";
        public const string ConstValueRelativePathVcvarsallBatchFileDefaultValue = @"\Microsoft Visual Studio 10.0\VC\vcvarsall.bat";
        public const string ConstValueSGenPlusConfigFilePathDefaultValue = "SgenPlusListeExclusion.txt";
        public const string ConstValueVersionNumberDefaultValue = "0.9999";

        #endregion ConstValue

        #region ConstLabelSub

        public const string ConstLabelSubKillHeoDefaultValue = ConstLabelCallPrefix + "KillHeo";
        public const string ConstLabelSubKillHeoVsHostDefaultValue = ConstLabelCallPrefix + "KillVsHost";

        #endregion ConstLabelSub

        #endregion Default value
    }
}