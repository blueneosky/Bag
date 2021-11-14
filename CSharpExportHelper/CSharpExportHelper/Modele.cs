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
                new ArchiveCreator().DemarrerTraitementRepertoire(source);
            }
            else if (File.Exists(source))
            {
                new ArchiveExtractor().DemarrerTraitementArchive(source);
            }
            else
            {
                throw new Exception("'" + source + "' n'est pas un argument valide.");
            }
        }
    }
}