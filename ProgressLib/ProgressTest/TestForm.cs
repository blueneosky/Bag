using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using ProgressLib.ServiceProgression;

namespace ProgressTest
{
    public partial class TestForm : Form
    {
        private ProgressionForm _progressionForm;
        private TacheProgression _tacheProgression;
        private SelecteurTacheProgression _selecteurTacheProgression;

        private ProgressionManagerTestHelper _helper1;
        private ProgressionManagerTestHelper _helper2;
        private ProgressionManagerTestHelper _helper3;
        private ProgressTestHelper _helper4;

        private const string ConstanteNomProgressionSurFenetre = "Fenetre";
        private const string ConstanteNomProgressionSurBoite = "Boite";

        public TestForm()
        {
            InitializeComponent();

            _helper1 = new ProgressionManagerTestHelper(start1, stop1, reset1, trackBar1, checkBox1, textBox1, progressBar1, label1);
            _helper2 = new ProgressionManagerTestHelper(start2, stop2, reset2, trackBar2, checkBox2, textBox2, progressBar2, label2);
            _helper3 = new ProgressionManagerTestHelper(start3, stop3, reset3, trackBar3, checkBox3, textBox3, progressBar3, label3);
            _helper4 = new ProgressTestHelper(progressBarTotal, labelTotal, true);

            GestionnaireTacheProgression.Instance.TempsPauseMillisecondes = 2000;

            _selecteurTacheProgression = new SelecteurTacheProgression(GestionnaireTacheProgression.Instance.TacheProgression);
            ITacheProgression tacheProgressionFenetre = _selecteurTacheProgression.CreerTacheProgressionSortie(ConstanteNomProgressionSurFenetre);
            ITacheProgression tacheProgressionBoite = _selecteurTacheProgression.CreerTacheProgressionSortie(ConstanteNomProgressionSurBoite);

            _helper4.AttacherProgression(tacheProgressionFenetre);
            new ProgressionForm(tacheProgressionBoite);

            radioButtonAucun.Checked = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            _helper1.Dispose();
            _helper2.Dispose();
            _helper3.Dispose();
            _helper4.Dispose();

            if (_tacheProgression != null)
                _tacheProgression.Dispose();
            if (_progressionForm != null)
                _progressionForm.Dispose();

            base.OnClosed(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_tacheProgression == null)
                _tacheProgression = new TacheProgression();
            if (_progressionForm == null)
                _progressionForm = new ProgressionForm(_tacheProgression);

            int p = 0;
            ArgumentProgression argumentProgression = new ArgumentProgression()
            {
                Minimum = 0,
                Maximum = 5,
            };
            _tacheProgression.Demarrer(argumentProgression);

            long seconde = (long)DateTime.Now.TimeOfDay.TotalSeconds + 1;
            while (true)
            {
                long s = (long)DateTime.Now.TimeOfDay.TotalSeconds;
                if (s > seconde)
                {
                    seconde = s;
                    p++;
                    argumentProgression.Progression = Math.Min(p, argumentProgression.Maximum.Value); ;
                    _tacheProgression.Modifier(argumentProgression);

                    if (p > argumentProgression.Maximum + 1)
                    {
                        _tacheProgression.Terminer();
                        return;
                    }
                }
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            string nom = null;
            if (radioButtonFenetre.Checked)
                nom = ConstanteNomProgressionSurFenetre;
            else if (radioButtonBoite.Checked)
                nom = ConstanteNomProgressionSurBoite;

            _selecteurTacheProgression.NomTacheProgressionSortieCourante = nom;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                using (TacheProgression progression = GestionnaireTacheProgression.Instance.CreerEtInscrireTache())
                {
                }
            }
            using (TacheProgression progression = GestionnaireTacheProgression.Instance.CreerEtInscrireTache())
            {
                ArgumentProgression argumentPogression = new ArgumentProgression()
                {
                    Minimum = 0,
                    Maximum = 0,
                };
                progression.Demarrer(argumentPogression);
            }
        }

    }
}
