using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode
{
    [Serializable]
    public class BatchFile : BatchFileBase, ICollection<BatchFileNodeBase>
    {
        private List<BatchFileNodeBase> _nodes;

        public BatchFile(bool withEchoOff = true)
            : base(withEchoOff)
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
    }
}