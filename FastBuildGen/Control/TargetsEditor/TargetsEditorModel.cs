using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Control.TargetsEditor
{
    internal class TargetsEditorModel : ListEditorModel
    {
        private readonly IFastBuildParamModel _fastBuildParamModel;

        public TargetsEditorModel(IFastBuildParamModel fastBuildParamModel, IUndoRedoManager undoRedoManager)
            : base(undoRedoManager)
        {
            _fastBuildParamModel = fastBuildParamModel;

            _fastBuildParamModel.HeoTargetParamsChanged += _fastBuildParamModel_HeoTargetParamsChanged;

            UpdateTargets();
        }

        public IFastBuildParamModel FastBuildParamModel
        {
            get { return _fastBuildParamModel; }
        }

        public IParamDescriptionHeoTarget TargetSelected
        {
            get
            {
                TargetElement targetElement = ElementSelected as TargetElement;
                IParamDescriptionHeoTarget target = (targetElement != null) ? targetElement.Target : null;

                return target;
            }
        }

        private void _fastBuildParamModel_HeoTargetParamsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateTargets();
        }

        private void UpdateTargets()
        {
            IEnumerable<IParamDescriptionHeoTarget> targets = _fastBuildParamModel.HeoTargetParams;

            IEnumerable<TargetElement> elements = targets
                .Select(t => new TargetElement(t));
            Elements = elements;
        }
    }
}