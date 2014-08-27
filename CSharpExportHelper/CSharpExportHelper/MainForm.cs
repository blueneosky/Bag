using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgressLib.ServiceProgression;
using CSharpExportHelper.Properties;

namespace CSharpExportHelper
{
    public partial class MainForm : Form
    {
        private readonly Modele _modele;

        private string _texteInitial;

        /// <summary>
        /// pour le designer
        /// </summary>
        private MainForm()
        {
            InitializeComponent();
        }

        public MainForm(Modele modele)
        {
            InitializeComponent();

            Icon = Resources.Main;

            _modele = modele;

            _texteInitial = _label.Text;

            ITacheProgression tacheProgression = GestionnaireTacheProgression.Instance.TacheProgression;
            tacheProgression.StatusProgressionModifiee += new EventHandler<StatutProgressionEventArgs>(tacheProgression_StatusProgressionModifiee);
        }

        private void tacheProgression_StatusProgressionModifiee(object sender, StatutProgressionEventArgs e)
        {
            switch (e.StatutTacheProgression)
            {
                case EnumStatutTacheProgression.Demarrage:
                    TacheDemarree(e.StatutProgression);
                    MiseAJourTexteProgression(e.StatutProgression.TexteProgression);
                    break;

                case EnumStatutTacheProgression.EnCours:
                    MiseAJourStatusProgression(e.StatutProgression);
                    MiseAJourTexteProgression(e.StatutProgression.TexteProgression);
                    break;

                case EnumStatutTacheProgression.Arret:
                    MiseAJourTexteProgression(null);
                    TacheTerminee(e.StatutProgression);
                    break;

                case EnumStatutTacheProgression.Aucun:
                default:
                    Debug.Fail("Cas non gérés");
                    break;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnShown(e);

            _backgroundWorker.RunWorkerAsync();
            //_timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            _backgroundWorker.RunWorkerAsync();
        }
        private void _backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                _modele.DemarrerTraitement();
            }
            catch (Exception exception)
            {
                MessageBox.Show("erreur survenu :" + Environment.NewLine + exception.Message);
            }
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        #region Gestion démarrage et fin de tâche

        private void TacheDemarree(ArgumentStatutProgression statutProgression)
        {
            // Mise à jour des états à l'affichage de la fenêtre
            MiseAJourStatusProgression(statutProgression);
        }

        private void TacheTerminee(ArgumentStatutProgression statutProgression)
        {
            // rien
        }

        #endregion Gestion démarrage et fin de tâche

        #region Mise à jour depuis tâche de progrression

        private void MiseAJourTexteProgression(string texteProgression)
        {
            if (String.IsNullOrEmpty(texteProgression))
            {
                texteProgression = _texteInitial;
            }

            Action action = delegate
            {
                if (!this.Visible)
                    return;
                if (String.Equals(_label.Text, texteProgression))
                    return;
                _label.Text = texteProgression;
            };
            ModifierControle(action);
        }

        private void MiseAJourStatusProgression(ArgumentStatutProgression statutProgression)
        {
            Action action = delegate
            {
                if (!this.Visible)
                    return;

                ProgressBarStyle style = ProgressBarStyle.Marquee;
                EnumEtatProgression etat = statutProgression.EtatProgression;
                if (etat == EnumEtatProgression.ProgressionEnCours || etat == EnumEtatProgression.Terminee)
                    style = ProgressBarStyle.Blocks;
                if (_progressBar.Style != style)
                    _progressBar.Style = style;

                if (etat != EnumEtatProgression.ProgressionIndeterminee)
                {
                    int minimum = statutProgression.Minimum;
                    int progression = statutProgression.Progression;
                    int maximum = statutProgression.Maximum ?? Math.Max(minimum, progression);

                    _progressBar.Minimum = minimum;
                    _progressBar.Maximum = maximum;
                    _progressBar.Value = progression;
                }
            };
            ModifierControle(action);
        }

        #endregion Mise à jour depuis tâche de progrression

        /// <summary>
        /// Exécute l'action dans le contexte de la fenêtre.
        /// </summary>
        /// <param name="action"></param>
        public void ModifierControle(Action action)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }


    }
}