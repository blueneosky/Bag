using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.Common.UI;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Control.InternalVarEditor
{
    internal class InternalVarEditorModel : UIModelBase, INotifyPropertyChanged
    {
        #region Members

        private readonly IFastBuildInternalVarModel _fastBuildInternalVarModel;
        private readonly IUndoRedoManager _undoRedoManager;
        private string _keyword;

        #endregion Members

        #region ctor

        public InternalVarEditorModel(IFastBuildInternalVarModel fastBuildInternalVarModel, IUndoRedoManager undoRedoManager)
            : base(undoRedoManager)
        {
            _fastBuildInternalVarModel = fastBuildInternalVarModel;
            _undoRedoManager = undoRedoManager;

            _fastBuildInternalVarModel.PropertiesChanged += _fastBuildInternalVarModel_PropertiesChanged;
        }

        ~InternalVarEditorModel()
        {
            _fastBuildInternalVarModel.PropertiesChanged -= _fastBuildInternalVarModel_PropertiesChanged;
        }

        #endregion ctor

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion Events

        #region Properties

        public IFastBuildInternalVarModel FastBuildInternalVarModel
        {
            get { return _fastBuildInternalVarModel; }
        }

        public string Keyword
        {
            get { return _keyword; }
            set
            {
                if (_keyword == value)
                    return;
                _keyword = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstInternalVarEditorModelEvent.ConstKeyword));
            }
        }

        public string Value
        {
            get
            {
                string keyword = Keyword;
                string value = null;
                if (keyword != null)
                    _fastBuildInternalVarModel.TryGetValue(keyword, out value);

                return value;
            }
        }

        #endregion Properties

        #region Functions

        private void _fastBuildInternalVarModel_PropertiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            string keyword = Keyword;

            KeyValuePair<string, string>? oldItem = (e.OldItems ?? new KeyValuePair<string, string>[0])
                .OfType<KeyValuePair<string, string>>()
                .Where(kvp => kvp.Key == keyword)
                .Select(kvp => (KeyValuePair<string, string>?)kvp)
                .FirstOrDefault();
            KeyValuePair<string, string>? newItem = (e.NewItems ?? new KeyValuePair<string, string>[0])
                .OfType<KeyValuePair<string, string>>()
                .Where(kvp => kvp.Key == keyword)
                .Select(kvp => (KeyValuePair<string, string>?)kvp)
                .FirstOrDefault();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (newItem.HasValue)
                        UpdateValue();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (oldItem.HasValue)
                        UpdateKeyword(null);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    if (oldItem.HasValue || newItem.HasValue)
                        UpdateKeyword(newItem.HasValue ? newItem.Value.Key : null);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    UpdateKeyword(null);
                    break;

                case NotifyCollectionChangedAction.Move:
                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void UpdateKeyword(string keyword)
        {
            Keyword = keyword;
            UpdateValue();
        }

        private void UpdateValue()
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(ConstInternalVarEditorModelEvent.ConstValue));
        }

        #endregion Functions
    }
}