using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel.Old
{
    internal class FastBuildInternalVarModel : IFastBuildInternalVarModel
    {
        #region Members

        private readonly ObservableDictionary<string, string> _properties;

        #endregion Members

        #region ctor

        public FastBuildInternalVarModel()
        {
            _properties = new ObservableDictionary<string, string>();

            _properties.CollectionChanged += _properties_CollectionChanged;
        }

        ~FastBuildInternalVarModel()
        {
            _properties.CollectionChanged -= _properties_CollectionChanged;
        }

        private void _properties_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertiesChanged(this, e);
        }

        #endregion ctor

        #region Initialize

        public void Initialize()
        {
            ResetToDefault();
        }

        #endregion Initialize

        #region Properties

        public IDictionary<string, string> DefaultProperties
        {
            get
            {
                Dictionary<string, string> result = new Dictionary<string, string> { };

                result[ConstFastBuildInternalVarModel.ConstLabelGotoHelp] = ConstFBModel.ConstLabelGotoHelpDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelGotoEnd] = ConstFBModel.ConstLabelGotoEndDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralVersionNumberName] = ConstFBModel.ConstLiteralVersionNumberNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralVersionName] = ConstFBModel.ConstLiteralVersionNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueVersionNumber] = ConstFBModel.ConstValueVersionNumberDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextFastBuild] = ConstFBModel.ConstLabelTextFastBuildDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralEnvSystemVcvarsallCheckStatus] = ConstFBModel.ConstLiteralEnvSystemVcvarsallCheckStatusDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroVcvarsall] = ConstFBModel.ConstBaseLabelMacroVcvarsallDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroVcvarsallX32X64] = ConstFBModel.ConstBaseLabelMacroVcvarsallX32X64DefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextVcvarsall] = ConstFBModel.ConstLabelTextVcvarsallDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueRelativePathVcvarsallBatchFile] = ConstFBModel.ConstValueRelativePathVcvarsallBatchFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralStartTime] = ConstFBModel.ConstLiteralStartTimeDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroParametersParsing] = ConstFBModel.ConstBaseLabelMacroParametersParsingDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelGotoVersion] = ConstFBModel.ConstLabelGotoVersionDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValuePathMeasureBuildLogFile] = ConstFBModel.ConstValuePathMeasureBuildLogFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextVcvarsallAlreadyinMemory] = ConstFBModel.ConstLabelTextVcvarsallAlreadyinMemoryDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextRemSeparator] = ConstFBModel.ConstLabelTextRemSeparatorDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelSubKillHeoVsHost] = ConstFBModel.ConstLabelSubKillHeoVsHostDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelSubKillHeo] = ConstFBModel.ConstLabelSubKillHeoDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextSectionHeoModules] = ConstFBModel.ConstLabelTextSectionHeoModulesDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextKillHeoVsHost] = ConstFBModel.ConstLabelTextKillHeoVsHostDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextKillHeo] = ConstFBModel.ConstLabelTextKillHeoDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueHeoVsHostImageName] = ConstFBModel.ConstValueHeoVsHostImageNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueHeoImageName] = ConstFBModel.ConstValueHeoImageNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildLogFile] = ConstFBModel.ConstLiteralMSBuildLogFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueMSBuildLogFile] = ConstFBModel.ConstValueMSBuildLogFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusConfigFilePath] = ConstFBModel.ConstLiteralSGenPlusConfigFilePathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueSGenPlusConfigFilePath] = ConstFBModel.ConstValueSGenPlusConfigFilePathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralHeoForcedOutputDirPath] = ConstFBModel.ConstLiteralHeoForcedOutputDirPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildPlatform] = ConstFBModel.ConstLiteralMSBuildPlatformDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildConfiguration] = ConstFBModel.ConstLiteralMSBuildConfigurationDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueMSBuildConfigurationDsac] = ConstFBModel.ConstValueMSBuildConfigurationDsacDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildCliWin32] = ConstFBModel.ConstLiteralMSBuildCliWin32DefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildCliX86] = ConstFBModel.ConstLiteralMSBuildCliX86DefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWithWin32Targets] = ConstFBModel.ConstLiteralMSBuildWithWin32TargetsDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWithX86Targets] = ConstFBModel.ConstLiteralMSBuildWithX86TargetsDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildsWithTargets] = ConstFBModel.ConstLiteralMSBuildsWithTargetsDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWin32NeedRun] = ConstFBModel.ConstLiteralMSBuildWin32NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildX86NeedRun] = ConstFBModel.ConstLiteralMSBuildX86NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildWin32NeedRun] = ConstFBModel.ConstBaseLabelMacroMSBuildWin32NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLAbelMacroMSBuildX86NeedRun] = ConstFBModel.ConstBaseLAbelMacroMSBuildX86NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildTryLoopCond] = ConstFBModel.ConstLiteralMSBuildTryLoopCondDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildWin32TryLoop] = ConstFBModel.ConstBaseLabelMacroMSBuildWin32TryLoopDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildX86TryLoop] = ConstFBModel.ConstBaseLabelMacroMSBuildX86TryLoopDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroSGenPlusNeedRun] = ConstFBModel.ConstBaseLabelMacroSGenPlusNeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroSGenPlusStatus] = ConstFBModel.ConstBaseLabelMacroSGenPlusStatusDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusNeedRun] = ConstFBModel.ConstLiteralSGenPlusNeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralHeoLanceurPath] = ConstFBModel.ConstLiteralHeoLanceurPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralHeoLanceurBinPath] = ConstFBModel.ConstLiteralHeoLanceurBinPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueHeoLanceurPath] = ConstFBModel.ConstValueHeoLanceurPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralConfigurationPath] = ConstFBModel.ConstLiteralConfigurationPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValuePathBin] = ConstFBModel.ConstValuePathBinDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusTargetBinaryPath] = ConstFBModel.ConstLiteralSGenPlusTargetBinaryPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusCli] = ConstFBModel.ConstLiteralSGenPlusCliDefaultValue;

                return result;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> Properties
        {
            get { return _properties; }
        }

        public string this[string key]
        {
            get { return _properties[key]; }
            set
            {
                string oldValue;
                bool success = _properties.TryGetValue(key, out oldValue);
                if (success && (oldValue == value))
                    return;

                if (ContainsPropertyValue(value))
                    throw new Exception("Value used by an other Internal Variable.");

                _properties[key] = value;
            }
        }

        #endregion Properties

        #region Events

        public event NotifyCollectionChangedEventHandler PropertiesChanged;

        protected void OnPropertiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PropertiesChanged.Notify(sender, e);
        }

        #endregion Events

        #region Functions

        public bool ContainsPropertyName(string propertyName)
        {
            return _properties.ContainsKey(propertyName);
        }

        public bool ContainsPropertyValue(string propertyValue, string exceptForName = null)
        {
            IEnumerable<KeyValuePair<string, string>> properties = _properties;

            if (exceptForName != null)
                properties = properties.Where(kvp => kvp.Key != exceptForName);

            properties = properties.Where(kvp => kvp.Value == propertyValue);

            return properties.Any();
        }

        public void ResetToDefault()
        {
            _properties.Clear();
            foreach (KeyValuePair<string, string> property in DefaultProperties)
            {
                this[property.Key] = property.Value;
            }
        }

        public bool TryGetValue(string propertyName, out string value)
        {
            return _properties.TryGetValue(propertyName, out value);
        }

        #endregion Functions
    }
}