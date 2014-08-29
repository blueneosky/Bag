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

        #region InternalVar

        #region ConstLabelGoto

        public const string ConstLabelGotoEnd = "LabelGotoEnd";
        public const string ConstLabelGotoHelp = "LabelGotoHelp";
        public const string ConstLabelGotoVersion = "LabelGotoVersion";

        #endregion ConstLabelGoto

        #region ConstBaseLabelMacro

        public const string ConstBaseLabelMacroMSBuildWin32NeedRun = "BaseLabelMacroMSBuildWin32NeedRun";
        public const string ConstBaseLabelMacroMSBuildWin32TryLoop = "BaseLabelMacroMSBuildWin32TryLoop";
        public const string ConstBaseLAbelMacroMSBuildX86NeedRun = "BaseLAbelMacroMSBuildX86NeedRun";
        public const string ConstBaseLabelMacroMSBuildX86TryLoop = "BaseLabelMacroMSBuildX86TryLoop";
        public const string ConstBaseLabelMacroParametersParsing = "BaseLabelMacroParametersParsing";
        public const string ConstBaseLabelMacroSGenPlusNeedRun = "BaseLabelMacroSGenPlusNeedRun";
        public const string ConstBaseLabelMacroSGenPlusStatus = "BaseLabelMacroSGenPlusStatus";
        public const string ConstBaseLabelMacroVcvarsall = "BaseLabelMacroVcvarsall";
        public const string ConstBaseLabelMacroVcvarsallX32X64 = "BaseLabelMacroVcvarsallX32X64";

        #endregion ConstBaseLabelMacro

        #region ConstLiteral

        public const string ConstLiteralConfigurationPath = "LiteralConfigurationPath";
        public const string ConstLiteralEnvSystemVcvarsallCheckStatus = "LiteralEnvSystemVcvarsallCheckStatus";
        public const string ConstLiteralHeoForcedOutputDirPath = "LiteralHeoForcedOutputDirPath";
        public const string ConstLiteralHeoLanceurBinPath = "LiteralHeoLanceurBinPath";
        public const string ConstLiteralHeoLanceurPath = "LiteralHeoLanceurPath";
        public const string ConstLiteralMSBuildCliWin32 = "LiteralMSBuildCliWin32";
        public const string ConstLiteralMSBuildCliX86 = "LiteralMSBuildCliX86";
        public const string ConstLiteralMSBuildConfiguration = "LiteralMSBuildConfiguration";
        public const string ConstLiteralMSBuildLogFile = "LiteralMSBuildLogFile";
        public const string ConstLiteralMSBuildPlatform = "LiteralMSBuildPlatform";
        public const string ConstLiteralMSBuildsWithTargets = "LiteralMSBuildsWithTargets";
        public const string ConstLiteralMSBuildTryLoopCond = "LiteralMSBuildTryLoopCond";
        public const string ConstLiteralMSBuildWin32NeedRun = "LiteralMSBuildWin32NeedRun";
        public const string ConstLiteralMSBuildWithWin32Targets = "LiteralMSBuildWithWin32Targets";
        public const string ConstLiteralMSBuildWithX86Targets = "LiteralMSBuildWithX86Targets";
        public const string ConstLiteralMSBuildX86NeedRun = "LiteralMSBuildX86NeedRun";
        public const string ConstLiteralSGenPlusCli = "LiteralSGenPlusCli";
        public const string ConstLiteralSGenPlusConfigFilePath = "LiteralSGenPlusConfigFilePath";
        public const string ConstLiteralSGenPlusNeedRun = "LiteralSGenPlusNeedRun";
        public const string ConstLiteralSGenPlusTargetBinaryPath = "LiteralSGenPlusTargetBinaryPath";
        public const string ConstLiteralStartTime = "LiteralStartTime";
        public const string ConstLiteralVersionName = "LiteralVersionName";
        public const string ConstLiteralVersionNumberName = "LiteralVersionNumberName";

        #endregion ConstLiteral

        #region ConstLabelText

        public const string ConstLabelTextFastBuild = "LabelTextFastBuild";
        public const string ConstLabelTextKillHeo = "LabelTextKillHeo";
        public const string ConstLabelTextKillHeoVsHost = "LabelTextKillHeoVsHost";
        public const string ConstLabelTextRemSeparator = "LabelTextRemSeparator";
        public const string ConstLabelTextSectionHeoModules = "LabelTextSectionHeoModules";
        public const string ConstLabelTextVcvarsall = "LabelTextVcvarsall";
        public const string ConstLabelTextVcvarsallAlreadyinMemory = "LabelTextVcvarsallAlreadyinMemory";

        #endregion ConstLabelText

        #region ConstValue

        public const string ConstValueHeoImageName = "ValueHeoImageName";
        public const string ConstValueHeoLanceurPath = "ValueHeoLanceurPath";
        public const string ConstValueHeoVsHostImageName = "ValueHeoVsHostImageName";
        public const string ConstValueMSBuildConfigurationDsac = "ValueMSBuildConfigurationDsac";
        public const string ConstValueMSBuildLogFile = "ValueMSBuildLogFile";
        public const string ConstValuePathBin = "ValuePathBin";
        public const string ConstValuePathMeasureBuildLogFile = "ValuePathMeasureBuildLogFile";
        public const string ConstValueRelativePathVcvarsallBatchFile = "ValueRelativePathVcvarsallBatchFile";
        public const string ConstValueSGenPlusConfigFilePath = "ValueSGenPlusConfigFilePath";
        public const string ConstValueVersionNumber = "ValueVersionNumber";

        #endregion ConstValue

        #region ConstLabelSub

        public const string ConstLabelSubKillHeo = "LabelSubKillHeo";
        public const string ConstLabelSubKillHeoVsHost = "LabelSubKillHeoVsHost";

        #endregion ConstLabelSub

        public static IDictionary<string, string> InternalVarDefaultProperties
        {
            get
            {
                Dictionary<string, string> result = new Dictionary<string, string> { };

                result[ConstLabelGotoHelp] = ConstLabelGotoHelpDefaultValue;
                result[ConstLabelGotoEnd] = ConstLabelGotoEndDefaultValue;
                result[ConstLiteralVersionNumberName] = ConstLiteralVersionNumberNameDefaultValue;
                result[ConstLiteralVersionName] = ConstLiteralVersionNameDefaultValue;
                result[ConstValueVersionNumber] = ConstValueVersionNumberDefaultValue;
                result[ConstLabelTextFastBuild] = ConstLabelTextFastBuildDefaultValue;
                result[ConstLiteralEnvSystemVcvarsallCheckStatus] = ConstLiteralEnvSystemVcvarsallCheckStatusDefaultValue;
                result[ConstBaseLabelMacroVcvarsall] = ConstBaseLabelMacroVcvarsallDefaultValue;
                result[ConstBaseLabelMacroVcvarsallX32X64] = ConstBaseLabelMacroVcvarsallX32X64DefaultValue;
                result[ConstLabelTextVcvarsall] = ConstLabelTextVcvarsallDefaultValue;
                result[ConstValueRelativePathVcvarsallBatchFile] = ConstValueRelativePathVcvarsallBatchFileDefaultValue;
                result[ConstLiteralStartTime] = ConstLiteralStartTimeDefaultValue;
                result[ConstBaseLabelMacroParametersParsing] = ConstBaseLabelMacroParametersParsingDefaultValue;
                result[ConstLabelGotoVersion] = ConstLabelGotoVersionDefaultValue;
                result[ConstValuePathMeasureBuildLogFile] = ConstValuePathMeasureBuildLogFileDefaultValue;
                result[ConstLabelTextVcvarsallAlreadyinMemory] = ConstLabelTextVcvarsallAlreadyinMemoryDefaultValue;
                result[ConstLabelTextRemSeparator] = ConstLabelTextRemSeparatorDefaultValue;
                result[ConstLabelSubKillHeoVsHost] = ConstLabelSubKillHeoVsHostDefaultValue;
                result[ConstLabelSubKillHeo] = ConstLabelSubKillHeoDefaultValue;
                result[ConstLabelTextSectionHeoModules] = ConstLabelTextSectionHeoModulesDefaultValue;
                result[ConstLabelTextKillHeoVsHost] = ConstLabelTextKillHeoVsHostDefaultValue;
                result[ConstLabelTextKillHeo] = ConstLabelTextKillHeoDefaultValue;
                result[ConstValueHeoVsHostImageName] = ConstValueHeoVsHostImageNameDefaultValue;
                result[ConstValueHeoImageName] = ConstValueHeoImageNameDefaultValue;
                result[ConstLiteralMSBuildLogFile] = ConstLiteralMSBuildLogFileDefaultValue;
                result[ConstValueMSBuildLogFile] = ConstValueMSBuildLogFileDefaultValue;
                result[ConstLiteralSGenPlusConfigFilePath] = ConstLiteralSGenPlusConfigFilePathDefaultValue;
                result[ConstValueSGenPlusConfigFilePath] = ConstValueSGenPlusConfigFilePathDefaultValue;
                result[ConstLiteralHeoForcedOutputDirPath] = ConstLiteralHeoForcedOutputDirPathDefaultValue;
                result[ConstLiteralMSBuildPlatform] = ConstLiteralMSBuildPlatformDefaultValue;
                result[ConstLiteralMSBuildConfiguration] = ConstLiteralMSBuildConfigurationDefaultValue;
                result[ConstValueMSBuildConfigurationDsac] = ConstValueMSBuildConfigurationDsacDefaultValue;
                result[ConstLiteralMSBuildCliWin32] = ConstLiteralMSBuildCliWin32DefaultValue;
                result[ConstLiteralMSBuildCliX86] = ConstLiteralMSBuildCliX86DefaultValue;
                result[ConstLiteralMSBuildWithWin32Targets] = ConstLiteralMSBuildWithWin32TargetsDefaultValue;
                result[ConstLiteralMSBuildWithX86Targets] = ConstLiteralMSBuildWithX86TargetsDefaultValue;
                result[ConstLiteralMSBuildsWithTargets] = ConstLiteralMSBuildsWithTargetsDefaultValue;
                result[ConstLiteralMSBuildWin32NeedRun] = ConstLiteralMSBuildWin32NeedRunDefaultValue;
                result[ConstLiteralMSBuildX86NeedRun] = ConstLiteralMSBuildX86NeedRunDefaultValue;
                result[ConstBaseLabelMacroMSBuildWin32NeedRun] = ConstBaseLabelMacroMSBuildWin32NeedRunDefaultValue;
                result[ConstBaseLAbelMacroMSBuildX86NeedRun] = ConstBaseLAbelMacroMSBuildX86NeedRunDefaultValue;
                result[ConstLiteralMSBuildTryLoopCond] = ConstLiteralMSBuildTryLoopCondDefaultValue;
                result[ConstBaseLabelMacroMSBuildWin32TryLoop] = ConstBaseLabelMacroMSBuildWin32TryLoopDefaultValue;
                result[ConstBaseLabelMacroMSBuildX86TryLoop] = ConstBaseLabelMacroMSBuildX86TryLoopDefaultValue;
                result[ConstBaseLabelMacroSGenPlusNeedRun] = ConstBaseLabelMacroSGenPlusNeedRunDefaultValue;
                result[ConstBaseLabelMacroSGenPlusStatus] = ConstBaseLabelMacroSGenPlusStatusDefaultValue;
                result[ConstLiteralSGenPlusNeedRun] = ConstLiteralSGenPlusNeedRunDefaultValue;
                result[ConstLiteralHeoLanceurPath] = ConstLiteralHeoLanceurPathDefaultValue;
                result[ConstLiteralHeoLanceurBinPath] = ConstLiteralHeoLanceurBinPathDefaultValue;
                result[ConstValueHeoLanceurPath] = ConstValueHeoLanceurPathDefaultValue;
                result[ConstLiteralConfigurationPath] = ConstLiteralConfigurationPathDefaultValue;
                result[ConstValuePathBin] = ConstValuePathBinDefaultValue;
                result[ConstLiteralSGenPlusTargetBinaryPath] = ConstLiteralSGenPlusTargetBinaryPathDefaultValue;
                result[ConstLiteralSGenPlusCli] = ConstLiteralSGenPlusCliDefaultValue;

                return result;
            }
        }

        #endregion
    }
}