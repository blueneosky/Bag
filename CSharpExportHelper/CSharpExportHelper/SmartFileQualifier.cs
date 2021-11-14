using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpExportHelper
{
    public static class SmartFileQualifier
    {
        private static bool EstCheminValide(string chemin, IEnumerable<string> dossierExclus)
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

        public static IEnumerable<string> ExclureCheminsNonValide(IEnumerable<string> chemins)
        {
            // exclure *.suo
            // exclure *.csproj.user
            // exclure bin et obj qd à coté de *.csproj

            chemins = chemins.ToArray();
            IEnumerable<string> dossierProjets = chemins
                .Where(c => c.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) || c.EndsWith(".vcxproj", StringComparison.OrdinalIgnoreCase))
                .Select(c => Path.GetDirectoryName(c));
            IEnumerable<string> dossierExclus = dossierProjets
                .SelectMany(d => new[]
                {
                    Path.Combine(d, "bin"),
                    Path.Combine(d, "obj"),
                    Path.Combine(d, "release"),
                    Path.Combine(d, "debug"),
                    Path.Combine(d, "Debug sans analyse de code")
                })
                .ToArray();

            IEnumerable<string> dossierSolutions = chemins
                .Where(c => c.EndsWith(".sln", StringComparison.OrdinalIgnoreCase))
                .Select(c => Path.GetDirectoryName(c));
            dossierExclus = dossierExclus.Concat(dossierSolutions
                    .SelectMany(d => new[]
                    {
                        Path.Combine(d, "Packages"),
                        Path.Combine(d, ".vs")
                    }))
                .ToArray();

            IEnumerable<string> resultat = chemins
                .Where(c => EstCheminValide(c, dossierExclus));

            return resultat;
        }

        public static IEnumerable<string> ObtenirFichierDuRepertoire(string repertoire)
        {
            IEnumerable<string> resultat = Directory.EnumerateFiles(repertoire, "*", SearchOption.AllDirectories);

            return resultat;
        }

    }
}