using System;
using System.Collections.Generic;
using System.ComponentModel;
using FastBuildGen.Common;
using System.Collections.ObjectModel;

namespace FastBuildGen.BusinessModel
{
    internal class FBModel : INotifyPropertyChanged
    {
        #region Consts

        #region Param Guid

        public static readonly Guid ConstGuidDsac = Guid.NewGuid();
        public static readonly Guid ConstGuidForceOutputDirPath = Guid.NewGuid();
        public static readonly Guid ConstGuidFxcop = Guid.NewGuid();
        public static readonly Guid ConstGuidHelp = Guid.NewGuid();
        public static readonly Guid ConstGuidKillheo = Guid.NewGuid();
        public static readonly Guid ConstGuidNosgp = Guid.NewGuid();
        public static readonly Guid ConstGuidNowarn = Guid.NewGuid();
        public static readonly Guid ConstGuidPara = Guid.NewGuid();
        public static readonly Guid ConstGuidQuiet = Guid.NewGuid();
        public static readonly Guid ConstGuidRebuild = Guid.NewGuid();
        public static readonly Guid ConstGuidRelease = Guid.NewGuid();
        public static readonly Guid ConstGuidVer = Guid.NewGuid();
        public static readonly Guid ConstGuidVshost = Guid.NewGuid();
        public static readonly Guid ConstGuidWait = Guid.NewGuid();

        #endregion Param Guid

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

        private FBTarget[] _heoParamTargets;
        private FBTarget[] _paramTargets;

        public FBModel()
        {
            SolutionTargets = new ObservableCollection<FBSolutionTarget> { };
            MacroSolutionTargets = new ObservableCollection<FBMacroSolutionTarget> { };
            InternalVars = new ReadOnlyDictionary<string, string>(ConstFBModel.InternalVarDefaultProperties);

            Initialize();
        }

