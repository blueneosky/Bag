using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.BusinessModel
{
    internal class ApplicationController
    {
        private readonly ApplicationModel _model;

        public ApplicationController(ApplicationModel model)
        {
            _model = model;
        }

        internal void SaveFastBuildConfig(string configFilePath)
        {
#warning TODO DELTA point - not necessary to keep it
            throw new System.NotImplementedException();
        }

        internal bool LoadFastBuildConfig(string configFilePath)
        {
#warning TODO DELTA point - not necessary to keep it
            throw new System.NotImplementedException();
        }

        internal bool DeleteMacroSolutionTarget(Guid id)
        {
            FBModel fbModel = _model.FBModel;
            if (fbModel == null) return false;

            FBMacroSolutionTarget macroSolutionTarget = fbModel.MacroSolutionTargets
                .FirstOrDefault(mst => mst.Id == id);
            if (macroSolutionTarget == null) return false;

            bool success = fbModel.MacroSolutionTargets.Remove(macroSolutionTarget);

            return success;
        }

        internal FBMacroSolutionTarget NewMacroSolutionTarget(string preferedKeyword)
        {
            FBModel fbModel = _model.FBModel;
            if (fbModel == null) return null;

            preferedKeyword = fbModel.AllTargets
                .Select(t => t.Keyword)
                .UniqName(preferedKeyword);

            FBMacroSolutionTarget macroSolutionTarget = new FBMacroSolutionTarget(Guid.NewGuid());
            macroSolutionTarget.Keyword = preferedKeyword;

            return macroSolutionTarget;
        }
    }
}