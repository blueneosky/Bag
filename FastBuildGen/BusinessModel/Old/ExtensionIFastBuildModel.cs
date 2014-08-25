using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    internal static class ExtensionIFastBuildModel
    {
        #region FastBuildInternalVarModel

        public static string BaseLabelMacroMSBuildWin32NeedRun(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroMSBuildWin32NeedRun();
        }

        public static string BaseLabelMacroMSBuildWin32TryLoop(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroMSBuildWin32TryLoop();
        }

        public static string BaseLAbelMacroMSBuildX86NeedRun(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLAbelMacroMSBuildX86NeedRun();
        }

        public static string BaseLabelMacroMSBuildX86TryLoop(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroMSBuildX86TryLoop();
        }

        public static string BaseLabelMacroParametersParsing(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroParametersParsing();
        }

        public static string BaseLabelMacroSGenPlusNeedRun(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroSGenPlusNeedRun();
        }

        public static string BaseLabelMacroSGenPlusStatus(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroSGenPlusStatus();
        }

        public static string BaseLabelMacroVcvarsall(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroVcvarsall();
        }

        public static string BaseLabelMacroVcvarsallX32X64(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.BaseLabelMacroVcvarsallX32X64();
        }

        public static string LabelGotoEnd(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelGotoEnd();
        }

        public static string LabelGotoHelp(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelGotoHelp();
        }

        public static string LabelGotoVersion(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelGotoVersion();
        }

        public static string LabelSubKillHeo(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelSubKillHeo();
        }

        public static string LabelSubKillHeoVsHost(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelSubKillHeoVsHost();
        }

        public static string LabelTextFastBuild(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextFastBuild();
        }

        public static string LabelTextKillHeo(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextKillHeo();
        }

        public static string LabelTextKillHeoVsHost(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextKillHeoVsHost();
        }

        public static string LabelTextRemSeparator(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextRemSeparator();
        }

        public static string LabelTextSectionHeoModules(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextSectionHeoModules();
        }

        public static string LabelTextVcvarsall(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextVcvarsall();
        }

        public static string LabelTextVcvarsallAlreadyinMemory(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LabelTextVcvarsallAlreadyinMemory();
        }

        public static string LiteralConfigurationPath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralConfigurationPath();
        }

        public static string LiteralEnvSystemVcvarsallCheckStatus(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralEnvSystemVcvarsallCheckStatus();
        }

        public static string LiteralHeoForcedOutputDirPath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralHeoForcedOutputDirPath();
        }

        public static string LiteralHeoLanceurBinPath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralHeoLanceurBinPath();
        }

        public static string LiteralHeoLanceurPath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralHeoLanceurPath();
        }

        public static string LiteralMSBuildCliWin32(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildCliWin32();
        }

        public static string LiteralMSBuildCliX86(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildCliX86();
        }

        public static string LiteralMSBuildConfiguration(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildConfiguration();
        }

        public static string LiteralMSBuildLogFile(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildLogFile();
        }

        public static string LiteralMSBuildPlatform(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildPlatform();
        }

        public static string LiteralMSBuildsWithTargets(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildsWithTargets();
        }

        public static string LiteralMSBuildTryLoopCond(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildTryLoopCond();
        }

        public static string LiteralMSBuildWin32NeedRun(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildWin32NeedRun();
        }

        public static string LiteralMSBuildWithWin32Targets(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildWithWin32Targets();
        }

        public static string LiteralMSBuildWithX86Targets(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildWithX86Targets();
        }

        public static string LiteralMSBuildX86NeedRun(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralMSBuildX86NeedRun();
        }

        public static string LiteralSGenPlusCli(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralSGenPlusCli();
        }

        public static string LiteralSGenPlusConfigFilePath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralSGenPlusConfigFilePath();
        }

        public static string LiteralSGenPlusNeedRun(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralSGenPlusNeedRun();
        }

        public static string LiteralSGenPlusTargetBinaryPath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralSGenPlusTargetBinaryPath();
        }

        public static string LiteralStartTime(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralStartTime();
        }

        public static string LiteralVersionName(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralVersionName();
        }

        public static string LiteralVersionNumberName(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.LiteralVersionNumberName();
        }

        public static string ValueHeoImageName(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueHeoImageName();
        }

        public static string ValueHeoLanceurPath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueHeoLanceurPath();
        }

        public static string ValueHeoVsHostImageName(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueHeoVsHostImageName();
        }

        public static string ValueMSBuildConfigurationDsac(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueMSBuildConfigurationDsac();
        }

        public static string ValueMSBuildLogFile(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueMSBuildLogFile();
        }

        public static string ValuePathBin(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValuePathBin();
        }

        public static string ValuePathMeasureBuildLogFile(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValuePathMeasureBuildLogFile();
        }

        public static string ValueRelativePathVcvarsallBatchFile(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueRelativePathVcvarsallBatchFile();
        }

        public static string ValueSGenPlusConfigFilePath(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueSGenPlusConfigFilePath();
        }

        public static string ValueVersionNumber(this IFastBuildModel model)
        {
            return model.FastBuildInternalVarModel.ValueVersionNumber();
        }

        #endregion FastBuildInternalVarModel
    }
}