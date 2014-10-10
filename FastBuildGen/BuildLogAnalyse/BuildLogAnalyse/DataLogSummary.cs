using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildLogAnalyse
{
    public class DataLogSummary
    {

        public DataLogSummary(List<DataLogSet> sets)
        {
            ILookup<long, DataLogSet> setsParJours = sets
               .ToLookup(s => s.Start.Date.Ticks);

            JourTotal = setsParJours.Count;
            NbCompilationTotal = sets.Count;


            List<int> nbCompiles = setsParJours
                .Select(grp => grp.Count())
                .OrderBy(qt => qt)
                .ToList();

            NbCompileParJourMin = nbCompiles.Min();
            NbCompileParJourMax = nbCompiles.Max();

            NbCompileParJourMoyen = nbCompiles.Count > 0 ? nbCompiles.Sum() / nbCompiles.Count : 0;

            NbCompileParJourMedian = nbCompiles.Count > 0 ? nbCompiles[(int)(nbCompiles.Count / 2)] : 0;


            List<long> temps = sets
                .Select(s => s.Ellapsed.Ticks)
                .OrderBy(s => s)
                .ToList();

            TempsCompilationMin = new TimeSpan(temps.Min()).ToString();
            TempsCompilationMax = new TimeSpan(temps.Max()).ToString();
            long ticks = (long)Moyenne(temps.Select(i => (double)i));
            TimeSpan mean = TimeSpan.FromSeconds((long)(new TimeSpan(ticks).TotalSeconds));
            TempsCompilationMoyen = temps.Count > 0 ? mean.ToString() : String.Empty;
            TempsCompilationMedian = temps.Count > 0 ? new TimeSpan(temps[(int)(temps.Count / 2)]).ToString() : String.Empty;

            TimeSpan totalMean = TimeSpan.FromSeconds((long)(new TimeSpan(ticks*NbCompileParJourMoyen).TotalSeconds));
            TempsMoyenConsommeParJour = temps.Count > 0 ? totalMean.ToString() : String.Empty;
        }

        private const int CstMaxMean = 1000;

        private double Moyenne(IEnumerable<double> valeurs)
        {
            valeurs = valeurs.ToList();
            double resultat;

            int qt = valeurs.Count();
            if (qt > CstMaxMean)
            {
                int split = qt / 2;
                double m1 = Moyenne(valeurs.Take(split));
                double m2 = Moyenne(valeurs.Skip(split));
                resultat = (m1 + m2) / 2.0;
            }
            else if (qt > 0)
            {
                resultat = valeurs.Sum() / (double)qt;
            }
            else
            {
                resultat = 0;
            }

            return resultat;
        }

        public int JourTotal { get; private set; }

        public int NbCompilationTotal { get; private set; }

        public int NbCompileParJourMoyen { get; private set; }

        public int NbCompileParJourMax { get; private set; }
        public int NbCompileParJourMin { get; private set; }
        public int NbCompileParJourMedian { get; private set; }

        public string TempsCompilationMax { get; private set; }
        public string TempsCompilationMin { get; private set; }
        public string TempsCompilationMoyen { get; private set; }
        public string TempsCompilationMedian { get; private set; }

        public string TempsMoyenConsommeParJour { get; private set; }
    }
}
