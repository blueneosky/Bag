using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

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

            _applicationModel.PropertyChanged += _applicationModel_PropertyChanged;

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

        private FBModel _fbModel;

        public FBModel FBModel
        {
            get { return _fbModel; }
            set
            {
                if (_fbModel != null)
                {
                    _fbModel.SolutionTargets.CollectionChanged -= _fbModel_SolutionTargets_CollectionChanged;
                }
                _fbModel = value;
                if (_fbModel != null)
                {
                    _fbModel.SolutionTargets.CollectionChanged += _fbModel_SolutionTargets_CollectionChanged;
                }
            }
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
                    _macroSolutionTarget.SolutionTargetIds.CollectionChanged -= _macroSolutionTarget_SolutionTargetIds_CollectionChanged;
                }
                _macroSolutionTarget = value;
                if (_macroSolutionTarget != null)
                {
                    _macroSolutionTarget.SolutionTargetIds.CollectionChanged += _macroSolutionTarget_SolutionTargetIds_CollectionChanged;
                }
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstMacroSolutionTargetEditorModelEvent.ConstMacroSolutionTarget));
                UpdateAvailableSolutionTargets();
            }
        }

        #endregion Properties

        #region Model events

        private void _fbModel_SolutionTargets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAvailableSolutionTargets();
        }

        private void _macroSolutionTarget_SolutionTargetIds_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAvailableSolutionTargets();
        }

        private void _applicationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstApplicationModelFBModel:
                    UpdateFBModel();
                    break;

                default:
                    // ignored
                    break;
            }
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
                availableSolutionTargets = _applicationModel.FBModel.SolutionTargets
                    .Where(m => false == solutionIdsUsed.Contains(m.Id));
            }

            AvailableSolutionTargets = availableSolutionTargets;
        }

        private void UpdateFBModel()
        {
            FBModel = _applicationModel.FBModel;
            UpdateAvailableSolutionTargets();
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