using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.Control.ListEditor
{
    internal class ListEditorModel : INotifyPropertyChanged
    {
        private bool _addEnabled = true;
        private IEnumerable<ListEditorElement> _elements;
        private ListEditorElement _elementSelected;
        private bool _removeEnabled = true;

        public ListEditorModel()
        {
            _elements = new ListEditorElement[0];
        }

        public ListEditorModel(IEnumerable<ListEditorElement> elements)
        {
            _elements = elements.ToArray();  // duplicate
        }

        public bool AddEnabled
        {
            get { return _addEnabled; }
            set
            {
                if (_addEnabled == value)
                    return;
                _addEnabled = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstListEditorModelEvent.ConstAddEnabled));
            }
        }

        public virtual IEnumerable<ListEditorElement> Elements
        {
            get { return _elements; }
            protected set
            {
                _elements = value
                    .OrderBy(e => e.Text)
                    .ToArray();
                OnElementsChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                UpdateElementSelected();
            }
        }

        public ListEditorElement ElementSelected
        {
            get { return _elementSelected; }
            set
            {
                if (Object.ReferenceEquals(_elementSelected, value))
                    return;
                _elementSelected = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstListEditorModelEvent.ConstElementSelected));
            }
        }

        // default
        public bool RemoveEnabled
        {
            get { return _removeEnabled; }
            set
            {
                if (_removeEnabled == value)
                    return;
                _removeEnabled = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstListEditorModelEvent.ConstRemoveEnabled));
            }
        }

        private void UpdateElementSelected()
        {
            ListEditorElement elementSelected = ElementSelected;
            if (elementSelected != null)
            {
                object valueSelected = elementSelected.Value;
                elementSelected = Elements
                    .Where(e => Object.Equals(valueSelected, e.Value))
                    .FirstOrDefault();
                ElementSelected = elementSelected;  // removed from edition
            }
        }

        #region Events

        public event NotifyCollectionChangedEventHandler ElementsChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnElementsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ElementsChanged.Notify(sender, e);
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion Events
    }
}