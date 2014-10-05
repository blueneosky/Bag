using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public abstract class BaseItem<T> : IItem<T>
    {
        #region Membres

        private readonly T _entite;
        private readonly EnumTypeItem _typeItem;

        #endregion Membres

        #region ctor

        protected BaseItem(EnumTypeItem typeItem)
            : this(typeItem, default(T))
        {
        }

        protected BaseItem(T entite)
            : this(EnumTypeItem.Entite, entite)
        {
        }

        protected BaseItem(EnumTypeItem typeItem, T entite)
        {
            Debug.Assert(typeItem == EnumTypeItem.Entite && entite != null || typeItem != EnumTypeItem.Entite && entite == null);
            _typeItem = typeItem;
            _entite = entite;
        }

        #endregion ctor

        #region IItem<T> Membres

        public T Entite
        {
            get { return _entite; }
        }

        public string Libelle
        {
            get { return ObtenirLibelle(); }
        }

        public EnumTypeItem TypeItem
        {
            get { return _typeItem; }
        }

        #endregion IItem<T> Membres

        #region Méthodes

        private string ObtenirLibelle()
        {
            switch (_typeItem)
            {
                case EnumTypeItem.Aucun:
                    return "- Aucun -";
                case EnumTypeItem.Tous:
                    return "- Tous -";
                case EnumTypeItem.Entite:
                    return ObtenirLibelleEntite();
                default:
                    Debug.Fail("Cas non prévus");
                    return ">> @?! <<";
            }
        }

        protected abstract string ObtenirLibelleEntite();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            IItem<T> item = obj as IItem<T>;
            bool resultat = (this.TypeItem == item.TypeItem)
                && (this.TypeItem != EnumTypeItem.Entite || EntiteEgale(item.Entite));

            return resultat;
        }

        protected virtual bool EntiteEgale(T entite)
        {
            return Object.Equals(Entite, entite);
        }

        public override int GetHashCode()
        {
            if (TypeItem == EnumTypeItem.Entite)
                return Entite.GetHashCode();
            return (int)TypeItem;
        }

        #endregion Méthodes
    }
}