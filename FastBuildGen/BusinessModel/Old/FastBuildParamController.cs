using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel.Old
{
    internal class FastBuildParamController : IFastBuildParamController
    {
        private readonly IFastBuildParamModel _model;

        public FastBuildParamController(IFastBuildParamModel model)
        {
            _model = model;
        }

        public IParamDescriptionHeoModule AddModule(string name, string keyword)
        {
            return AddModuleCore(name, keyword);
        }

        public IParamDescriptionHeoTarget AddTarget(string name, string keyword)
        {
            return AddTargetCore(name, keyword);
        }

        public bool DeleteModule(string name)
        {
            return DeleteModuleCore(name);
        }

        public bool DeleteTarget(string name)
        {
            return DeleteTargetCore(name);
        }

        public string GetUniqKeyword(string baseKeyword)
        {
            string keyword = new IParamDescription[0]
                .Concat(_model.HeoModuleParams)
                .Concat(_model.HeoTargetParams)
                .Select(m => m.Keyword)
                .UniqName(baseKeyword);

            return keyword;
        }

        public string GetUniqName(string baseName)
        {
            string name = new IParamDescription[0]
                .Concat(_model.HeoModuleParams)
                .Concat(_model.HeoTargetParams)
                .Select(m => m.Name)
                .UniqName(baseName);

            return name;
        }

        public IParamDescriptionHeoModule NewModule(string baseName)
        {
            string name = GetUniqName(baseName);
            string keyword = GetUniqKeyword(name);
            IParamDescriptionHeoModule newModule = AddModuleCore(name, keyword);

            return newModule;
        }

        public IParamDescriptionHeoTarget NewTarget(string baseName)
        {
            string name = GetUniqName(baseName);
            string keyword = GetUniqKeyword(name);
            IParamDescriptionHeoTarget newTarget = AddTargetCore(name, keyword);

            return newTarget;
        }

        #region Core

        protected IParamDescriptionHeoModule AddModuleCore(string name, string keyword)
        {
            IParamDescriptionHeoModule result = null;

            try
            {
                result = _model.AddHeoModuleParam(name, keyword);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }

        protected IParamDescriptionHeoTarget AddTargetCore(string name, string keyword)
        {
            IParamDescriptionHeoTarget result = null;

            try
            {
                result = _model.AddHeoTargetParam(name, keyword);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return result;
        }

        protected bool DeleteModuleCore(string name)
        {
            bool success = false;

            try
            {
                IEnumerable<IParamDescriptionHeoTarget> targets = _model.HeoTargetParams
                    .Where(t => t.Dependencies.Any(m => m.Name == name))
                    .ToArray();
                bool isUsedByTargets = targets.Any();
                success = true;
                if (isUsedByTargets)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want delete all targets used by module '" + name + "' ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult != DialogResult.Yes)
                        return false;
                    foreach (IParamDescriptionHeoTarget target in targets)
                    {
                        success = success && DeleteTargetCore(target.Name);
                    }
                }

                if (success)
                    success = _model.RemoveHeoModuleParam(name);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return success;
        }

        protected bool DeleteTargetCore(string name)
        {
            bool success = false;

            try
            {
                success = _model.RemoveHeoTargetParam(name);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return success;
        }

        #endregion Core
    }
}