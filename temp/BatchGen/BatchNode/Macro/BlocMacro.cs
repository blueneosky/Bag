using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatchGen.BatchNode.Macro
{
    [Serializable]
    public class BlocMacro : BatchStatementNodeBase, ICollection<BatchStatementNodeBase>
    {
        private List<BatchStatementNodeBase> _nodes;
        private bool _withIncTab;

        public BlocMacro(bool withIncTab = false)
        {
            _withIncTab = withIncTab;
            _nodes = new List<BatchStatementNodeBase> { };
        }

        public bool WithIncTab
        {
            get { return _withIncTab; }
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

        public void Add(BatchStatementNodeBase item)
        {
            _nodes.Add(item);
        }

        public void Clear()
        {
            _nodes.Clear();
        }

        public bool Contains(BatchStatementNodeBase item)
        {
            return _nodes.Contains(item);
        }

        public void CopyTo(BatchStatementNodeBase[] array, int arrayIndex)
        {
            _nodes.CopyTo(array, arrayIndex);
        }

        public IEnumerator<BatchStatementNodeBase> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        public bool Remove(BatchStatementNodeBase item)
        {
            return _nodes.Remove(item);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion ICollection

        public static BlocMacro Wrap(BatchStatementNodeBase statement, bool withIncTab = false)
        {
            BlocMacro macro = new BlocMacro(withIncTab);
            macro.Add(statement);

            return macro;
        }

        public override object Accept(IBatchNodeVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public void AddRange(IEnumerable<BatchStatementNodeBase> nodes)
        {
            foreach (BatchStatementNodeBase node in nodes)
            {
                Add(node);
            }
        }
    }
}