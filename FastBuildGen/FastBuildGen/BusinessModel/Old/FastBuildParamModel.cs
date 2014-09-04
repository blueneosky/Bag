using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel.Old
{
    internal class FastBuildParamModel : IFastBuildParamModel
    {
        #region Consts

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

        #region Fields

        private readonly ObservableCollection<IParamDescriptionHeoModule> _heoModuleParams;
        private readonly ObservableCollection<IParamDescriptionHeoTarget> _heoTargetParams;
        private bool _dataChanged;
        private IParamDescription[] _fastBuildHeoParams;
        private IParamDescription[] _fastBuildParams;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FastBuildParamModel()
        {
            _heoModuleParams = new ObservableCollection<IParamDescriptionHeoModule> { };
            _heoTargetParams = new ObservableCollection<IParamDescriptionHeoTarget> { };

            _heoModuleParams.CollectionChanged += _heoModulesParams_CollectionChanged;
            _heoTargetParams.CollectionChanged += _heoTargetsParams_CollectionChanged;
        }

        public void Initialize()
        {
            // Default configuration

            _fastBuildParams = new[] {
                new ParamDescription(ConstKeywordParamSwitchHelp      ){ Keyword = ConstKeywordParamSwitchHelp      , HelpText = ConstDescriptionParamSwitchHelp       },
                new ParamDescription(ConstKeywordParamSwitchPara      ){ Keyword = ConstKeywordParamSwitchPara      , HelpText = ConstDescriptionParamSwitchPara       },
                new ParamDescription(ConstKeywordParamSwitchQuiet     ){ Keyword = ConstKeywordParamSwitchQuiet     , HelpText = ConstDescriptionParamSwitchQuiet      },
                new ParamDescription(ConstKeywordParamSwitchRelease   ){ Keyword = ConstKeywordParamSwitchRelease   , HelpText = ConstDescriptionParamSwitchRelease    },
                new ParamDescription(ConstKeywordParamSwitchDsac      ){ Keyword = ConstKeywordParamSwitchDsac      , HelpText = ConstDescriptionParamSwitchDsac       },
                new ParamDescription(ConstKeywordParamSwitchFxcop     ){ Keyword = ConstKeywordParamSwitchFxcop     , HelpText = ConstDescriptionParamSwitchFxcop      },
                new ParamDescription(ConstKeywordParamSwitchNowarn    ){ Keyword = ConstKeywordParamSwitchNowarn    , HelpText = ConstDescriptionParamSwitchNowarn     },
                new ParamDescription(ConstKeywordParamSwitchRebuild   ){ Keyword = ConstKeywordParamSwitchRebuild   , HelpText = ConstDescriptionParamSwitchRebuild    },
                new ParamDescription(ConstKeywordParamSwitchWait      ){ Keyword = ConstKeywordParamSwitchWait      , HelpText = ConstDescriptionParamSwitchWait       },
                new ParamDescription(ConstKeywordParamSwitchNosgp     ){ Keyword = ConstKeywordParamSwitchNosgp     , HelpText = ConstDescriptionParamSwitchNosgp      },
                new ParamDescription(ConstKeywordParamSwitchVer       ){ Keyword = ConstKeywordParamSwitchVer       , HelpText = ConstDescriptionParamSwitchVer        },
            };
            _fastBuildHeoParams = new[]{
                new ParamDescription(ConstKeywordParamSwitchVshost                ){ Keyword = ConstKeywordParamSwitchVshost               , HelpText = ConstDescriptionParamSwitchVshost              },
                new ParamDescription(ConstKeywordParamSwitchKillheo               ){ Keyword = ConstKeywordParamSwitchKillheo              , HelpText = ConstDescriptionParamSwitchKillheo             },
                new ParamDescription(ConstKeywordParamSwitchForceOutputDirPath    ){ Keyword = ConstKeywordParamSwitchForceOutputDirPath   , HelpText = ConstDescriptionParamSwitchForceOutputDirPath  },
            };

#if DEBUG
            ParamDescriptionHeoModule chiModule = new ParamDescriptionHeoModule("chi") { Keyword = "chi", HelpText = "compilation module Chiffrage", MSBuildTarget = @"Modules\Chiffrage\Heo_Chiffrage_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule edpModule = new ParamDescriptionHeoModule("edp") { Keyword = "edp", HelpText = "compilation module EditeurProjet", MSBuildTarget = @"Modules\EditeurProjet\Heo_EditeurProjet_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule edsModule = new ParamDescriptionHeoModule("eds") { Keyword = "eds", HelpText = "compilation module EditeurSymbole", MSBuildTarget = @"Modules\EditeurSymbole\Heo_EditeurSymbole_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule etqModule = new ParamDescriptionHeoModule("etq") { Keyword = "etq", HelpText = "compilation module Etiquette", MSBuildTarget = @"Modules\Etiquette\Heo_Etiquette_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule expModule = new ParamDescriptionHeoModule("exp") { Keyword = "exp", HelpText = "compilation module ExplorateurProjet", MSBuildTarget = @"Modules\ExplorateurProjet\Heo_ExplorateurProjet_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule gduModule = new ParamDescriptionHeoModule("gdu") { Keyword = "gdu", HelpText = "compilation module GDU", MSBuildTarget = @"Modules\GDU\Heo_GestionnaireDonneeUtilisateur_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule gcpModule = new ParamDescriptionHeoModule("gcp") { Keyword = "gcp", HelpText = "compilation module GuideChoixProduit", MSBuildTarget = @"Modules\GuideChoixProduit\Heo_GuideChoixProduit_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule impModule = new ParamDescriptionHeoModule("imp") { Keyword = "imp", HelpText = "compilation module Implantation", MSBuildTarget = @"Modules\Implantation\Heo_Implantation_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule ldmModule = new ParamDescriptionHeoModule("ldm") { Keyword = "ldm", HelpText = "compilation module ListeMateriel", MSBuildTarget = @"Modules\ListeMateriel\Heo_ListeMateriel_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule masModule = new ParamDescriptionHeoModule("mas") { Keyword = "mas", HelpText = "compilation module MultifilaireAssiste", MSBuildTarget = @"Modules\MultifilaireAssiste\Heo_MultifilaireAssiste_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule uasModule = new ParamDescriptionHeoModule("uas") { Keyword = "uas", HelpText = "compilation module UnifilaireAssiste", MSBuildTarget = @"Modules\UnifilaireAssiste\Heo_UnifilaireAssiste_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule echModule = new ParamDescriptionHeoModule("ech") { Keyword = "ech", HelpText = "compilation module Calcul Echauffement", MSBuildTarget = @"Modules\CalculEchauffement\Heo_CalculEchauffement_Vue", Platform = EnumPlatform.X86 };
            ParamDescriptionHeoModule lanceurModule = new ParamDescriptionHeoModule("lanceur") { Keyword = "lanceur", HelpText = "compilation module Lanceur", MSBuildTarget = @"Heo_Lanceur", Platform = EnumPlatform.X86 };

            _heoModuleParams.Add(chiModule);
            _heoModuleParams.Add(edpModule);
            _heoModuleParams.Add(edsModule);
            _heoModuleParams.Add(etqModule);
            _heoModuleParams.Add(expModule);
            _heoModuleParams.Add(gduModule);
            _heoModuleParams.Add(gcpModule);
            _heoModuleParams.Add(impModule);
            _heoModuleParams.Add(ldmModule);
            _heoModuleParams.Add(masModule);
            _heoModuleParams.Add(uasModule);
            _heoModuleParams.Add(echModule);
            _heoModuleParams.Add(lanceurModule);

            ParamDescriptionHeoModule[] minimalTargetDependencies = new ParamDescriptionHeoModule[] { lanceurModule, expModule, edpModule };
            ParamDescriptionHeoTarget minimalTarget = new ParamDescriptionHeoTarget("minimal") { Keyword = "minimal", HelpText = "compilation de Lanceur, ExplorateurProjet et EditeurProjet" };
            minimalTarget.AddDependencies(minimalTargetDependencies);

            ParamDescriptionHeoModule[] heoTargetDependencies = new ParamDescriptionHeoModule[] {
                chiModule
                , edpModule
                , edsModule
                , etqModule
                , expModule
                , gduModule
                , gcpModule
                , impModule
                , ldmModule
                , masModule
                , uasModule
                , echModule
                , lanceurModule
            };
            ParamDescriptionHeoTarget heoTarget = new ParamDescriptionHeoTarget("heo") { Keyword = "heo", HelpText = "compilation Minimal + tous les modules" };
            heoTarget.AddDependencies(heoTargetDependencies);

            ParamDescriptionHeoModule[] schematiqueAssisteDependencies = new ParamDescriptionHeoModule[] { uasModule, masModule };
            ParamDescriptionHeoTarget schematiqueAssiste = new ParamDescriptionHeoTarget("sas") { Keyword = "sas", HelpText = "compilation de UAS et MAS" };
            schematiqueAssiste.AddDependencies(schematiqueAssisteDependencies);

            _heoTargetParams.Add(minimalTarget);
            _heoTargetParams.Add(heoTarget);
            _heoTargetParams.Add(schematiqueAssiste);
#endif
        }

        #endregion Ctor

        #region Property

        public bool DataChanged
        {
            get { return _dataChanged; }
            private set
            {
                if (_dataChanged == value)
                    return;
                _dataChanged = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstIFastBuildParamModelEvent.ConstDataChanged));
            }
        }

        public IEnumerable<IParamDescription> FastBuildHeoParams
        {
            get { return _fastBuildHeoParams; }
        }

        public IEnumerable<IParamDescription> FastBuildParams
        {
            get { return _fastBuildParams; }
        }

        public IEnumerable<IParamDescriptionHeoModule> HeoModuleParams
        {
            get { return _heoModuleParams; }
        }

        public IEnumerable<IParamDescriptionHeoTarget> HeoTargetParams
        {
            get { return _heoTargetParams; }
        }

        #endregion Property

        #region Events

        public event NotifyCollectionChangedEventHandler HeoModuleParamsChanged;

        public event NotifyCollectionChangedEventHandler HeoTargetParamsChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnHeoModulesParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HeoModuleParamsChanged.Notify(sender, e);
        }

        protected virtual void OnHeoTargetsParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            HeoTargetParamsChanged.Notify(sender, e);
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        private void _heoModulesParams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // forward
            OnHeoModulesParamsChanged(this, e);
        }

        private void _heoTargetsParams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // forward
            OnHeoTargetsParamsChanged(this, e);
        }

        private void heoModuleParam_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetDataChanged();
        }

        private void heoTargetParam_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetDataChanged();
        }

        #endregion Events

        #region Functions

        public IParamDescriptionHeoModule AddHeoModuleParam(string name, string keyword)
        {
            bool isUsed = IsNameUsed(name);
            if (isUsed)
                throw new FastBuildGenException("Module " + name + " already exist");

            IParamDescriptionHeoModule result = new ParamDescriptionHeoModule(name)
            {
                Keyword = keyword,
                Platform = EnumPlatform.X86,
            };
            _heoModuleParams.Add(result);

            result.PropertyChanged += heoModuleParam_PropertyChanged;

            return result;
        }

        public IParamDescriptionHeoTarget AddHeoTargetParam(string name, string keyword)
        {
            bool isUsed = IsNameUsed(name);
            if (isUsed)
                throw new FastBuildGenException("Target " + name + " already exist");

            IParamDescriptionHeoTarget result = new ParamDescriptionHeoTarget(name)
            {
                Keyword = keyword,
            };
            _heoTargetParams.Add(result);

            result.PropertyChanged += heoTargetParam_PropertyChanged;

            return result;
        }

        public void ClearHeoModuleParams()
        {
            ClearHeoTargetParams(); // depndance

            _heoModuleParams.ForEach(m => m.PropertyChanged -= heoModuleParam_PropertyChanged);
            _heoModuleParams.Clear();
        }

        public void ClearHeoTargetParams()
        {
            _heoTargetParams.ForEach(t => t.PropertyChanged -= heoTargetParam_PropertyChanged);
            _heoTargetParams.Clear();
        }

        public bool IsKeywordUsed(string keyword)
        {
            bool isKeywordUsed = new IParamDescription[0]
                .Concat(HeoModuleParams)
                .Concat(HeoTargetParams)
                .Where(m => m.Keyword == keyword)
                .Any();

            return isKeywordUsed;
        }

        public bool IsNameUsed(string name)
        {
            bool isNameUsed = new IParamDescription[0]
                .Concat(HeoModuleParams)
                .Concat(HeoTargetParams)
                .Where(m => m.Name == name)
                .Any();

            return isNameUsed;
        }

        public bool RemoveHeoModuleParam(string name)
        {
            IParamDescriptionHeoModule module = _heoModuleParams
                .Where(m => m.Name == name)
                .FirstOrDefault();

            if (module == null)
                return false;

            bool success = _heoModuleParams.Remove(module);

            module.PropertyChanged -= heoModuleParam_PropertyChanged;

            return success;
        }

        public bool RemoveHeoTargetParam(string name)
        {
            IParamDescriptionHeoTarget target = _heoTargetParams
                .Where(m => m.Name == name)
                .FirstOrDefault();

            if (target == null)
                return false;

            bool success = _heoTargetParams.Remove(target);

            target.PropertyChanged -= heoTargetParam_PropertyChanged;

            return success;
        }

        public void ResetDataChanged()
        {
            DataChanged = false;
        }

        #endregion Functions

        #region Private

        private void SetDataChanged()
        {
            DataChanged = true;
        }

        #endregion Private
    }
}