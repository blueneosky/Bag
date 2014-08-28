using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.UI;
using ImputationH31per.Util;

namespace FastBuildGen.Common.Forms
{
    internal class BaseForm : Form
    {
        private const int WS_EX_COMPOSITED = 0x02000000;

        private System.ComponentModel.IContainer components = null;

        protected BaseForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            _shortcutsManager = new ShortcutsManager();
        }

        protected override void Dispose(bool disposing)
        {
            PartialDispose(disposing);
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadGlobalDoubleBuffered();
        }

        protected virtual void PartialDispose(bool disposing)
        {
        }

        #region Update management

        private int _updateCounter;

        protected bool IsUpdating
        {
            get { return _updateCounter > 0; }
        }

        protected void BeginUpdate()
        {
            _updateCounter++;
        }

        protected void EndUpdate()
        {
            _updateCounter--;
        }

        #endregion Update management

        #region ShortcutsManager

        private ShortcutsManager _shortcutsManager;

        protected ShortcutsManager ShortcutsManager
        {
            get { return _shortcutsManager; }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool traite = _shortcutsManager.ProcessKey(keyData);
            if (traite)
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion ShortcutsManager

        #region Validation / Error provider

        protected void ValidationWithErrorProvider(Action action, System.Windows.Forms.Control control, ErrorProvider errorProvider, CancelEventArgs e, ErrorIconAlignment? errorIconAlignment = null)
        {
            try
            {
                action();
                if ((errorProvider != null) && (false == String.IsNullOrEmpty(errorProvider.GetError(control))))
                    errorProvider.SetError(control, null);   // supprimer l'ancienne erreur
            }
            catch (Exception exception)
            {
                if (errorIconAlignment.HasValue)
                    errorProvider.SetIconAlignment(control, errorIconAlignment.Value);
                if ((errorProvider != null))
                    errorProvider.SetError(control, exception.Message);
                if (e != null)
                    e.Cancel = true;
            }
        }

        #endregion Validation / Error provider

        #region DoubleBuffered

        public virtual bool DoubleBufferedEx
        {
            get { return DoubleBuffered && this.GetStyle(ControlStyles.UserPaint); }
            set
            {
                base.DoubleBuffered = value;
                this.SetStyle(ControlStyles.UserPaint, value);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (DoubleBufferedEx)
                    cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }

        protected override bool DoubleBuffered
        {
            get { return base.DoubleBuffered; }
            set
            {
                if (value)
                {
                    base.DoubleBuffered = true;
                }
                else
                {
                    DoubleBufferedEx = false;
                }
            }
        }

        private void LoadGlobalDoubleBuffered()
        {
            DoubleBufferedEx = UIDoubleBufferedModeManager.GlobalDoubleBufferedEx;
            DoubleBuffered = UIDoubleBufferedModeManager.GlobalDoubleBuffered;
        }

        #endregion DoubleBuffered

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // BaseForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 291);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}