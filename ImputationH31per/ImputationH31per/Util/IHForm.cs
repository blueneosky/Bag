using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Properties;

namespace ImputationH31per.Util
{
    public class IHForm : Form
    {
        private const int ConstanteValeurMaximumCompteurMiseAJour = 20; // arbitraire

        private readonly IIHFormControleur _controleur;
        private readonly IIHFormModele _modele;
        private int _compteurMiseAJour;

        /// <summary>
        /// ctor - designer
        /// </summary>
        protected IHForm()
        {
            InitializeComponent();

            if (false == DesignMode)
            {
                this.Icon = Resources.Main;
            }
        }

        protected IHForm(IIHFormModele modele, IIHFormControleur controleur)
            : this()
        {
            _modele = modele;
            _controleur = controleur;
            _compteurMiseAJour = 0;
        }

        protected bool EstMiseAJourEnCours
        {
            get { return _compteurMiseAJour > 0; }
        }

        protected void CommencerMiseAJour()
        {
            _compteurMiseAJour++;
            Debug.Assert(_compteurMiseAJour <= ConstanteValeurMaximumCompteurMiseAJour);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (false == e.Cancel)
            {
                try
                {
                    MemoriserPosition(this);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Exception non géré : " + exception.Message);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                RestituerPosition();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception non géré : " + exception.Message);
            }
        }

        protected void TerminerMiseAJour()
        {
            _compteurMiseAJour--;
            Debug.Assert(_compteurMiseAJour >= 0);
        }

        protected void ValidationAvecErrorProvider(Action action, Control control, ErrorProvider errorProvider, CancelEventArgs e)
        {
            try
            {
                action();
                if ((errorProvider != null) && (false == String.IsNullOrEmpty(errorProvider.GetError(control))))
                    errorProvider.SetError(control, null);   // supprimer l'ancienne erreur
            }
            catch (IHException exception)
            {
                if ((errorProvider != null))
                    errorProvider.SetError(control, exception.Message);
                if (e != null)
                    e.Cancel = true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // IHForm
            //
            this.ClientSize = new System.Drawing.Size(397, 250);
            this.Name = "IHForm";
            this.ResumeLayout(false);
        }

        #region Gestion de mémorisation et restitution de positionnement de fenêtre

        private void MemoriserPosition(IHForm ihForm)
        {
            if ((_modele == null) || (_controleur == null))
                return;

            bool estAgrandi = ihForm.WindowState == FormWindowState.Maximized;
            Point localisation = ihForm.Location;
            Size taille = ihForm.Size;

            _controleur.MemoriserPreference(estAgrandi, localisation, taille);
        }

        private void RestituerPosition()
        {
            if ((_modele == null) || (false == _modele.EstDefini))
                return;

            this.Location = _modele.Localisation;
            this.Size = _modele.Taille;

            this.WindowState = _modele.EstAgrandi ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        #endregion Gestion de mémorisation et restitution de positionnement de fenêtre
    }
}