        private void Initialize()
        {
            // Default configuration

            _paramTargets = new[] {
                new FBTarget(ConstGuidHelp   ){ Keyword = ConstKeywordParamSwitchHelp      , HelpText = ConstDescriptionParamSwitchHelp       },
                new FBTarget(ConstGuidPara   ){ Keyword = ConstKeywordParamSwitchPara      , HelpText = ConstDescriptionParamSwitchPara       },
                new FBTarget(ConstGuidQuiet  ){ Keyword = ConstKeywordParamSwitchQuiet     , HelpText = ConstDescriptionParamSwitchQuiet      },
                new FBTarget(ConstGuidRelease){ Keyword = ConstKeywordParamSwitchRelease   , HelpText = ConstDescriptionParamSwitchRelease    },
                new FBTarget(ConstGuidDsac   ){ Keyword = ConstKeywordParamSwitchDsac      , HelpText = ConstDescriptionParamSwitchDsac       },
                new FBTarget(ConstGuidFxcop  ){ Keyword = ConstKeywordParamSwitchFxcop     , HelpText = ConstDescriptionParamSwitchFxcop      },
                new FBTarget(ConstGuidNowarn ){ Keyword = ConstKeywordParamSwitchNowarn    , HelpText = ConstDescriptionParamSwitchNowarn     },
                new FBTarget(ConstGuidRebuild){ Keyword = ConstKeywordParamSwitchRebuild   , HelpText = ConstDescriptionParamSwitchRebuild    },
                new FBTarget(ConstGuidWait   ){ Keyword = ConstKeywordParamSwitchWait      , HelpText = ConstDescriptionParamSwitchWait       },
                new FBTarget(ConstGuidNosgp  ){ Keyword = ConstKeywordParamSwitchNosgp     , HelpText = ConstDescriptionParamSwitchNosgp      },
                new FBTarget(ConstGuidVer    ){ Keyword = ConstKeywordParamSwitchVer       , HelpText = ConstDescriptionParamSwitchVer        },
            };
            _heoParamTargets = new[]{
                new FBTarget(ConstGuidVshost            ){ Keyword = ConstKeywordParamSwitchVshost               , HelpText = ConstDescriptionParamSwitchVshost              },
                new FBTarget(ConstGuidKillheo           ){ Keyword = ConstKeywordParamSwitchKillheo              , HelpText = ConstDescriptionParamSwitchKillheo             },
                new FBTarget(ConstGuidForceOutputDirPath){ Keyword = ConstKeywordParamSwitchForceOutputDirPath   , HelpText = ConstDescriptionParamSwitchForceOutputDirPath  },
            };

#warning TODO DELTA point - retirer ce code d'initialisation
#if DEBUG
            FBSolutionTarget chiModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "chi", HelpText = "compilation module Chiffrage", MSBuildTarget = @"Modules\Chiffrage\Heo_Chiffrage_Vue", Enabled = true };
            FBSolutionTarget edpModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "edp", HelpText = "compilation module EditeurProjet", MSBuildTarget = @"Modules\EditeurProjet\Heo_EditeurProjet_Vue", Enabled = true };
            FBSolutionTarget edsModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "eds", HelpText = "compilation module EditeurSymbole", MSBuildTarget = @"Modules\EditeurSymbole\Heo_EditeurSymbole_Vue", Enabled = true };
            FBSolutionTarget etqModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "etq", HelpText = "compilation module Etiquette", MSBuildTarget = @"Modules\Etiquette\Heo_Etiquette_Vue", Enabled = true };
            FBSolutionTarget expModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "exp", HelpText = "compilation module ExplorateurProjet", MSBuildTarget = @"Modules\ExplorateurProjet\Heo_ExplorateurProjet_Vue", Enabled = true };
            FBSolutionTarget gduModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "gdu", HelpText = "compilation module GDU", MSBuildTarget = @"Modules\GDU\Heo_GestionnaireDonneeUtilisateur_Vue", Enabled = true };
            FBSolutionTarget gcpModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "gcp", HelpText = "compilation module GuideChoixProduit", MSBuildTarget = @"Modules\GuideChoixProduit\Heo_GuideChoixProduit_Vue", Enabled = true };
            FBSolutionTarget impModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "imp", HelpText = "compilation module Implantation", MSBuildTarget = @"Modules\Implantation\Heo_Implantation_Vue", Enabled = true };
            FBSolutionTarget ldmModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "ldm", HelpText = "compilation module ListeMateriel", MSBuildTarget = @"Modules\ListeMateriel\Heo_ListeMateriel_Vue", Enabled = true };
            FBSolutionTarget masModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "mas", HelpText = "compilation module MultifilaireAssiste", MSBuildTarget = @"Modules\MultifilaireAssiste\Heo_MultifilaireAssiste_Vue", Enabled = true };
            FBSolutionTarget uasModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "uas", HelpText = "compilation module UnifilaireAssiste", MSBuildTarget = @"Modules\UnifilaireAssiste\Heo_UnifilaireAssiste_Vue", Enabled = true };
            FBSolutionTarget echModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "ech", HelpText = "compilation module Calcul Echauffement", MSBuildTarget = @"Modules\CalculEchauffement\Heo_CalculEchauffement_Vue", Enabled = true };
            FBSolutionTarget lanceurModule = new FBSolutionTarget(Guid.NewGuid()) { Keyword = "lanceur", HelpText = "compilation module Lanceur", MSBuildTarget = @"Heo_Lanceur", Enabled = true };

            SolutionTargets.Add(chiModule);
            SolutionTargets.Add(edpModule);
            SolutionTargets.Add(edsModule);
            SolutionTargets.Add(etqModule);
            SolutionTargets.Add(expModule);
            SolutionTargets.Add(gduModule);
            SolutionTargets.Add(gcpModule);
            SolutionTargets.Add(impModule);
            SolutionTargets.Add(ldmModule);
            SolutionTargets.Add(masModule);
            SolutionTargets.Add(uasModule);
            SolutionTargets.Add(echModule);
            SolutionTargets.Add(lanceurModule);

            Guid[] minimalTargetDependencies = new[] { lanceurModule.Id, expModule.Id, edpModule.Id };
            FBMacroSolutionTarget minimalTarget = new FBMacroSolutionTarget(Guid.NewGuid()) { Keyword = "minimal", HelpText = "compilation de Lanceur, ExplorateurProjet et EditeurProjet" };
            minimalTarget.SolutionTargetIds.AddRange(minimalTargetDependencies);

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
            FBMacroSolutionTarget heoTarget = new FBMacroSolutionTarget(Guid.NewGuid()) { Keyword = "heo", HelpText = "compilation Minimal + tous les modules" };
            heoTarget.SolutionTargetIds.AddRange(heoTargetDependencies);

            Guid[] schematiqueAssisteDependencies = new[] { uasModule.Id, masModule.Id };
            FBMacroSolutionTarget schematiqueAssiste = new FBMacroSolutionTarget(Guid.NewGuid()) { Keyword = "sas", HelpText = "compilation de UAS et MAS" };
            schematiqueAssiste.SolutionTargetIds.AddRange(schematiqueAssisteDependencies);

            MacroSolutionTargets.Add(minimalTarget);
            MacroSolutionTargets.Add(heoTarget);
            MacroSolutionTargets.Add(schematiqueAssiste);
#endif
        }

        public ObservableCollection<FBSolutionTarget> SolutionTargets { get; private set; }

        public ObservableCollection<FBMacroSolutionTarget> MacroSolutionTargets { get; private set; }

        public ReadOnlyDictionary<string, string> InternalVars { get; private set; }

        public IEnumerable<FBTarget> HeoParamTargets
        {
            get { return _heoParamTargets; }
        }

        public IEnumerable<FBTarget> ParamTargets
        {
            get { return _paramTargets; }
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