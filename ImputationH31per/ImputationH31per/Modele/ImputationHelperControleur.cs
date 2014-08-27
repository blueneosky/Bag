using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using ImputationH31per.Data;
using ImputationH31per.Data.Entite;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Modele
{
    public class ImputationH31perControleur : IImputationH31perControleur
    {
        private readonly IImputationH31perModele _modele;

        public ImputationH31perControleur(IImputationH31perModele modele)
        {
            _modele = modele;
        }

        public IEnumerable<IImputationTfsNotifiable> AjouterImputationTfs(IEnumerable<IInformationImputationTfs> imputationTfss)
        {
            return imputationTfss.Select(AjouterImputationTfs).Execute();
        }

        public void ChargerData(IServiceData serviceData)
        {
            bool valider = VerifierEtEnregistrerDonneAvantPerte();
            if (!valider)
                return;

            IDatData<IDatTacheTfs, IDatIHFormParametre> data = serviceData.ObtenirData();

            // nettoyage
            _modele.ViderImputationTfss();

            try
            {
                // chargement
                foreach (IDatTacheTfs datTacheTfs in data.TacheTfss)
                {
                    int numero = datTacheTfs.Numero;
                    bool estPremierTicket = true;
                    foreach (IDatTicketTfs datTicketTfs in datTacheTfs.TicketTfss)
                    {
                        int? numeroComplementaire = datTicketTfs.NumeroComplementaire;
                        bool estPremiereImputation = true;

                        IEnumerable<IDatImputationTfs> datImputationTfss = datTicketTfs.ImputationTfss.Execute();
                        Debug.Assert(datImputationTfss.Any());

                        // création et définition des imputations
                        foreach (IDatImputationTfs datImputationTfs in datImputationTfss)
                        {
                            IImputationTfs imputationTfs = _modele.CreerImputationTfs(numero, numeroComplementaire, datImputationTfs.DateHorodatage);
                            imputationTfs.DefinirProprietesImputationTfs(datImputationTfs);

                            if (estPremiereImputation)
                            {
                                // définition des info du tickets
                                IInformationTicketTfs informationTicketTfs = _modele.ObtenirTicketTfs(numero, numeroComplementaire);
                                informationTicketTfs.DefinirProprietesTicketTfs(datTicketTfs);

                                estPremiereImputation = false;
                            }
                        }

                        if (estPremierTicket)
                        {
                            // définition des info de la tâche
                            IInformationTacheTfs tacheTfs = _modele.ObtenirTacheTfs(numero);
                            tacheTfs.DefinirProprietesTacheTfs(datTacheTfs);

                            estPremierTicket = false;
                        }
                    }
                }
                _modele.ServiceDataActif = serviceData;
            }
            catch (IHException)
            {
                MessageBox.Show("Erreur de chargement des données - ré-initialisation");
                _modele.ViderImputationTfss();
                _modele.ServiceDataActif = null;
            }
            finally
            {
                _modele.ReinitialisetEstModifier();
            }
        }

        public void ChargerDataParDefaut()
        {
            IServiceData serviceData = FabriqueServiceData.ServiceDataParDefaut;
            ChargerData(serviceData);
        }

        public void EnregistrerData(IServiceData serviceData)
        {
            DatData data = new DatData();
            data.TacheTfss = _modele.TacheTfss
                .Select(t => new DatTacheTfs(t))
                .ToArray();

            serviceData.EnregistrerData(data);
        }

        public void EnregistrerDataActive()
        {
            IServiceData serviceData = _modele.ServiceDataActif;
            if (serviceData == null)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Souhaitez-vous enregistrer dans les données pas défaut ?"
                    , "Aucune Données active"
                    , MessageBoxButtons.YesNo
                    , MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1
                    );
                if (dialogResult == DialogResult.Yes)
                {
                    serviceData = FabriqueServiceData.ServiceDataParDefaut;
                }
            }

            if (serviceData != null)
            {
                EnregistrerData(serviceData);
                _modele.ReinitialisetEstModifier();
            }
        }

        public bool VerifierEtEnregistrerDonneAvantPerte()
        {
            if (!_modele.EstModifie)
                return true;

            DialogResult dialogResult = MessageBox.Show(
                "Souhaitez-vous enregistrer avant de continuer ?"
                , "Modification en attente sur " + _modele.ServiceDataActif.Nom
                , MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1
                );

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    EnregistrerDataActive();
                }
                catch (IHException e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);

                    return false;
                }
            }

            return true;
        }

        private IImputationTfsNotifiable AjouterImputationTfs(IInformationImputationTfs informationImputationTfs)
        {
            IImputationTfsNotifiable imputationTfs = null;
            try
            {
                imputationTfs = _modele.CreerImputationTfs(informationImputationTfs);
                imputationTfs.DefinirProprietes(informationImputationTfs);
            }
            catch (IHException)
            {
                MessageBox.Show("Imputation déjà insérée : " + informationImputationTfs.NumeroComplet() + " " + informationImputationTfs.NomComplet());
            }

            return imputationTfs;
        }
    }
}