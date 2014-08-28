using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastBuildGen.Common;

namespace FastBuildGen.Control.ListEditor
{
    internal class ListViewItemElement : ListViewItemValue<ListEditorElement>
    {
        public ListViewItemElement(ListEditorElement element)
            : base(element)
        {
        }
    }
}