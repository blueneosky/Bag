using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public class InformationImputationTfs : InformationImputationTfsBase
    {
        #region ctor

        public InformationImputationTfs(IInformationTicketTfs ticketTfs, DateTimeOffset dateHorodatage)
            : base(ticketTfs, dateHorodatage)
        {
        }

        public InformationImputationTfs(int numero, int? numeroComplementaire, DateTimeOffset dateHorodatage)
            : base(numero, numeroComplementaire, dateHorodatage)
        {
        }

        #endregion ctor

        public static InformationImputationTfs Copier(IInformationImputationTfs informationTfs)
        {
            InformationImputationTfs resultat = new InformationImputationTfs(informationTfs, informationTfs.DateHorodatage);
            resultat.DefinirProprietes(informationTfs);

            return resultat;
        }
    }
}