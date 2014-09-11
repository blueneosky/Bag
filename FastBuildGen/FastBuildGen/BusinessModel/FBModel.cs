using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class FBModel : INotifyPropertyChanged
    {
        #region Consts

        #region Param Guid

        public static readonly Guid ConstGuidDsac = new Guid("1190A21D-1580-484C-B095-C8F43CA0E67B");
        public static readonly Guid ConstGuidFxcop = new Guid("8DB510A1-3B3B-4BF7-A667-C5DEC17BA01C");
        public static readonly Guid ConstGuidHelp = new Guid("ABB65B99-24DC-496E-9277-CB7F771F3FD6");
        public static readonly Guid ConstGuidKillheo = new Guid("3B047684-732E-47A5-A7F0-E030D85D97BC");
        public static readonly Guid ConstGuidNosgp = new Guid("6B000177-D119-419D-A5DC-811AC49FB5D1");
        public static readonly Guid ConstGuidNowarn = new Guid("CB4DE11B-4F24-44C7-BD92-D3C5DE1750D9");
        public static readonly Guid ConstGuidPara = new Guid("7C0941F7-112E-4193-A570-58D854D7B802");
        public static readonly Guid ConstGuidQuiet = new Guid("994E58BB-985D-45AA-BE39-6BE29DF78126");
        public static readonly Guid ConstGuidRebuild = new Guid("682C0892-498B-4581-8D6F-D7B032203658");
        public static readonly Guid ConstGuidRelease = new Guid("EB20E8E7-FAC8-4569-8A38-9DC34E9584F3");
        public static readonly Guid ConstGuidVer = new Guid("56DF0C4D-7E37-404B-A056-1B0D039C3B86");
        public static readonly Guid ConstGuidVshost = new Guid("0BA5D674-D740-453F-A108-960AB3B61CC2");
        public static readonly Guid ConstGuidWait = new Guid("5DA6C3C1-A32D-400F-9B9D-93A3EE049095");
        public static readonly Guid ConstGuidAll = new Guid("FBBFCA6A-5EED-4918-A66C-E32A8E6DA097");

        #endregion Param Guid

        #region Param switch

        public const string ConstKeywordParamSwitchDsac = "dsac";
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
        public const string ConstKeywordParamSwitchAll = "all";

        #endregion Param switch

        #region Parm switch help

        public const string ConstDescriptionParamSwitchDsac = "générer Debug sans analyse de code au lieu de Debug";
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
        public const string ConstDescriptionParamSwitchAll = "génère tout, cible par défaut si aucune spécifiée";

        #endregion Parm switch help

        #endregion Consts

        private FBTarget[] _heoParamTargets;
        private FBTarget[] _paramTargets;

        private FBMacroSolutionTarget _macroSolutionTargetAll;

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
                new FBTarget(ConstGuidHelp   , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchHelp      , HelpText = ConstDescriptionParamSwitchHelp       },
                new FBTarget(ConstGuidPara   , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchPara      , HelpText = ConstDescriptionParamSwitchPara       },
                new FBTarget(ConstGuidQuiet  , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchQuiet     , HelpText = ConstDescriptionParamSwitchQuiet      },
                new FBTarget(ConstGuidRelease, EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchRelease   , HelpText = ConstDescriptionParamSwitchRelease    },
                new FBTarget(ConstGuidDsac   , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchDsac      , HelpText = ConstDescriptionParamSwitchDsac       },
                new FBTarget(ConstGuidFxcop  , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchFxcop     , HelpText = ConstDescriptionParamSwitchFxcop      },
                new FBTarget(ConstGuidNowarn , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchNowarn    , HelpText = ConstDescriptionParamSwitchNowarn     },
                new FBTarget(ConstGuidRebuild, EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchRebuild   , HelpText = ConstDescriptionParamSwitchRebuild    },
                new FBTarget(ConstGuidWait   , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchWait      , HelpText = ConstDescriptionParamSwitchWait       },
                new FBTarget(ConstGuidNosgp  , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchNosgp     , HelpText = ConstDescriptionParamSwitchNosgp      },
                new FBTarget(ConstGuidVer    , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchVer       , HelpText = ConstDescriptionParamSwitchVer        },
            };
            _heoParamTargets = new[]{
                new FBTarget(ConstGuidVshost , EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchVshost    , HelpText = ConstDescriptionParamSwitchVshost     },
                new FBTarget(ConstGuidKillheo, EnumFBTargetReadonly.MaskAll){ Keyword = ConstKeywordParamSwitchKillheo   , HelpText = ConstDescriptionParamSwitchKillheo    },
            };

            _macroSolutionTargetAll = new FBMacroSolutionTargetAll(ConstGuidAll)
            {
                Keyword = ConstKeywordParamSwitchAll,
                HelpText = ConstDescriptionParamSwitchAll,
            };
            MacroSolutionTargets.Add(_macroSolutionTargetAll);
            SolutionTargets.CollectionChanged += SolutionTargets_CollectionChanged;
        }

        private void SolutionTargets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _macroSolutionTargetAll.SolutionTargetIds.AddRange(e.NewItems.OfType<FBTarget>().Select(t => t.Id));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    _macroSolutionTargetAll.SolutionTargetIds.RemoveRange(e.OldItems.OfType<FBTarget>().Select(t => t.Id));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    _macroSolutionTargetAll.SolutionTargetIds.Clear();
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                default:
                    Debug.Fail("Unspecified case");
                    break;
            }
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