using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Modele;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;
using System.Text;

namespace ImputationH31per.Vue.RapportMail.Modele
{
    public class RapportMailFormModele : IRapportMailFormModele
    {
        #region Constantes

        private static IComparer<IInformationImputationTfs> ConstanteOrdreAffichageImputationTfs = ImputationTfs.ComparateurImputationTfsCroissant;

        #endregion Constantes

        #region Membres

        private readonly IImputationH31perModele _ImputationH31perModele;

        private IEnumerable<IInformationImputationTfs> _imputationTfsDisponibles;
        private IEnumerable<IInformationImputationTfs> _imputationTfsSelectionnees;
        private DateTimeOffset _tempsDebut;
        private DateTimeOffset _tempsFin;
        private string _texteRapport;
        private int _sommeDifferenceHeureConsommee;

        #endregion Membres

        #region ctor

        public RapportMailFormModele(IImputationH31perModele ImputationH31perModele)
        {
            _ImputationH31perModele = ImputationH31perModele;

            _ImputationH31perModele.ImputationTfssAChange += _ImputationH31perModele_ImputationTfssAChange;

            DefinirTempsDebutEtFin(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow);
            //MiseAJourModele(_ImputationH31perModele); // fait depuis DefinirTempsDebutEtFin(...)
        }

        #endregion ctor

        #region Evennement

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(sender, e);
        }

        #endregion Evennement

        #region Propriété

