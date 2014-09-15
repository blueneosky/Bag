using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class ApplicationModel : INotifyPropertyChanged
    {
        private FBModel _fbModel;
        private bool _dataChanged;

        public ApplicationModel()
        {
            _fbModel = null;
        }

        public FBModel FBModel
        {
            get { return _fbModel; }
            set
            {
                if (_fbModel != null)
                {
                    Unsubscribe(_fbModel);
                }
                _fbModel = value;
                if (_fbModel != null)
                {
                    Subscribe(_fbModel);
                }
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstApplicationModelFBModel));
            }
        }

        public bool DataChanged
        {
            get { return _dataChanged; }
            set
            {
                if (_dataChanged == value) return;
                _dataChanged = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstApplicationModelDataChanged));
            }
        }

        #region FBModel events subcribtion

        private void Subscribe(FBModel fbModel)
        {
            _fbModel.PropertyChanged += _fbModel_PropertyChanged;
            _fbModel.SolutionTargets.CollectionChanged += _fbModel_SolutionTargets_CollectionChanged;
            _fbModel.MacroSolutionTargets.CollectionChanged += _fbModel_MacroSolutionTargets_CollectionChanged;
            _fbModel.SolutionTargets.ForEach(Subscribe);
            _fbModel.MacroSolutionTargets.ForEach(Subscribe);
        }

        private void Unsubscribe(FBModel fbModel)
        {
            _fbModel.PropertyChanged -= _fbModel_PropertyChanged;
            _fbModel.SolutionTargets.CollectionChanged -= _fbModel_SolutionTargets_CollectionChanged;
            _fbModel.MacroSolutionTargets.CollectionChanged -= _fbModel_MacroSolutionTargets_CollectionChanged;
            _fbModel.SolutionTargets.ForEach(Unsubscribe);
            _fbModel.MacroSolutionTargets.ForEach(Unsubscribe);
        }

        private void Subscribe(FBTarget fbTarget)
        {
            fbTarget.PropertyChanged += _fbModel_PropertyChanged;
        }

        private void Unsubscribe(FBTarget fbTarget)
        {
            fbTarget.PropertyChanged -= _fbModel_PropertyChanged;
        }

        private void Subscribe(FBSolutionTarget fbSolutionTarget)
        {
            Subscribe((FBTarget)fbSolutionTarget);
        }

        private void Unsubscribe(FBSolutionTarget fbSolutionTarget)
        {
            Unsubscribe((FBTarget)fbSolutionTarget);
        }

        private void Subscribe(FBMacroSolutionTarget fbMacroSolutionTarget)
        {
            Subscribe((FBTarget)fbMacroSolutionTarget);
            fbMacroSolutionTarget.SolutionTargetIds.CollectionChanged += SolutionTargetIds_CollectionChanged;
        }

        private void SolutionTargetIds_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataChanged = true;
        }

        private void Unsubscribe(FBMacroSolutionTarget fbMacroSolutionTarget)
        {
            Unsubscribe((FBTarget)fbMacroSolutionTarget);
            fbMacroSolutionTarget.SolutionTargetIds.CollectionChanged -= SolutionTargetIds_CollectionChanged;
        }

        private void _fbModel_SolutionTargets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.OfType<FBSolutionTarget>().ForEach(Subscribe);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.OfType<FBSolutionTarget>().ForEach(Unsubscribe);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    e.OldItems.OfType<FBSolutionTarget>().ForEach(Unsubscribe);
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                default:
                    Debug.Fail("Unspecified case");
                    break;
            }
            DataChanged = true;
        }

        private void _fbModel_MacroSolutionTargets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.OfType<FBMacroSolutionTarget>().ForEach(Subscribe);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.OfType<FBMacroSolutionTarget>().ForEach(Unsubscribe);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    e.OldItems.OfType<FBMacroSolutionTarget>().ForEach(Unsubscribe);
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                default:
                    Debug.Fail("Unspecified case");
                    break;
            }
            DataChanged = true;
        }

        private void _fbModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DataChanged = true;
        }

        #endregion FBModel events subcribtion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged
    }
}