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
                    int totalCentre = (_totalHeure ?? 0) + constCentre;
                    TotalDemiJournee = (int)(totalCentre / 4);
                    ExcedantDemiJournee = (totalCentre % 4) - constCentre;
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

        protected override string ObtenirLibelleEntite()
        {
            string resultat = Nom;
            if (TotalHeure.HasValue)
                resultat += " (" + (TotalDemiJournee / 2.0) + " jours " + (ExcedantDemiJournee > 0 ? "+" : "") + ExcedantDemiJournee + " h)";
            return resultat;
        }

        public List<IInformationItem<IInformationTacheTfs>> Items
        {
            get { return _items; }
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