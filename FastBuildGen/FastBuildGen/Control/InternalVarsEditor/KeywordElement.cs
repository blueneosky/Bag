using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Control.ListEditor;

namespace FastBuildGen.Control.InternalVarsEditor
{
    internal class KeywordElement : ListEditorElement
    {
        private readonly string _keyword;

        public KeywordElement(string keyword)
            : base(keyword)
        {
            _keyword = keyword;
        }

        public string Keyword
        {
            get { return _keyword; }
        }
    }
}