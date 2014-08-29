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
    }
}