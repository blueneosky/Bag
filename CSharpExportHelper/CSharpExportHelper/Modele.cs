using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using ProgressLib.ServiceProgression;

namespace CSharpExportHelper
{
    public class Modele
    {
        private readonly IEnumerable<string> _sources;

        public Modele(IEnumerable<string> sources)
        {
            this._sources = sources
                .Select(s => Path.GetFullPath(s))
                .ToArray();
        }

        public void DemarrerTraitement()
        {
            foreach (string source in _sources)
            {
                try
                {
                    DemarrerTraitement(source);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur durant traitement de '" + source + "' : " + Environment.NewLine + e.Message);
                }
            }
        }

        public void DemarrerTraitement(string source)
        {
            if (Directory.Exists(source))
            {
                DemarrerTraitementRepertoire(source);
            }
            else if (File.Exists(source))
            {
                DemarrerTraitementArchive(source);
            }
            else
            {
                throw new Exception("'" + source + "' n'est pas un argument valide.");
            }
        }

        #region Traitement de dossier vers archive

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

        private void DemarrerTraitementRepertoire(string repertoire)
        {
            string racine = Path.GetDirectoryName(repertoire);
            string nomArchive = Path.GetFileName(repertoire) + ".zip";
            string cheminArchive = Path.Combine(racine, nomArchive);

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
                IEnumerable<string> chemins = ObtenirFichierDuRepertoire(repertoire);

                // exclure les fichiers
                chemins = ExclureCheminsNonValide(chemins);

                // création de l'archive
                CreerArchiveZip(repertoire, chemins, cheminArchive, tacheProgression);
            }
        }

        private bool EstCheminValide(string chemin, IEnumerable<string> dossierExclus)
        {
            if (chemin.EndsWith(".suo", StringComparison.OrdinalIgnoreCase))
                return false;
            if (chemin.EndsWith(".csproj.user", StringComparison.OrdinalIgnoreCase))
                return false;

            foreach (string dossierExclu in dossierExclus)
            {
                if (chemin.StartsWith(dossierExclu, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        private IEnumerable<string> ExclureCheminsNonValide(IEnumerable<string> chemins)
        {
            // exclure *.suo
            // exclure *.csproj.user
            // exclure bin et obj qd à coté de *.csproj

            chemins = chemins.ToArray();
            IEnumerable<string> dossierProjets = chemins
                .Where(c => c.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) || c.EndsWith(".vcxproj", StringComparison.OrdinalIgnoreCase))
                .Select(c => Path.GetDirectoryName(c));
            IEnumerable<string> dossierExclus = dossierProjets
                .SelectMany(d => new[]{
                    Path.Combine(d, "bin")
                    , Path.Combine(d, "obj")
                    , Path.Combine(d, "release")
                    , Path.Combine(d, "debug")
                    , Path.Combine(d, "Debug sans analyse de code")
                })
                .ToArray();

            IEnumerable<string> resultat = chemins
                .Where(c => EstCheminValide(c, dossierExclus));

            return resultat;
        }

        private IEnumerable<string> ObtenirFichierDuRepertoire(string repertoire)
        {
            IEnumerable<string> resultat = Directory.EnumerateFiles(repertoire, "*", SearchOption.AllDirectories);

            return resultat;
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

        #endregion Traitement de dossier vers archive

        #region Traitement d'archive vers dossier

        private static string GetFirstDirectory(string chemin)
        {
            string directory = Path.GetDirectoryName(chemin);
            if (String.IsNullOrEmpty(directory))
                return chemin;

            return GetFirstDirectory(directory);
        }

        private void DemarrerTraitementArchive(string cheminArchive)
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

        private void zipFile_ExtractProgress(object sender, ExtractProgressEventArgs e, TacheProgression tacheProgression)
        {
            ArgumentProgression argumentProgression = tacheProgression.StatutProgression.ObtenirProgression();
            if (e.EventType == ZipProgressEventType.Extracting_BeforeExtractEntry)
            {
                argumentProgression.Maximum = e.EntriesTotal;
                argumentProgression.Progression = e.EntriesExtracted;
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

        #endregion Traitement d'archive vers dossier
    }
}