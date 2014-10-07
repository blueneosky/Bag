using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class Regroupement : BaseItem<string>, IEnumerable<IInformationItem<IInformationTacheTfs>>
    {
        private readonly List<IInformationItem<IInformationTacheTfs>> _items;
        private int? _totalHeure;

        public Regroupement(string nom)
            : base(nom)
        {
            _items = new List<IInformationItem<IInformationTacheTfs>> { };
        }

        public string Nom
        {
            get { return base.Entite; }
            set
            {
                base.Entite = value;
                NotifierNomModifie(this, EventArgs.Empty);
            }
        }

        public int? TotalHeure
        {
            get { return _totalHeure; }
            set
            {
                _totalHeure = value;
                if (_totalHeure.HasValue)
                {
                    const int constCentre = 1;
                    int totalDemiJournee;
                    int excedantDemiJournee;
                    ObtenirTotalDemiJourneeAvecExcedant(_totalHeure ?? 0, constCentre, out totalDemiJournee, out excedantDemiJournee);
                    TotalDemiJournee = totalDemiJournee;
                    ExcedantDemiJournee = excedantDemiJournee;
                }
                else
                {
                    TotalDemiJournee = 0;
                    ExcedantDemiJournee = 0;
                }
            }
        }

        public int TotalDemiJournee { get; private set; }

        public int ExcedantDemiJournee { get; private set; }

        public event EventHandler<EventArgs> NomModifie;

        protected virtual void NotifierNomModifie(object sender, EventArgs e)
        {
            NomModifie.Notifier(sender, e);
        }

        protected override bool EntiteEgale(string entite)
        {
            return String.Equals(Entite, entite);
        }

        private static void ObtenirTotalDemiJourneeAvecExcedant(int total, int centre, out int demiJournee, out int excedantDemiJournee)
        {
            int totalCentre = total + centre;
            demiJournee = (int)(totalCentre / 4);
            excedantDemiJournee = (totalCentre % 4) - centre;
        }

        protected override string ObtenirLibelleEntite()
        {
            StringBuilder resultat = new StringBuilder(Nom);
            if (TotalHeure.HasValue)
            {
                int totalDemiJournee = TotalDemiJournee;
                int excedantDemiJournee = ExcedantDemiJournee;

                resultat
                    .Append(" (")
                    .AppendFormat("{0} jours", (totalDemiJournee / 2.0));
                if (excedantDemiJournee != 0)
                {
                    resultat.AppendFormat(" {0}{1} h", (excedantDemiJournee > 0 ? "+" : ""), excedantDemiJournee);
                }
                resultat.Append(")");
            }
            return resultat.ToString();
        }

        public List<IInformationItem<IInformationTacheTfs>> Items
        {
            get { return _items; }
        }

        internal Regroupement Clone()
        {
            Regroupement resultat = new Regroupement(this.Nom)
            {
                TotalHeure = this.TotalHeure,
            };

            return resultat;
        }

        #region IEnumerable<IInformationItem<IInformationImputationTfs>> Membres

        public IEnumerator<IInformationItem<IInformationTacheTfs>> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion IEnumerable<IInformationItem<IInformationImputationTfs>> Membres

        #region IEnumerable Membres

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable Membres
    }
}