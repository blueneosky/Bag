using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace BuildLogAnalyse
{
    public partial class Form1 : Form
    {

        private const string ConstanteCheminFichierDonee = @"D:\_workspaces\HEO\V1\Developpement\ProduitCommercial\measure_build.log.txt";
        private const string ConstanteCheminFichierDoneeFallback = @"C:\_workspaces\HEO\V1\Developpement\ProduitCommercial\measure_build.log.txt";
        private static readonly string[] ConstanteCheminFichierDonnees = new[] { ConstanteCheminFichierDonee, ConstanteCheminFichierDoneeFallback };

        private IEnumerable<DataLogSet> _logs;
        private DataLogSummary _dataLogSummary;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool succes = ChargerFichierDonnees();
            if (false == succes)
                Close();

            RafraichirVue();

        }

        private bool ChargerFichierDonnees()
        {
            List<DataLogSet> sets = new List<DataLogSet> { };

            StreamReader reader = null;
            foreach (string cheminFichier in ConstanteCheminFichierDonnees)
            {
                if (false == File.Exists(cheminFichier)) continue;

                try
                {
                    reader = new StreamReader(cheminFichier);
                    break;
                }
                catch (Exception) { }
            }
            if (reader == null)
            {
                MessageBox.Show("Le fichier de log de fastbuild n'a pas été trouvé");
                return false;
            }

            string ligne;
            int indice = 0;
            DateTime derniereDate = DateTime.MinValue;
            while ((ligne = reader.ReadLine()) != null)
            {
                string[] items = ligne.Split(new char[] { '\t', ',', '.' });
                Debug.Assert(items.Length == 5);
                string texteJour = items[0].Trim();
                string texteHeureDebut = items[1].Trim();
                string texteHeureFin = items[3].Trim();

                DateTime debut = DateTime.Parse(texteJour + " " + texteHeureDebut, CultureInfo.CurrentCulture.DateTimeFormat);
                DateTime fin = DateTime.Parse(texteJour + " " + texteHeureFin, CultureInfo.CurrentCulture.DateTimeFormat);

                if (derniereDate != debut.Date)
                {
                    indice = 1;
                    derniereDate = debut.Date;
                }
                else
                {
                    indice++;
                }

                DataLogSet set = new DataLogSet()
                {
                    Start = debut,
                    Stop = fin,
                    IndiceSurJour = indice,
                };

                sets.Add(set);
            }

            reader.Close();

            _logs = sets;
            _dataLogSummary = new DataLogSummary(sets);

            return true;
        }

        private void RafraichirVue()
        {
            IEnumerable<ListViewItem> items = _logs
                .Select(data =>
                {
                    ListViewItem item = new ListViewItem(data.Stop.Date.ToString());
                    item.SubItems.Add(data.Start.TimeOfDay.ToString());
                    item.SubItems.Add(data.Stop.TimeOfDay.ToString());
                    item.SubItems.Add(data.Ellapsed.ToString());
                    item.SubItems.Add("" + data.IndiceSurJour);
                    return item;
                });

            listView.Items.Clear();
            listView.Items.AddRange(items.ToArray());

            propertyGrid.SelectedObject = _dataLogSummary;
        }
    }
}
