using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public class ImputationTfsData : ImputationTfsBase
    {
        #region ctor

        public ImputationTfsData(int numero, int? numeroComplentaire, DateTimeOffset dateHorodatage)
            : base(numero, numeroComplentaire, dateHorodatage)
        {
        }

        public ImputationTfsData(IInformationTicketTfs ticketTfs, DateTimeOffset dateHorodatage)
            : base(ticketTfs, dateHorodatage)
        {
        }

        #endregion ctor

        public static ImputationTfsData Copier(IInformationImputationTfs imputation)
        {
            ImputationTfsData resultat = new ImputationTfsData(imputation, imputation.DateHorodatage);
            resultat.DefinirProprietes(imputation);

            return resultat;
        }
    }
}