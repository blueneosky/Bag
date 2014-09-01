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
            _fbModel.SolutionTargets.CollectionChanged += _fbModel_Targets_CollectionChanged;
            _fbModel.MacroSolutionTargets.CollectionChanged += _fbModel_Targets_CollectionChanged;
            _fbModel.SolutionTargets.ForEach(t => t.PropertyChanged += _fbModel_PropertyChanged);
            _fbModel.MacroSolutionTargets.ForEach(t => t.PropertyChanged += _fbModel_PropertyChanged);
        }

        private void Unsubscribe(FBModel fbModel)
        {
            _fbModel.PropertyChanged -= _fbModel_PropertyChanged;
            _fbModel.SolutionTargets.CollectionChanged -= _fbModel_Targets_CollectionChanged;
            _fbModel.MacroSolutionTargets.CollectionChanged -= _fbModel_Targets_CollectionChanged;
            _fbModel.SolutionTargets.ForEach(t => t.PropertyChanged -= _fbModel_PropertyChanged);
            _fbModel.MacroSolutionTargets.ForEach(t => t.PropertyChanged -= _fbModel_PropertyChanged);
        }

        private void _fbModel_Targets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    e.NewItems.OfType<FBTarget>().ForEach(t => t.PropertyChanged += _fbModel_PropertyChanged);
                    break;

                case NotifyCollectionChangedAction.Move:
                    Debug.Fail("Not managed");
                    break;

                case NotifyCollectionChangedAction.Remove:
                    e.OldItems.OfType<FBTarget>().ForEach(t => t.PropertyChanged -= _fbModel_PropertyChanged);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    Debug.Fail("Not managed");
                    break;

                case NotifyCollectionChangedAction.Reset:
                    e.OldItems.OfType<FBTarget>().ForEach(t => t.PropertyChanged -= _fbModel_PropertyChanged);
                    break;

                default:
                    break;
            }
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