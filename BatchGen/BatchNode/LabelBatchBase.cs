using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public abstract class LabelBatchBase : BatchFileNodeBase
    {
        private string _label;

        protected LabelBatchBase(string label)
        {
            _label = label;
        }

        public string Label
        {
            get { return _label; }
        }
    }
}