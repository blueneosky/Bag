using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public abstract class BatchNodeBase
    {
        private Guid _nodeGuid;

        protected BatchNodeBase()
        {
            _nodeGuid = Guid.NewGuid();
        }

        public Guid NodeGuid
        {
            get { return _nodeGuid; }
        }

        public abstract object Accept(IBatchNodeVisitor visitor);
    }
}