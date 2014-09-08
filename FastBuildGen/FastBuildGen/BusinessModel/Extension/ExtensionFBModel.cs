using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Extension
{
    internal static class ExtensionFBModel
    {
        public static string BaseLabelMacroParametersParsing(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstBaseLabelMacroParametersParsing];
        }

        public static string BaseLabelMacroSGenPlusNeedRun(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstBaseLabelMacroSGenPlusNeedRun];
        }

        public static string BaseLabelMacroSGenPlusStatus(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstBaseLabelMacroSGenPlusStatus];
        }

        public static string BaseLabelMacroVcvarsall(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstBaseLabelMacroVcvarsall];
        }

        public static string BaseLabelMacroVcvarsallX32X64(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstBaseLabelMacroVcvarsallX32X64];
        }

        public static string LabelGotoEnd(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelGotoEnd];
        }

        public static string LabelGotoHelp(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelGotoHelp];
        }

        public static string LabelGotoVersion(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelGotoVersion];
        }

        public static string LabelSubKillHeo(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelSubKillHeo];
        }

        public static string LabelSubKillHeoVsHost(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelSubKillHeoVsHost];
        }

        public static string LabelTextFastBuild(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextFastBuild];
        }

        public static string LabelTextKillHeo(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextKillHeo];
        }

        public static string LabelTextKillHeoVsHost(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextKillHeoVsHost];
        }

        public static string LabelTextRemSeparator(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextRemSeparator];
        }

        public static string LabelTextSectionHeoModules(this  FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextSectionHeoModules];
        }

        public static string LabelTextVcvarsall(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextVcvarsall];
        }

        public static string LabelTextVcvarsallAlreadyinMemory(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLabelTextVcvarsallAlreadyinMemory];
        }

        public static string LiteralConfigurationPath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralConfigurationPath];
        }

        public static string LiteralEnvSystemVcvarsallCheckStatus(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralEnvSystemVcvarsallCheckStatus];
        }

        public static string LiteralHeoLanceurBinPath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralHeoLanceurBinPath];
        }

        public static string LiteralHeoLanceurPath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralHeoLanceurPath];
        }

        public static string LiteralMSBuildCliX86(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildCli];
        }

        public static string LiteralMSBuildConfiguration(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildConfiguration];
        }

        public static string LiteralMSBuildLogFile(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildLogFile];
        }

        public static string LiteralMSBuildPlatform(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildPlatform];
        }

        public static string LiteralMSBuildsWithTargets(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildsWithTargets];
        }

        public static string LiteralMSBuildTryLoopCond(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildTryLoopCond];
        }

        public static string LiteralMSBuildWithX86Targets(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralMSBuildWithX86Targets];
        }

        public static string LiteralSGenPlusCli(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralSGenPlusCli];
        }

        public static string LiteralSGenPlusConfigFilePath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralSGenPlusConfigFilePath];
        }

        public static string LiteralSGenPlusNeedRun(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralSGenPlusNeedRun];
        }

        public static string LiteralSGenPlusTargetBinaryPath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralSGenPlusTargetBinaryPath];
        }

        public static string LiteralStartTime(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralStartTime];
        }

        public static string LiteralVersionName(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralVersionName];
        }

        public static string LiteralVersionNumberName(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstLiteralVersionNumberName];
        }

        public static string ValueHeoImageName(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueHeoImageName];
        }

        public static string ValueHeoLanceurPath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueHeoLanceurPath];
        }

        public static string ValueHeoVsHostImageName(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueHeoVsHostImageName];
        }

        public static string ValueMSBuildConfigurationDsac(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueMSBuildConfigurationDsac];
        }

        public static string ValueMSBuildLogFile(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueMSBuildLogFile];
        }

        public static string ValuePathBin(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValuePathBin];
        }

        public static string ValuePathMeasureBuildLogFile(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValuePathMeasureBuildLogFile];
        }

        public static string ValueRelativePathVcvarsallBatchFile(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueRelativePathVcvarsallBatchFile];
        }

        public static string ValueSGenPlusConfigFilePath(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueSGenPlusConfigFilePath];
        }

        public static string ValueVersionNumber(this FBModel model)
        {
            return model.InternalVars[ConstFBModel.ConstValueVersionNumber];
        }
    }
}