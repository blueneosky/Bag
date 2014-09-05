using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.SolutionTargetsEditor
{
    internal class SolutionTargetsEditorModel : ListEditorModel
    {
        private readonly ApplicationModel _applicationModel;

        public SolutionTargetsEditorModel(ApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;

            _applicationModel.PropertyChanged += _applicationModel_PropertyChanged;

            UpdateSolutionTargets();

            AddEnabled = false;
            RemoveEnabled = false;
        }

        public ApplicationModel ApplicationModel
        {
            get { return _applicationModel; }
        }

        private FBModel _fbModel;

        public FBModel FBModel
        {
            get { return _fbModel; }
            set
            {
                if (_fbModel != null)
                {
                    _fbModel.SolutionTargets.CollectionChanged -= _fbModel_SolutionTargets_CollectionChanged;
                }
                _fbModel = value;
                if (_fbModel != null)
                {
                    _fbModel.SolutionTargets.CollectionChanged += _fbModel_SolutionTargets_CollectionChanged;
                }
            }
        }

        public FBSolutionTarget SolutionTargetSelected
        {
            get
            {
                SolutionTargetElement moduleElement = ElementSelected as SolutionTargetElement;
                FBSolutionTarget module = (moduleElement != null) ? moduleElement.SolutionTarget : null;

                return module;
            }
        }

        private void _applicationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstApplicationModelFBModel:
                    UpdateFBModel();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void _fbModel_SolutionTargets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSolutionTargets();
        }

        private void UpdateFBModel()
        {
            FBModel = _applicationModel.FBModel;
            UpdateSolutionTargets();
        }

        private void UpdateSolutionTargets()
        {
            FBModel fbModel = FBModel;
            IEnumerable<SolutionTargetElement> elements;
            if (fbModel != null)
            {
                elements = fbModel.SolutionTargets
                    .Select(t => new SolutionTargetElement(t));
            }
            else
            {
                elements = null;
            }

            if (Elements != null)
            {
                foreach (SolutionTargetElement ste in Elements.Cast<SolutionTargetElement>())
                {
                    ste.Dispose();
                }
            }

            Elements = elements;
        }
    }
}