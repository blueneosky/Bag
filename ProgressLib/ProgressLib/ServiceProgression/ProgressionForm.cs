using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace ProgressLib.ServiceProgression
{
    /// <summary>
    /// Boite de dialogue permettant d'afficher une progression.
    /// </summary>
    public partial class ProgressionForm : Form
    {

        #region Membres

        private ITacheProgression _tacheProgression;

        private string _texteInitial;

        private volatile Thread _threadHote;
        private object _verrouThreadHote = new object();
        private AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur - réservé pour le designer
        /// </summary>
        private ProgressionForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="tacheProgression"></param>
        public ProgressionForm(ITacheProgression tacheProgression)
        {
            InitializeComponent();

            _texteInitial = this._detailLabel.Text;

            this._tacheProgression = tacheProgression;

            tacheProgression.StatusProgressionModifiee += new EventHandler<StatutProgressionEventArgs>(tacheProgression_StatusProgressionModifiee);
        }

        #endregion

        #region Surcharges

        /// <summary>
        /// Déclenche l'événement <see cref="E:System.Windows.Forms.Form.Shown" />.
        /// </summary>
        /// <param name="e"><see cref="T:System.EventArgs" /> qui contient les données d'événement.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            _autoResetEvent.Set();
        }

        /// <summary>
        /// Déclenche l'événement <see cref="E:System.Windows.Forms.Control.VisibleChanged" />.
        /// </summary>
        /// <param name="e"><see cref="T:System.EventArgs" /> qui contient les données d'événement.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            ArgumentStatutProgression statutProgression = _tacheProgression.StatutProgression;
            MiseAJourTexteProgression(statutProgression.TexteProgression);
            MiseAJourStatusProgression(statutProgression);
        }

        #endregion

        #region Evennements

        void tacheProgression_StatusProgressionModifiee(object sender, StatutProgressionEventArgs e)
        {
            Debug.Assert(sender == _tacheProgression);
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

        #endregion

        #region Gestion démarrage et fin de tâche

        private void TacheDemarree(ArgumentStatutProgression statutProgression)
        {
            lock (_verrouThreadHote)
            {
                if (_threadHote != null)
                {
                    Debug.Fail("La progression est déjà démarrée..");
                    return;
                }
                Action action = delegate
                {
                    this.ShowDialog();
                };
                _threadHote = new Thread(new ThreadStart(action));
                _autoResetEvent.Reset();
                _threadHote.Start();

                // attend l'exécution du thread
                while (!_threadHote.IsAlive) ;
                // attendre l'affichage de la boite de dialogue
                // Note : cette attente est très importante pour ne pas avoir de concurrence avec la fonction TacheTerminee(...)
                _autoResetEvent.WaitOne();
            }

            // Mise à jour des états à l'affichage de la fenêtre
            MiseAJourStatusProgression(statutProgression);
        }

        private void TacheTerminee(ArgumentStatutProgression statutProgression)
        {
            lock (_verrouThreadHote)
            {
                Action action = delegate
                {
                    this.Hide();
                };
                ModifierControleSynchrone(action);
                if (_threadHote == null)
                {
                    Debug.Fail("La progeression est déjà arretée.");
                    return;
                }
                _threadHote.Join(); // on s'assure de la bonne terminaison du thread
                _threadHote = null;
            }
        }

        #endregion

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
                if (String.Equals(_detailLabel.Text, texteProgression))
                    return;
                _detailLabel.Text = texteProgression;
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

        #endregion

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

        /// <summary>
        /// Exécute l'action dans le contexte de la fenêtre.
        /// </summary>
        /// <param name="action"></param>
        public void ModifierControleSynchrone(Action action)
        {
            if (InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

    }
}
