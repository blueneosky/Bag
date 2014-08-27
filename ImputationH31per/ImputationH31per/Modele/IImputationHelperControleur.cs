using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Data;
using ImputationH31per.Modele.Entite;

using IImputationTfsModele = ImputationH31per.Modele.Entite.IImputationTfsNotifiable;

namespace ImputationH31per.Modele
{
    public interface IImputationH31perControleur
    {
        IEnumerable<IImputationTfsModele> AjouterImputationTfs(IEnumerable<IInformationImputationTfs> imputationTfss);

        void ChargerData(IServiceData serviceData);

        void ChargerDataParDefaut();

        void EnregistrerData(IServiceData serviceData);

        void EnregistrerDataActive();

        bool VerifierEtEnregistrerDonneAvantPerte();
    }
}