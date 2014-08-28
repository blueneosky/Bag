using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public abstract class BatchFileBase : BatchNodeBase, IEnumerable<BatchFileNodeBase>
    {
        private bool _withEchoOff;

        protected BatchFileBase(bool withEchoOff = true)
        {
            _withEchoOff = withEchoOff;
        }

        public bool WithEchoOff
        {
            get { return _withEchoOff; }
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