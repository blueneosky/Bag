﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele;

namespace ImputationH31per.Vue.RapportMensuel.Modele
{
    public class RapportMensuelFormModele : IRapportMensuelFormModele
    {
        #region Mebres

        private readonly IImputationH31perModele _imputationH31perModele;

        #endregion Mebres

        #region ctor

        public RapportMensuelFormModele(IImputationH31perModele imputationH31perModele)
        {
            this._imputationH31perModele = imputationH31perModele;
        }

        #endregion ctor

        #region Propriétés

        public IImputationH31perModele ImputationH31perModele
        {
            get { return _imputationH31perModele; }
        }

        #endregion Propriétés

        #region IRapportMensuelFormModele Membres

#warning TODO - point ALPHA - implémenter !
        public DateTime DateMoisAnnee
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<Entite.GroupeItem> Groupes
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Entite.TacheItem> Taches
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<Entite.TicketItem> Tickets
        {
            get { throw new NotImplementedException(); }
        }

        public Entite.GroupeItem GroupeSelectionne
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Entite.TacheItem TacheSelectionnee
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Entite.TicketItem TicketSelectionne
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}