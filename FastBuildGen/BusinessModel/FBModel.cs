using System;
using System.ComponentModel;
using FastBuildGen.Common;
using System.Collections.Generic;

namespace FastBuildGen.BusinessModel
{
    internal class FBModel : INotifyPropertyChanged
    {
        #region Consts

        #region Param Guid

        public static readonly Guid ConstGuidDsac = Guid.Empty;
        public static readonly Guid ConstGuidForceOutputDirPath = Guid.Empty;
        public static readonly Guid ConstGuidFxcop = Guid.Empty;
        public static readonly Guid ConstGuidHelp = Guid.Empty;
        public static readonly Guid ConstGuidKillheo = Guid.Empty;
        public static readonly Guid ConstGuidNosgp = Guid.Empty;
        public static readonly Guid ConstGuidNowarn = Guid.Empty;
        public static readonly Guid ConstGuidPara = Guid.Empty;
        public static readonly Guid ConstGuidQuiet = Guid.Empty;
        public static readonly Guid ConstGuidRebuild = Guid.Empty;
        public static readonly Guid ConstGuidRelease = Guid.Empty;
        public static readonly Guid ConstGuidVer = Guid.Empty;
        public static readonly Guid ConstGuidVshost = Guid.Empty;
        public static readonly Guid ConstGuidWait = Guid.Empty;

        #endregion

        #region Param switch

        public const string ConstKeywordParamSwitchDsac = "dsac";
        public const string ConstKeywordParamSwitchForceOutputDirPath = "fo";
        public const string ConstKeywordParamSwitchFxcop = "fxcop";
        public const string ConstKeywordParamSwitchHelp = "?";
        public const string ConstKeywordParamSwitchKillheo = "killheo";
        public const string ConstKeywordParamSwitchNosgp = "nosgp";
        public const string ConstKeywordParamSwitchNowarn = "nowarn";
        public const string ConstKeywordParamSwitchPara = "p";
        public const string ConstKeywordParamSwitchQuiet = "q";
        public const string ConstKeywordParamSwitchRebuild = "rebuild";
        public const string ConstKeywordParamSwitchRelease = "r";
        public const string ConstKeywordParamSwitchVer = "ver";
        public const string ConstKeywordParamSwitchVshost = "vshost";
        public const string ConstKeywordParamSwitchWait = "wait";

        #endregion Param switch

        #region Parm switch help

        public const string ConstDescriptionParamSwitchDsac = "générer Debug sans analyse de code au lieu de Debug";
        public const string ConstDescriptionParamSwitchForceOutputDirPath = "Force la sortie de génération du code dans le répertoire de Lanceur";
        public const string ConstDescriptionParamSwitchFxcop = "générer avec FxCop";
        public const string ConstDescriptionParamSwitchHelp = "cette aide";
        public const string ConstDescriptionParamSwitchKillheo = "tue le processus Heo.exe";
        public const string ConstDescriptionParamSwitchNosgp = "ne pas éxécuter SGenPlus, permet de réduire le temps de compilation";
        public const string ConstDescriptionParamSwitchNowarn = "pas de warning";
        public const string ConstDescriptionParamSwitchPara = "travaille parallelise";
        public const string ConstDescriptionParamSwitchQuiet = "réduit la sortie de MSBuild au erreur et warning uniquement";
        public const string ConstDescriptionParamSwitchRebuild = "nettoie et recompile";
        public const string ConstDescriptionParamSwitchRelease = "générer Release au lieu de Debug";
        public const string ConstDescriptionParamSwitchVer = "numéro de version de fastbuild";
        public const string ConstDescriptionParamSwitchVshost = "Tue le processus Heo.Lanceur.vshost.exe pour le rechargement de l'IDE";
        public const string ConstDescriptionParamSwitchWait = "place une pause à la fin de la génération";

        #endregion Parm switch help

        #endregion Consts

        private FBTarget[] _fastBuildHeoParamTargets;
        private FBTarget[] _fastBuildParamTargets;

        public FBModel()
        {
            Targets = new ObservableDictionary<Guid, FBSolutionTarget> { };
            MacroTargets = new ObservableDictionary<Guid, FBMacroSolutionTarget> { };
            InternalVars = new ObservableDictionary<string, string> { };

            Initialize();
        }

