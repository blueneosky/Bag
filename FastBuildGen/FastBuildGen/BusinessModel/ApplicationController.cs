using System;
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
#warning TODO ALPHA point - do stuff
            throw new System.NotImplementedException();
        }


        internal FBMacroSolutionTarget NewMacroSolutionTarget(string newKeyword)
        {
#warning TODO ALPHA point - do stuff
            throw new NotImplementedException();
        }

        internal FBSolutionTarget NewSolutionTarget(string baseKeyword)
        {
#warning TODO ALPHA point - do stuff
            throw new NotImplementedException();
        }

        internal bool DeleteSolutionTarget(Guid guid)
        {
#warning TODO ALPHA point - do stuff
            throw new NotImplementedException();
        }
    }
}