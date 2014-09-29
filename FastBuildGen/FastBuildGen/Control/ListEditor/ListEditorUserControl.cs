using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.Common.Control;

namespace FastBuildGen.Control.ListEditor
{
    internal partial class ListEditorUserControl : BaseUserControl
    {
        #region Members

        private ListEditorController _controller;
        private ListEditorElement _elementSelected;
        private ListEditorModel _model;

        #endregion Members

        #region ctor

        public ListEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(ListEditorModel model, ListEditorController controller)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;
            _model.ElementsChanged += _model_ElementsChanged;

            UpdateElementSelected();
            RefreshModel();
        }

        #endregion ctor

        #region Properties

        public string AddButtonText
        {
            get { return _addModuleButton.Text; }
            set { _addModuleButton.Text = value; }
        }

        public string EditorGroupBoxText
        {
            get { return _editorGroupBox.Text; }
            set { _editorGroupBox.Text = value; }
        }

        public Panel EditorPanel
        {
            get { return _editorPanel; }
        }

        public string ListColumnName
        {
            get { return _columnHeader.Text; }
            set { _columnHeader.Text = value; }
        }

        public string ListGroupBoxText
        {
            get { return _listGroupBox.Text; }
            set { _listGroupBox.Text = value; }
        }

        private ListEditorElement ElementSelected
        {
            get { return _elementSelected; }
            set
            {
                if (Object.Equals(_elementSelected, value))
                    return;
                if (_elementSelected != null)
                {
                    _elementSelected.PropertyChanged -= _elementSelected_PropertyChanged;
                }
                _elementSelected = value;
                if (_elementSelected != null)
                {
                    _elementSelected.PropertyChanged += _elementSelected_PropertyChanged;
                }
            }
        }

        #endregion Properties

        #region Overrides

        protected override void PartialDispose(bool disposing)
        {
            if (disposing && (_model != null))
            {
                _model.PropertyChanged -= _model_PropertyChanged;
                _model.ElementsChanged -= _model_ElementsChanged;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _elementSelected_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstListEditorElementEvent.ConstText:
                    RefreshElements();
                    RefreshElementSelected();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void _model_ElementsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // all cases
            RefreshElements();
            RefreshElementSelected();
        }

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstListEditorModelEvent.ConstElementSelected:
                    UpdateElementSelected();
                    break;

                case ConstListEditorModelEvent.ConstAddEnabled:
                    RefreshAddEnable();
                    break;

                case ConstListEditorModelEvent.ConstRemoveEnabled:
                    RefreshRemoveEnable();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        #endregion Model events

        #region Model Update

        private void UpdateElementSelected()
        {
            ElementSelected = _model.ElementSelected;

            RefreshElementSelected();
        }

        #endregion Model Update

        #region UI Update

        private void RefreshAddEnable()
        {
            _addModuleButton.Enabled = _model.AddEnabled;
        }

        private void RefreshElements()
        {
            IEnumerable<ListEditorElement> elements = _model.Elements;

            BeginUpdate();
            _listViewEx.BeginUpdate();

            _listViewEx.Items.Clear();
            ListViewItemElement[] items = (elements ?? new ListEditorElement[0])
                .Select(e => new ListViewItemElement(e))
                .ToArray();
            _listViewEx.Items.AddRange(items);

            _listViewEx.EndUpdate();

            bool isEnabled = elements != null;
            _listPanel.Enabled = isEnabled;
            _buttonsPanel.Enabled = isEnabled;

            EndUpdate();
        }

        private void RefreshElementSelected()
        {
            BeginUpdate();
            _listViewEx.BeginUpdate();

            ListEditorElement element = _model.ElementSelected;
            ListViewItemElement item = _listViewEx.Items
                .OfType<ListViewItemElement>()
                .Where(e => Object.Equals(element, e.Value))
                .FirstOrDefault();
            _listViewEx.SelectedItems.Clear();
            if (item != null)
                item.Selected = true;

            IEnumerable<int> indices = _listViewEx.SelectedIndices
                .OfType<int>();
            if (indices.Any())
                _listViewEx.EnsureVisible(indices.First());

            _listViewEx.EndUpdate();

            EndUpdate();
        }

        private void RefreshModel()
        {
            RefreshAddEnable();
            RefreshRemoveEnable();
            RefreshElements();
            RefreshElementSelected();
        }

        private void RefreshRemoveEnable()
        {
            // nothing
        }

        #endregion UI Update

        #region User inputs

        private void _addModuleButton_Click(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            _controller.NewElement();
        }

        private void _listView_GlobalSelectionChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            ListViewItemElement item = _listViewEx.SelectedItems
                .OfType<ListViewItemElement>()
                .FirstOrDefault();
            ListEditorElement element = (item != null) ? item.Value : null;
            _controller.SelectElement(element);
        }

        private void _listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsUpdating)
                return;

            if (e.KeyCode == Keys.Delete)
            {
                _controller.RemoveSelectedElement();
                e.Handled = true;
            }
        }

        #endregion User inputs
    }
}