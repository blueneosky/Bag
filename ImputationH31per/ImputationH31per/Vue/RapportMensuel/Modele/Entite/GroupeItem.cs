﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.RapportMensuel.Modele.Entite
{
    public class GroupeItem : InformationBaseItem<IInformationTacheTfs>
    {
        public GroupeItem(EnumTypeItem typeItem)
            : base(typeItem)
        {
        }

        public GroupeItem(IInformationTacheTfs information)
            : base(information)
        {
        }

        protected override string ObtenirLibelleEntite()
        {
            return Information.NomGroupement;
        }
    }
}