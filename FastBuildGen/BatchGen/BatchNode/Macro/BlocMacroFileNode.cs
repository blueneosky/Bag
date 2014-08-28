using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Macro
{
    [Serializable]
    public class BlocMacroFileNode : BlocMacroFileNodeBase, ICollection<BatchFileNodeBase>
    {
        private List<BatchFileNodeBase> _nodes;

        public BlocMacroFileNode(bool withIncTab = false)
            : base(withIncTab)
        {
            _nodes = new List<BatchFileNodeBase> { };
        }

        #region ICollection

        public int Count
        {
            get { return _nodes.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(BatchFileNodeBase item)
        {
            _nodes.Add(item);
        }

        public void Clear()
        {
            _nodes.Clear();
        }

        public bool Contains(BatchFileNodeBase item)
        {
            return _nodes.Contains(item);
        }

        public void CopyTo(BatchFileNodeBase[] array, int arrayIndex)
        {
            _nodes.CopyTo(array, arrayIndex);
        }

        public override IEnumerator<BatchFileNodeBase> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public bool Remove(BatchFileNodeBase item)
        {
            return _nodes.Remove(item);
        }

        #endregion ICollection

        public static BlocMacroFileNode Wrap(BatchFileNodeBase node, bool withIncTab = false)
        {
            BlocMacroFileNode macro = new BlocMacroFileNode(withIncTab);
            macro.Add(node);

            return macro;
        }

        public void AddRange(IEnumerable<BatchFileNodeBase> nodes)
        {
            foreach (BatchFileNodeBase node in nodes)
            {
                Add(node);
            }
        }
    }
}