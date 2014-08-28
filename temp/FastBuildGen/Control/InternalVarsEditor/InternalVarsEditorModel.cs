using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FastBuildGen.BusinessModel;
using FastBuildGen.Control.ListEditor;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.InternalVarsEditor
{
    internal class InternalVarsEditorModel : ListEditorModel
    {
        private readonly IFastBuildInternalVarModel _fastBuildInternalVarModel;

        public InternalVarsEditorModel(IFastBuildInternalVarModel fastBuildInternalVarModel)
        {
            AddEnabled = false;
            RemoveEnabled = false;

            _fastBuildInternalVarModel = fastBuildInternalVarModel;

            _fastBuildInternalVarModel.PropertiesChanged += _fastBuildInternalVarModel_PropertiesChanged;

            UpdateKeywords();
        }

        public IFastBuildInternalVarModel FastBuildInternalVarModel
        {
            get { return _fastBuildInternalVarModel; }
        }

        public string KeywordSelected
        {
            get
            {
                KeywordElement keywordElement = ElementSelected as KeywordElement;
                string keyword = (keywordElement != null) ? keywordElement.Keyword : null;

                return keyword;
            }
        }

        private void _fastBuildInternalVarModel_PropertiesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateKeywords();
        }

        private void UpdateKeywords()
        {
            IEnumerable<string> keywords = _fastBuildInternalVarModel.Properties
                .Select(p => p.Key);

            IEnumerable<KeywordElement> elements = keywords
                .Select(k => new KeywordElement(k));
            Elements = elements;
        }
    }
}