using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.ModuleEditor
{
    internal class ModuleEditorController
    {
        private readonly ModuleEditorModel _model;

        public ModuleEditorController(ModuleEditorModel model)
        {
            _model = model;
        }

        public void SetMSBuildTarget(string value)
        {
            _model.Module.MSBuildTarget = value;
        }

        public void SetParamDescriptionHeoModule(IParamDescriptionHeoModule value)
        {
            _model.Module = value;
        }

        public void SetPlatform(EnumPlatform value)
        {
            _model.Module.Platform = value;
        }
    }
}