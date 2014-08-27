using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele.Entite
{
    public abstract class InformationTacheTfsBase : IInformationTacheTfsNotifiable
    {
        #region Membres

        private readonly int _numero;

        private string _nom;
        private string _nomGroupement;

        #endregion Membres

        #region ctor

        protected InformationTacheTfsBase(int numero)
        {
            _numero = numero;
        }

        #endregion ctor

        #region Evennement

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(sender, e);
        }

        #endregion Evennement

        #region Propriétés

        #region Identification

        public virtual int Numero
        {
            get { return _numero; }
        }

        #endregion Identification

        public virtual string Nom
        {
            get { return _nom; }
            set
            {
                if (String.Equals(_nom, value))
                    return;
                _nom = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationTacheTfsNotifiable.ConstanteProprieteNom));
            }
        }

        public virtual string NomGroupement
        {
            get { return _nomGroupement; }
            set
            {
                if (String.Equals(_nomGroupement, value))
                    return;
                _nomGroupement = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIInformationTacheTfsNotifiable.ConstanteProprieteNomGroupement));
            }
        }

        #endregion Propriétés

        #region Overrides

        // override object.Equals
        public override bool Equals(object obj)
        {
            return Object.ReferenceEquals(this, obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return _numero;
        }

        #endregion Overrides
    }
}