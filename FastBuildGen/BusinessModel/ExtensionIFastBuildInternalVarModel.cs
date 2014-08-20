using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel
{
    internal static class ExtensionIFastBuildInternalVarModel
    {
        public static string BaseLabelMacroMSBuildWin32NeedRun(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildWin32NeedRun];
        }

        public static string BaseLabelMacroMSBuildWin32TryLoop(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildWin32TryLoop];
        }

        public static string BaseLAbelMacroMSBuildX86NeedRun(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLAbelMacroMSBuildX86NeedRun];
        }

        public static string BaseLabelMacroMSBuildX86TryLoop(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildX86TryLoop];
        }

        public static string BaseLabelMacroParametersParsing(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroParametersParsing];
        }

        public static string BaseLabelMacroSGenPlusNeedRun(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroSGenPlusNeedRun];
        }

        public static string BaseLabelMacroSGenPlusStatus(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroSGenPlusStatus];
        }

        public static string BaseLabelMacroVcvarsall(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroVcvarsall];
        }

        public static string BaseLabelMacroVcvarsallX32X64(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstBaseLabelMacroVcvarsallX32X64];
        }

        public static string LabelGotoEnd(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelGotoEnd];
        }

        public static string LabelGotoHelp(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelGotoHelp];
        }

        public static string LabelGotoVersion(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelGotoVersion];
        }

        public static string LabelSubKillHeo(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelSubKillHeo];
        }

        public static string LabelSubKillHeoVsHost(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelSubKillHeoVsHost];
        }

        public static string LabelTextFastBuild(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextFastBuild];
        }

        public static string LabelTextKillHeo(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextKillHeo];
        }

        public static string LabelTextKillHeoVsHost(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextKillHeoVsHost];
        }

        public static string LabelTextRemSeparator(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextRemSeparator];
        }

        public static string LabelTextSectionHeoModules(this  IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextSectionHeoModules];
        }

        public static string LabelTextVcvarsall(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextVcvarsall];
        }

        public static string LabelTextVcvarsallAlreadyinMemory(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLabelTextVcvarsallAlreadyinMemory];
        }

        public static string LiteralConfigurationPath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralConfigurationPath];
        }

        public static string LiteralEnvSystemVcvarsallCheckStatus(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralEnvSystemVcvarsallCheckStatus];
        }

        public static string LiteralHeoForcedOutputDirPath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralHeoForcedOutputDirPath];
        }

        public static string LiteralHeoLanceurBinPath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralHeoLanceurBinPath];
        }

        public static string LiteralHeoLanceurPath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralHeoLanceurPath];
        }

        public static string LiteralMSBuildCliWin32(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildCliWin32];
        }

        public static string LiteralMSBuildCliX86(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildCliX86];
        }

        public static string LiteralMSBuildConfiguration(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildConfiguration];
        }

        public static string LiteralMSBuildLogFile(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildLogFile];
        }

        public static string LiteralMSBuildPlatform(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildPlatform];
        }

        public static string LiteralMSBuildsWithTargets(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildsWithTargets];
        }

        public static string LiteralMSBuildTryLoopCond(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildTryLoopCond];
        }

        public static string LiteralMSBuildWin32NeedRun(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWin32NeedRun];
        }

        public static string LiteralMSBuildWithWin32Targets(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWithWin32Targets];
        }

        public static string LiteralMSBuildWithX86Targets(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWithX86Targets];
        }

        public static string LiteralMSBuildX86NeedRun(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralMSBuildX86NeedRun];
        }

        public static string LiteralSGenPlusCli(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusCli];
        }

        public static string LiteralSGenPlusConfigFilePath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusConfigFilePath];
        }

        public static string LiteralSGenPlusNeedRun(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusNeedRun];
        }

        public static string LiteralSGenPlusTargetBinaryPath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusTargetBinaryPath];
        }

        public static string LiteralStartTime(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralStartTime];
        }

        public static string LiteralVersionName(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralVersionName];
        }

        public static string LiteralVersionNumberName(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstLiteralVersionNumberName];
        }

        public static string ValueHeoImageName(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueHeoImageName];
        }

        public static string ValueHeoLanceurPath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueHeoLanceurPath];
        }

        public static string ValueHeoVsHostImageName(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueHeoVsHostImageName];
        }

        public static string ValueMSBuildConfigurationDsac(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueMSBuildConfigurationDsac];
        }

        public static string ValueMSBuildLogFile(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueMSBuildLogFile];
        }

        public static string ValuePathBin(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValuePathBin];
        }

        public static string ValuePathMeasureBuildLogFile(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValuePathMeasureBuildLogFile];
        }

        public static string ValueRelativePathVcvarsallBatchFile(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueRelativePathVcvarsallBatchFile];
        }

        public static string ValueSGenPlusConfigFilePath(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueSGenPlusConfigFilePath];
        }

        public static string ValueVersionNumber(this IFastBuildInternalVarModel model)
        {
            return model[ConstFastBuildInternalVarModel.ConstValueVersionNumber];
        }
    }
}