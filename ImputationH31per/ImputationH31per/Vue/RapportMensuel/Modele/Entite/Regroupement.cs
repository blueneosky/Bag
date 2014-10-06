using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class Regroupement : BaseItem<string>, IEnumerable<IInformationItem<IInformationTacheTfs>>
    {
        private List<IInformationItem<IInformationTacheTfs>> _items;

        public Regroupement(string nom)
            : base(nom)
        {
            _items = new List<IInformationItem<IInformationTacheTfs>> { };
        }

        public string Nom
        {
            get { return base.Entite; }
            set { base.Entite = value; }
        }

        protected override bool EntiteEgale(string entite)
        {
            return String.Equals(Entite, entite);
        }

        protected override string ObtenirLibelleEntite()
        {
            return Nom;
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