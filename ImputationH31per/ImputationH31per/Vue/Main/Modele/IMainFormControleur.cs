using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Vue.Main.Modele
{
    public interface IMainFormControleur
    {
        void AfficherImportExcel();

        void AfficherRapportMail();

        void EnregistrerData();

        void NouvelleImputation();

        void NouvelleImputationsCourantes();

        void SupprimerImputationTfs(IImputationTfs imputationTfs, bool avecModifieur);

        bool VerifierEtEnregistrerData();

        void AfficherRapportMensuel();
    }
}