        private void Initialize()
        {
            // Default configuration

            _fastBuildParamTargets = new[] {
                new FBTarget(ConstGuidHelp   ){ Name = ConstKeywordParamSwitchHelp    , Keyword = ConstKeywordParamSwitchHelp      , HelpText = ConstDescriptionParamSwitchHelp       },
                new FBTarget(ConstGuidPara   ){ Name = ConstKeywordParamSwitchPara    , Keyword = ConstKeywordParamSwitchPara      , HelpText = ConstDescriptionParamSwitchPara       },
                new FBTarget(ConstGuidQuiet  ){ Name = ConstKeywordParamSwitchQuiet   , Keyword = ConstKeywordParamSwitchQuiet     , HelpText = ConstDescriptionParamSwitchQuiet      },
                new FBTarget(ConstGuidRelease){ Name = ConstKeywordParamSwitchRelease , Keyword = ConstKeywordParamSwitchRelease   , HelpText = ConstDescriptionParamSwitchRelease    },
                new FBTarget(ConstGuidDsac   ){ Name = ConstKeywordParamSwitchDsac    , Keyword = ConstKeywordParamSwitchDsac      , HelpText = ConstDescriptionParamSwitchDsac       },
                new FBTarget(ConstGuidFxcop  ){ Name = ConstKeywordParamSwitchFxcop   , Keyword = ConstKeywordParamSwitchFxcop     , HelpText = ConstDescriptionParamSwitchFxcop      },
                new FBTarget(ConstGuidNowarn ){ Name = ConstKeywordParamSwitchNowarn  , Keyword = ConstKeywordParamSwitchNowarn    , HelpText = ConstDescriptionParamSwitchNowarn     },
                new FBTarget(ConstGuidRebuild){ Name = ConstKeywordParamSwitchRebuild , Keyword = ConstKeywordParamSwitchRebuild   , HelpText = ConstDescriptionParamSwitchRebuild    },
                new FBTarget(ConstGuidWait   ){ Name = ConstKeywordParamSwitchWait    , Keyword = ConstKeywordParamSwitchWait      , HelpText = ConstDescriptionParamSwitchWait       },
                new FBTarget(ConstGuidNosgp  ){ Name = ConstKeywordParamSwitchNosgp   , Keyword = ConstKeywordParamSwitchNosgp     , HelpText = ConstDescriptionParamSwitchNosgp      },
                new FBTarget(ConstGuidVer    ){ Name = ConstKeywordParamSwitchVer     , Keyword = ConstKeywordParamSwitchVer       , HelpText = ConstDescriptionParamSwitchVer        },
            };
            _fastBuildHeoParamTargets = new[]{
                new FBTarget(ConstGuidVshost            ){ Name = ConstKeywordParamSwitchVshost            , Keyword = ConstKeywordParamSwitchVshost               , HelpText = ConstDescriptionParamSwitchVshost              },
                new FBTarget(ConstGuidKillheo           ){ Name = ConstKeywordParamSwitchKillheo           , Keyword = ConstKeywordParamSwitchKillheo              , HelpText = ConstDescriptionParamSwitchKillheo             },
                new FBTarget(ConstGuidForceOutputDirPath){ Name = ConstKeywordParamSwitchForceOutputDirPath, Keyword = ConstKeywordParamSwitchForceOutputDirPath   , HelpText = ConstDescriptionParamSwitchForceOutputDirPath  },
            };

#warning TODO DELTA point - retirer ce code d'initialisation
#if DEBUG
            FBSolutionTarget chiModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "chi", Keyword = "chi", HelpText = "compilation module Chiffrage", MSBuildTarget = @"Modules\Chiffrage\Heo_Chiffrage_Vue", Enabled = true };
            FBSolutionTarget edpModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "edp", Keyword = "edp", HelpText = "compilation module EditeurProjet", MSBuildTarget = @"Modules\EditeurProjet\Heo_EditeurProjet_Vue", Enabled = true };
            FBSolutionTarget edsModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "eds", Keyword = "eds", HelpText = "compilation module EditeurSymbole", MSBuildTarget = @"Modules\EditeurSymbole\Heo_EditeurSymbole_Vue", Enabled = true };
            FBSolutionTarget etqModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "etq", Keyword = "etq", HelpText = "compilation module Etiquette", MSBuildTarget = @"Modules\Etiquette\Heo_Etiquette_Vue", Enabled = true };
            FBSolutionTarget expModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "exp", Keyword = "exp", HelpText = "compilation module ExplorateurProjet", MSBuildTarget = @"Modules\ExplorateurProjet\Heo_ExplorateurProjet_Vue", Enabled = true };
            FBSolutionTarget gduModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "gdu", Keyword = "gdu", HelpText = "compilation module GDU", MSBuildTarget = @"Modules\GDU\Heo_GestionnaireDonneeUtilisateur_Vue", Enabled = true };
            FBSolutionTarget gcpModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "gcp", Keyword = "gcp", HelpText = "compilation module GuideChoixProduit", MSBuildTarget = @"Modules\GuideChoixProduit\Heo_GuideChoixProduit_Vue", Enabled = true };
            FBSolutionTarget impModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "imp", Keyword = "imp", HelpText = "compilation module Implantation", MSBuildTarget = @"Modules\Implantation\Heo_Implantation_Vue", Enabled = true };
            FBSolutionTarget ldmModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "ldm", Keyword = "ldm", HelpText = "compilation module ListeMateriel", MSBuildTarget = @"Modules\ListeMateriel\Heo_ListeMateriel_Vue", Enabled = true };
            FBSolutionTarget masModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "mas", Keyword = "mas", HelpText = "compilation module MultifilaireAssiste", MSBuildTarget = @"Modules\MultifilaireAssiste\Heo_MultifilaireAssiste_Vue", Enabled = true };
            FBSolutionTarget uasModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "uas", Keyword = "uas", HelpText = "compilation module UnifilaireAssiste", MSBuildTarget = @"Modules\UnifilaireAssiste\Heo_UnifilaireAssiste_Vue", Enabled = true };
            FBSolutionTarget echModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "ech", Keyword = "ech", HelpText = "compilation module Calcul Echauffement", MSBuildTarget = @"Modules\CalculEchauffement\Heo_CalculEchauffement_Vue", Enabled = true };
            FBSolutionTarget lanceurModule = new FBSolutionTarget(Guid.NewGuid()) { Name = "lanceur", Keyword = "lanceur", HelpText = "compilation module Lanceur", MSBuildTarget = @"Heo_Lanceur", Enabled = true };

            Targets.Add(chiModule.Id, chiModule);
            Targets.Add(edpModule.Id, edpModule);
            Targets.Add(edsModule.Id, edsModule);
            Targets.Add(etqModule.Id, etqModule);
            Targets.Add(expModule.Id, expModule);
            Targets.Add(gduModule.Id, gduModule);
            Targets.Add(gcpModule.Id, gcpModule);
            Targets.Add(impModule.Id, impModule);
            Targets.Add(ldmModule.Id, ldmModule);
            Targets.Add(masModule.Id, masModule);
            Targets.Add(uasModule.Id, uasModule);
            Targets.Add(echModule.Id, echModule);
            Targets.Add(lanceurModule.Id, lanceurModule);

            Guid[] minimalTargetDependencies = new[] { lanceurModule.Id, expModule.Id, edpModule.Id };
            FBMacroSolutionTarget minimalTarget = new FBMacroSolutionTarget(Guid.NewGuid()) { Name = "minimal", Keyword = "minimal", HelpText = "compilation de Lanceur, ExplorateurProjet et EditeurProjet" };
            minimalTarget.TargetIds.AddRange(minimalTargetDependencies);

            Guid[] heoTargetDependencies = new[] {
                chiModule  .Id
                , edpModule.Id
                , edsModule.Id
                , etqModule.Id
                , expModule.Id
                , gduModule.Id
                , gcpModule.Id
                , impModule.Id
                , ldmModule.Id
                , masModule.Id
                , uasModule.Id
                , echModule.Id
                , lanceurModule.Id
            };
            FBMacroSolutionTarget heoTarget = new FBMacroSolutionTarget(Guid.NewGuid()) { Name = "heo", Keyword = "heo", HelpText = "compilation Minimal + tous les modules" };
            heoTarget.TargetIds.AddRange(heoTargetDependencies);

            Guid[] schematiqueAssisteDependencies = new[] { uasModule.Id, masModule.Id };
            FBMacroSolutionTarget schematiqueAssiste = new FBMacroSolutionTarget(Guid.NewGuid()) { Name = "sas", Keyword = "sas", HelpText = "compilation de UAS et MAS" };
            schematiqueAssiste.TargetIds.AddRange(schematiqueAssisteDependencies);

            MacroTargets.Add(minimalTarget.Id, minimalTarget);
            MacroTargets.Add(heoTarget.Id, heoTarget);
            MacroTargets.Add(schematiqueAssiste.Id, schematiqueAssiste);
#endif
        }

        public ObservableDictionary<Guid, FBSolutionTarget> Targets { get; private set; }

        public ObservableDictionary<Guid, FBMacroSolutionTarget> MacroTargets { get; private set; }

        public ObservableDictionary<string, string> InternalVars { get; private set; }

        public IEnumerable<FBTarget> FastBuildHeoParamTargets
        {
            get { return _fastBuildHeoParamTargets; }
        }

        public IEnumerable<FBTarget> FastBuildParamTargets
        {
            get { return _fastBuildParamTargets; }
        }

        private bool _withEchoOff;

        public bool WithEchoOff
        {
            get { return _withEchoOff; }
            set
            {
                if (_withEchoOff == value) return;
                _withEchoOff = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFBEvent.ConstFBModelWithEchoOff));
            }
        }

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}