        public IEnumerable<IInformationImputationTfs> ImputationTfsDisponibles
        {
            get { return _imputationTfsDisponibles; }
            protected set
            {
                if (_imputationTfsDisponibles == value)
                    return;
                _imputationTfsDisponibles = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMailFormModele.ConstanteProprieteImputationTfsDisponibles));
            }
        }

        public IEnumerable<IInformationImputationTfs> ImputationTfsSelectionnees
        {
            get { return _imputationTfsSelectionnees; }
            protected set
            {
                if (_imputationTfsSelectionnees == value)
                    return;
                _imputationTfsSelectionnees = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMailFormModele.ConstanteProprieteImputationTfsSelectionnees));
            }
        }

        public DateTimeOffset TempsDebut
        {
            get { return _tempsDebut; }
            protected set
            {
                if (_tempsDebut == value)
                    return;
                _tempsDebut = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMailFormModele.ConstanteProprieteTempsDebut));
            }
        }

        public DateTimeOffset TempsFin
        {
            get { return _tempsFin; }
            protected set
            {
                if (_tempsFin == value)
                    return;
                _tempsFin = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMailFormModele.ConstanteProprieteTempsFin));
            }
        }

        public string TexteRapport
        {
            get { return _texteRapport; }
            protected set
            {
                if (_texteRapport == value)
                    return;
                _texteRapport = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMailFormModele.ConstanteProprieteTexteRapport));
            }
        }

        public int SommeDifferenceHeureConsommee
        {
            get { return _sommeDifferenceHeureConsommee; }
            protected set
            {
                if (_sommeDifferenceHeureConsommee == value)
                    return;
                _sommeDifferenceHeureConsommee = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIRapportMailFormModele.ConstanteProprieteSommeDifferenceHeureConsommee));
            }
        }

        #endregion Propriété

        #region Abonnement

        private void _ImputationH31perModele_ImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            MiseAJourModele(_ImputationH31perModele);
        }

        #endregion Abonnement

        #region Mise à jour modèle

        private void MiseAJourImputationTfsDisponibles()
        {
            DateTimeOffset dateMin = _tempsDebut.Date;
            DateTimeOffset dateMax = _tempsFin.AddDays(1).Date;

            ImputationTfsDisponibles = _ImputationH31perModele.ImputationTfss
                .Where(i => { DateTimeOffset date = i.DateImputationPlusRecente(); return date.EstComprisEntre(dateMin, dateMax); })
                .Reverse()  // optimisation
                .OrderBy(i => i, ConstanteOrdreAffichageImputationTfs)
                .Execute();

            DefinirImputationTfsSelectionnees(ImputationTfsDisponibles);
        }

        private void MiseAJourModele(IImputationH31perModele _ImputationH31perModele)
        {
            MiseAJourImputationTfsDisponibles();
        }

        private void _MiseAJourTexteRapport()
        {
            IEnumerable<IInformationImputationTfs> imputations = ImputationTfsSelectionnees
                .Execute();
            string[] entete = new[] { "Numéro\tNom\tEstim (h)\tDate estim\tConsommé (h)\tDate consomé\tDiff\tCommentaire" };
            string[] corps = imputations
                .OrderBy(i => i, ConstanteOrdreAffichageImputationTfs)
                .Select(_ObtenirTexteImputationTfs)
                .ToArray();

            string texte = entete.Concat(corps)
                .Aggregate((acc, l) => acc + Environment.NewLine + l);

            TexteRapport = texte;
            SommeDifferenceHeureConsommee = (int)(imputations.Sum(i => _ImputationH31perModele.ObtenirDifferenceConsommee(i) ?? 0));
        }

        private void MiseAJourTexteRapport()
        {
            IEnumerable<IInformationImputationTfs> imputations = ImputationTfsSelectionnees
                .Execute();
            string[][] entete = new[] { new[] { "Numéro", "Nom", "Estim (h)", "Date estim", "Consommé (h)", "Date consomé", "Diff", "Commentaire" } };
            IEnumerable<IEnumerable<string>> corps = imputations
                .OrderBy(i => i, ConstanteOrdreAffichageImputationTfs)
                .Select(ObtenirTexteImputationTfs)
                ;

            StringBuilder builder = new StringBuilder();
            IEnumerable<StringBuilder> buildList = entete
                .Concat(corps)
                .Select(l => l.Skip(1).Aggregate(builder.Append(l.First()), (acc, e) => acc.Append("\t").Append(e)).AppendLine())
                ;
            foreach (var item in buildList) ;
            string texte = builder.ToString();

            TexteRapport = texte;
            SommeDifferenceHeureConsommee = (int)(imputations.Sum(i => _ImputationH31perModele.ObtenirDifferenceConsommee(i) ?? 0));
        }



        private IEnumerable< string> ObtenirTexteImputationTfs(IInformationImputationTfs imputationTfs)
        {
            // Numéro        Nom        Estim (h)        Date estim        Consommé        Date consomé        Diff

            bool estEstimationDefinie = imputationTfs.EstEstimationDefinie();
            bool estConsommeDefinie = imputationTfs.EstConsommeDefinie();

            string texteDiff = String.Empty;
            double? diff = _ImputationH31perModele.ObtenirDifferenceConsommee(imputationTfs);
            if (diff.HasValue)
            {
                bool estDiffPositive = diff > 0;
                texteDiff = (estDiffPositive ? "+" : "") + diff;
            }
            string commentaire = imputationTfs.Commentaire;

            string[] resultat = new[]
            {
                imputationTfs.NumeroComplet(),
                imputationTfs.NomComplet(),
                estEstimationDefinie ? imputationTfs.EstimCourant.ToString() : String.Empty,
                estEstimationDefinie ? imputationTfs.DateEstimCourant.Value.LocalDateTime.ToShortDateString() : String.Empty,
                estConsommeDefinie ? imputationTfs.SommeConsommee.ToString() : String.Empty,
                estConsommeDefinie ? imputationTfs.DateSommeConsommee.Value.LocalDateTime.ToShortDateString() : String.Empty,
                texteDiff,
                commentaire,
            };

            return resultat;
        }


        private string _ObtenirTexteImputationTfs(IInformationImputationTfs imputationTfs)
        {
            // Numéro        Nom        Estim (h)        Date estim        Consommé        Date consomé        Diff
            string resultat = "";

            bool estEstimationDefinie = imputationTfs.EstEstimationDefinie();
            bool estConsommeDefinie = imputationTfs.EstConsommeDefinie();

            string texteDiff = String.Empty;
            double? diff = _ImputationH31perModele.ObtenirDifferenceConsommee(imputationTfs);
            if (diff.HasValue)
            {
                bool estDiffPositive = diff > 0;
                texteDiff = (estDiffPositive ? "+" : "") + diff;
            }
            string commentaire = imputationTfs.Commentaire;

            string[] data = new[]
            {
                imputationTfs.NumeroComplet(),
                imputationTfs.NomComplet(),
                estEstimationDefinie ? imputationTfs.EstimCourant.ToString() : String.Empty,
                estEstimationDefinie ? imputationTfs.DateEstimCourant.Value.LocalDateTime.ToShortDateString() : String.Empty,
                estConsommeDefinie ? imputationTfs.SommeConsommee.ToString() : String.Empty,
                estConsommeDefinie ? imputationTfs.DateSommeConsommee.Value.LocalDateTime.ToShortDateString() : String.Empty,
                texteDiff,
                commentaire,
            };

            resultat = data.Aggregate((acc, e) => acc + "\t" + e);

            return resultat;
        }

        #endregion Mise à jour modèle

        #region Méthodes

        public void DefinirImputationTfsSelectionnees(IEnumerable<IInformationImputationTfs> imputationTfss)
        {
            ImputationTfsSelectionnees = imputationTfss;

            MiseAJourTexteRapport();
        }

        public void DefinirTempsDebutEtFin(DateTimeOffset tempsDebut, DateTimeOffset tempsFin)
        {
            if ((TempsDebut == tempsDebut) && (TempsFin == tempsFin))
                return;

            TempsDebut = tempsDebut;
            TempsFin = tempsFin;

            MiseAJourImputationTfsDisponibles();
        }

        #endregion Méthodes
    }
}