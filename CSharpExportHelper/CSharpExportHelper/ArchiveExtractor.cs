using Ionic.Zip;
using ProgressLib.ServiceProgression;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CSharpExportHelper
{
    public class ArchiveExtractor
    {

        private static string GetFirstDirectory(string chemin)
        {
            string directory = Path.GetDirectoryName(chemin);
            if (string.IsNullOrEmpty(directory))
                return chemin;

            return GetFirstDirectory(directory);
        }

        public void DemarrerTraitementArchive(string cheminArchive)
        {
            // controler si archive zip
            bool estValide = EstArchiveValide(cheminArchive);
            if (false == estValide)
                throw new Exception("Archive invalide - extraction interronpue.");

            // extraction
            ExtraireArchive(cheminArchive);
        }

        private bool EstArchiveValide(string cheminArchive)
        {
            bool resultat = ZipFile.CheckZip(cheminArchive);

            return resultat;
        }

        private void ExtraireArchive(string cheminArchive)
        {
            using (ZipFile zipFile = new ZipFile(cheminArchive))
            {
                IEnumerable<string> repertoireRelatifDestinations = zipFile
                    .Select(e => GetFirstDirectory(e.FileName).ToLowerInvariant())
                    .Distinct()
                    .ToArray();
                if (repertoireRelatifDestinations.Count() != 1)
                    throw new Exception("Archive non conforme à C# ExportHelper - extraction interronpue.");

                string repertoireRelatifDestination = repertoireRelatifDestinations.First();
                string repertoireRacine = Path.GetDirectoryName(cheminArchive);
                string repertoireDestination = Path.Combine(repertoireRacine, repertoireRelatifDestination);
                if (File.Exists(repertoireDestination))
                    throw new Exception("Un fichier existe à la place du repertoire de destination  : " + repertoireDestination);

                if (Directory.Exists(repertoireDestination))
                {
                    DialogResult dialogResult = MessageBox.Show("Voulez-vous remplacer le repertoire de destination par celui de l'archive ?", "Remplacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.No)
                        return;

                    try
                    {
                        Directory.Delete(repertoireDestination, true);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("La suppression du répertoire existant à échoué (cause : " + e.Message + ")" + Environment.NewLine + "extraction interrompue.");
                    }
                }

                TacheProgression tacheProgression = GestionnaireTacheProgression.Instance.CreerEtInscrireTache();
                zipFile.ExtractProgress += (sender, e) => zipFile_ExtractProgress(sender, e, tacheProgression);
                zipFile.ExtractAll(repertoireRacine);
            }
        }

        private void MajorStep()
        {
            // if dest not exist
            //      fast extract

            // ask for replace
            // backup existing in tmp archive (full)
            // extract in tmp dir
            // try
            //      copy/update tmp to dest
            // 

        }

        //private void FastExtract()
        //{
        //    TacheProgression tacheProgression = GestionnaireTacheProgression.Instance.CreerEtInscrireTache();
        //    zipFile.ExtractProgress += (sender, e) => zipFile_ExtractProgress(sender, e, tacheProgression);
        //    zipFile.ExtractAll(repertoireRacine);
        //}

        private void zipFile_ExtractProgress(object sender, ExtractProgressEventArgs e, TacheProgression tacheProgression)
        {
            ArgumentProgression argumentProgression = tacheProgression.StatutProgression.ObtenirProgression();
            if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
            {
                var total = e.EntriesTotal;
                if (total == 0)
                    total = 1;
                argumentProgression.Maximum = total;
                argumentProgression.Progression = e.EntriesExtracted / total;
                if (e.CurrentEntry != null)
                {
                    argumentProgression.TexteProgression = "Extraction (" + e.CurrentEntry.FileName + ")";
                }
                else
                {
                    argumentProgression.TexteProgression = "Extraction ...)";
                }
            }
        }

        //private void WalkerUpdate

    }
}