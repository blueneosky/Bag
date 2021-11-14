using Ionic.Zip;
using ProgressLib.ServiceProgression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSharpExportHelper
{
    public class ArchiveCreator
    {
        public void DemarrerTraitementRepertoire(string repertoire, bool full = false, string cheminArchive = null)
        {
            string racine = Path.GetDirectoryName(repertoire);
            string nomArchive;
            if (cheminArchive == null)
            {
                nomArchive = Path.GetFileName(repertoire) + ".zip";
                cheminArchive = Path.Combine(racine, nomArchive);
            }
            else
            {
                nomArchive = Path.GetFileName(cheminArchive);
            }

            if (File.Exists(cheminArchive))
            {
                DialogResult dialogResult = MessageBox.Show("L'archive " + nomArchive + " existe déjà, remplacer ?", "Remplacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                    return;
            }

            using (TacheProgression tacheProgression = GestionnaireTacheProgression.Instance.CreerEtInscrireTache())
            {
                ArgumentProgression argumentProgression = tacheProgression.StatutProgression.ObtenirProgression();
                argumentProgression.TexteProgression = "Préparation en cours...";
                argumentProgression.Maximum = null; // indétrminé
                tacheProgression.Demarrer(argumentProgression);

                // déterminer les fichiers à copier
                IEnumerable<string> chemins = SmartFileQualifier.ObtenirFichierDuRepertoire(repertoire);

                if (!full)
                {
                    // exclure les fichiers
                    chemins = SmartFileQualifier.ExclureCheminsNonValide(chemins);
                }

                // création de l'archive
                CreerArchiveZip(repertoire, chemins, cheminArchive, tacheProgression);
            }
        }

        private void CreerArchiveZip(string repertoire, IEnumerable<string> chemins, string cheminArchive, TacheProgression tacheProgression)
        {
            chemins = chemins
                .ToArray();
            string racine = Path.GetDirectoryName(repertoire);
            int longueur = racine.Length + 1;

            ArgumentProgression argumentProgression = tacheProgression.StatutProgression.ObtenirProgression();
            argumentProgression.Maximum = chemins.Count();
            argumentProgression.Progression = 0;
            tacheProgression.Modifier(argumentProgression);

            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                foreach (string chemin in chemins)
                {
                    string cheminRelatif = chemin.Substring(longueur);
                    string cheminBaseRelatif = (Path.GetDirectoryName(chemin)).Substring(longueur);

                    argumentProgression.TexteProgression = "Ajout de " + cheminRelatif + " ...";
                    tacheProgression.Modifier(argumentProgression);

                    zip.AddItem(chemin, cheminBaseRelatif);

                    argumentProgression.Progression++;
                }

                argumentProgression.Maximum = null;
                argumentProgression.TexteProgression = "Enregistrement de l'arvchive ...";
                tacheProgression.Modifier(argumentProgression);

                zip.SaveProgress += (sender, e) => zip_SaveProgress(sender, e, tacheProgression);
                zip.Save(cheminArchive);
            }
        }

  
        private void zip_SaveProgress(object sender, SaveProgressEventArgs e, TacheProgression tacheProgression)
        {
            if (e.EventType == ZipProgressEventType.Saving_AfterWriteEntry)
            {
                ArgumentProgression argumentProgression = tacheProgression.StatutProgression.ObtenirProgression();
                argumentProgression.Maximum = e.EntriesTotal;
                argumentProgression.Progression = e.EntriesSaved;
                if (e.CurrentEntry != null)
                {
                    argumentProgression.TexteProgression = "Enregistrement (" + e.CurrentEntry.FileName + ")";
                }
                else
                {
                    argumentProgression.TexteProgression = "Enregistrement ...)";
                }
                tacheProgression.Modifier(argumentProgression);
            }
        }

    }
}