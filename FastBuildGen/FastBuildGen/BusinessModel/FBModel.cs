using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class FBModel : INotifyPropertyChanged
    {
        #region Consts

        #region Param Guid

#warning TODO ALPHA point - add real const id
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

        public IEnumerable<FBTarget> AllTargets
        {
            get
            {
                return ParamTargets
                    .Concat(HeoParamTargets)
                    .Concat(SolutionTargets)
                    .Concat(MacroSolutionTargets);
            }
        }

        internal bool IsKeywordUsed(string value)
        {
            bool isKeywordUsed = ParamTargets
                .Concat(HeoParamTargets)
                .Concat(SolutionTargets)
                .Concat(MacroSolutionTargets)
                .Any(t => t.Keyword == value);

            return isKeywordUsed;
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