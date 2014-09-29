using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FastBuildGen.Common.Control;

namespace FastBuildGen.Control.Faq
{
    internal partial class FaqUserControl : BaseUserControl
    {
        #region Members

        private FaqModel _model;
        private FaqController _controller;

        #endregion Members

        #region ctor

        public FaqUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(FaqModel model, FaqController controller)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;

            RefreshModel();
        }

        #endregion ctor

        #region Overrides

        protected override void PartialDispose(bool disposing)
        {
            if (disposing && (_model != null))
            {
                _model.PropertyChanged -= _model_PropertyChanged;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Action action = () => { _model_PropertyChanged(sender, e); };
                this.BeginInvoke(action);
                return;
            }

            switch (e.PropertyName)
            {
                case ConstFaqModelEvent.ConstSourcePage:
                    RefreshSourcePage();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        #endregion Model events

        #region UI Update

        private void RefreshModel()
        {
            RefreshSourcePage();
        }

        private void RefreshSourcePage()
        {
            string source = _model.SourcePage;
            if (source == null)
            {
                _faqWebBrowser.DocumentText = "Please wait, loading in progress...";
            }
            else
            {
                _faqWebBrowser.DocumentText = source;
            }
        }

        #endregion UI Update
    }
}