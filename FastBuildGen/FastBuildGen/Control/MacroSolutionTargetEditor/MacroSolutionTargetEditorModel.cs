using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal class MacroSolutionTargetEditorModel : INotifyPropertyChanged
    {
        private readonly IFastBuildParamModel _fastBuildParamModel;
        private IEnumerable<IParamDescriptionHeoModule> _availableModules;
        private IParamDescriptionHeoTarget _target;

        #region ctor

        public MacroSolutionTargetEditorModel(IFastBuildParamModel fastBuildParamModel)
        {
            _fastBuildParamModel = fastBuildParamModel;
            _fastBuildParamModel.HeoModuleParamsChanged += _fastBuildParamModel_HeoModuleParamsChanged;

            UpdateAvailableModules();
        }

        ~MacroSolutionTargetEditorModel()
        {
            Target = null;
        }

        #endregion ctor

        #region Properties

        public IEnumerable<IParamDescriptionHeoModule> AvailableModules
        {
            get { return _availableModules; }
            private set
            {
                _availableModules = value.Execute();
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMacroSolutionTargetEditorModelEvent.ConstAvailableModules));
            }
        }

        public IFastBuildParamModel FastBuildParamModel
        {
            get { return _fastBuildParamModel; }
        }

        public IParamDescriptionHeoTarget Target
        {
            get { return _target; }
            set
            {
                if (Object.Equals(_target, value))
                    return;
                if (_target != null)
                {
                    _target.DependenciesChanged -= _target_DependenciesChanged;
                }
                _target = value;
                if (_target != null)
                {
                    _target.DependenciesChanged += _target_DependenciesChanged;
                }
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMacroSolutionTargetEditorModelEvent.ConstTarget));
                UpdateAvailableModules();
            }
        }

        #endregion Properties

        #region Model events

        private void _fastBuildParamModel_HeoModuleParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAvailableModules();
        }

        private void _target_DependenciesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAvailableModules();
        }

        #endregion Model events

        #region Updates

        private void UpdateAvailableModules()
        {
            IEnumerable<IParamDescriptionHeoModule> availableModules = new IParamDescriptionHeoModule[0];
            if (Target != null)
            {
                HashSet<string> modulesNameUsed = Target.Dependencies
                    .Select(m => m.Name)
                    .ToHashSet();
                availableModules = _fastBuildParamModel.HeoModuleParams
                    .Where(m => false == modulesNameUsed.Contains(m.Name));
            }

            AvailableModules = availableModules;
        }

        #endregion Updates

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion Events
    }
}