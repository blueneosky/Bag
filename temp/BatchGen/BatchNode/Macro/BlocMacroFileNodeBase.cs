using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Macro
{
    public abstract class BlocMacroFileNodeBase : BatchFileNodeBase, IEnumerable<BatchFileNodeBase>
    {
        private bool _withIncTab;

        protected BlocMacroFileNodeBase(bool withIncTab = false)
        {
            _withIncTab = withIncTab;
        }

        public bool WithIncTab
        {
            get { return _withIncTab; }
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public abstract IEnumerator<BatchFileNodeBase> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}