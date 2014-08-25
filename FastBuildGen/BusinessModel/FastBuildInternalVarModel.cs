using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
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

                result[ConstFastBuildInternalVarModel.ConstLabelGotoHelp] = ConstModel.ConstLabelGotoHelpDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelGotoEnd] = ConstModel.ConstLabelGotoEndDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralVersionNumberName] = ConstModel.ConstLiteralVersionNumberNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralVersionName] = ConstModel.ConstLiteralVersionNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueVersionNumber] = ConstModel.ConstValueVersionNumberDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextFastBuild] = ConstModel.ConstLabelTextFastBuildDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralEnvSystemVcvarsallCheckStatus] = ConstModel.ConstLiteralEnvSystemVcvarsallCheckStatusDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroVcvarsall] = ConstModel.ConstBaseLabelMacroVcvarsallDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroVcvarsallX32X64] = ConstModel.ConstBaseLabelMacroVcvarsallX32X64DefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextVcvarsall] = ConstModel.ConstLabelTextVcvarsallDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueRelativePathVcvarsallBatchFile] = ConstModel.ConstValueRelativePathVcvarsallBatchFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralStartTime] = ConstModel.ConstLiteralStartTimeDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroParametersParsing] = ConstModel.ConstBaseLabelMacroParametersParsingDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelGotoVersion] = ConstModel.ConstLabelGotoVersionDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValuePathMeasureBuildLogFile] = ConstModel.ConstValuePathMeasureBuildLogFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextVcvarsallAlreadyinMemory] = ConstModel.ConstLabelTextVcvarsallAlreadyinMemoryDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextRemSeparator] = ConstModel.ConstLabelTextRemSeparatorDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelSubKillHeoVsHost] = ConstModel.ConstLabelSubKillHeoVsHostDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelSubKillHeo] = ConstModel.ConstLabelSubKillHeoDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextSectionHeoModules] = ConstModel.ConstLabelTextSectionHeoModulesDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextKillHeoVsHost] = ConstModel.ConstLabelTextKillHeoVsHostDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLabelTextKillHeo] = ConstModel.ConstLabelTextKillHeoDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueHeoVsHostImageName] = ConstModel.ConstValueHeoVsHostImageNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueHeoImageName] = ConstModel.ConstValueHeoImageNameDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildLogFile] = ConstModel.ConstLiteralMSBuildLogFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueMSBuildLogFile] = ConstModel.ConstValueMSBuildLogFileDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusConfigFilePath] = ConstModel.ConstLiteralSGenPlusConfigFilePathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueSGenPlusConfigFilePath] = ConstModel.ConstValueSGenPlusConfigFilePathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralHeoForcedOutputDirPath] = ConstModel.ConstLiteralHeoForcedOutputDirPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildPlatform] = ConstModel.ConstLiteralMSBuildPlatformDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildConfiguration] = ConstModel.ConstLiteralMSBuildConfigurationDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueMSBuildConfigurationDsac] = ConstModel.ConstValueMSBuildConfigurationDsacDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildCliWin32] = ConstModel.ConstLiteralMSBuildCliWin32DefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildCliX86] = ConstModel.ConstLiteralMSBuildCliX86DefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWithWin32Targets] = ConstModel.ConstLiteralMSBuildWithWin32TargetsDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWithX86Targets] = ConstModel.ConstLiteralMSBuildWithX86TargetsDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildsWithTargets] = ConstModel.ConstLiteralMSBuildsWithTargetsDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildWin32NeedRun] = ConstModel.ConstLiteralMSBuildWin32NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildX86NeedRun] = ConstModel.ConstLiteralMSBuildX86NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildWin32NeedRun] = ConstModel.ConstBaseLabelMacroMSBuildWin32NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLAbelMacroMSBuildX86NeedRun] = ConstModel.ConstBaseLAbelMacroMSBuildX86NeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralMSBuildTryLoopCond] = ConstModel.ConstLiteralMSBuildTryLoopCondDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildWin32TryLoop] = ConstModel.ConstBaseLabelMacroMSBuildWin32TryLoopDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroMSBuildX86TryLoop] = ConstModel.ConstBaseLabelMacroMSBuildX86TryLoopDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroSGenPlusNeedRun] = ConstModel.ConstBaseLabelMacroSGenPlusNeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstBaseLabelMacroSGenPlusStatus] = ConstModel.ConstBaseLabelMacroSGenPlusStatusDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusNeedRun] = ConstModel.ConstLiteralSGenPlusNeedRunDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralHeoLanceurPath] = ConstModel.ConstLiteralHeoLanceurPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralHeoLanceurBinPath] = ConstModel.ConstLiteralHeoLanceurBinPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValueHeoLanceurPath] = ConstModel.ConstValueHeoLanceurPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralConfigurationPath] = ConstModel.ConstLiteralConfigurationPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstValuePathBin] = ConstModel.ConstValuePathBinDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusTargetBinaryPath] = ConstModel.ConstLiteralSGenPlusTargetBinaryPathDefaultValue;
                result[ConstFastBuildInternalVarModel.ConstLiteralSGenPlusCli] = ConstModel.ConstLiteralSGenPlusCliDefaultValue;

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