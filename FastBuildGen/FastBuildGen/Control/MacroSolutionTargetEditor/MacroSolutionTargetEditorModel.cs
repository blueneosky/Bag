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
        private readonly ApplicationModel _applicationModel;
        private IEnumerable<FBSolutionTarget> _availableSolutionTargets;
        private FBMacroSolutionTarget _macroSolutionTarget;

        #region ctor

        public MacroSolutionTargetEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;
#warning TODO BETA point - event mangmt
            //_applicationModel.HeoModuleParamsChanged += _fastBuildParamModel_HeoModuleParamsChanged;

            UpdateAvailableSolutionTargets();
        }

        ~MacroSolutionTargetEditorModel()
        {
            MacroSolutionTarget = null;
        }

        #endregion ctor

        #region Properties

        public IEnumerable<FBSolutionTarget> AvailableSolutionTargets
        {
            get { return _availableSolutionTargets; }
            private set
            {
                _availableSolutionTargets = value.Execute();
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMacroSolutionTargetEditorModelEvent.ConstAvailableSolutionTargets));
            }
        }

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        public FBMacroSolutionTarget MacroSolutionTarget
        {
            get { return _macroSolutionTarget; }
            set
            {
                if (Object.Equals(_macroSolutionTarget, value))
                    return;
                if (_macroSolutionTarget != null)
                {
#warning TODO BETA point - event mangmt
                    //_macroSolutionTarget.DependenciesChanged -= _target_DependenciesChanged;
                }
                _macroSolutionTarget = value;
                if (_macroSolutionTarget != null)
                {
#warning TODO BETA point - event mangmt
                    //_macroSolutionTarget.DependenciesChanged += _target_DependenciesChanged;
                }
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMacroSolutionTargetEditorModelEvent.ConstMacroSolutionTarget));
                UpdateAvailableSolutionTargets();
            }
        }

        #endregion Properties

        #region Model events

        private void _fastBuildParamModel_HeoModuleParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAvailableSolutionTargets();
        }

        private void _target_DependenciesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAvailableSolutionTargets();
        }

        #endregion Model events

        #region Updates

        private void UpdateAvailableSolutionTargets()
        {
            IEnumerable<FBSolutionTarget> availableSolutionTargets = new FBSolutionTarget[0];
            if (MacroSolutionTarget != null)
            {
                HashSet<Guid> solutionIdsUsed = MacroSolutionTarget.SolutionTargetIds
                    .ToHashSet();
                availableSolutionTargets = _applicationModel.FBModel.SolutionTargets.Values
                    .Where(m => false == solutionIdsUsed.Contains(m.Id));
            }

            AvailableSolutionTargets = availableSolutionTargets;
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