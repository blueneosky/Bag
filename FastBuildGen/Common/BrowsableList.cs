using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public class BrowsableList<T> : IBrowsableList<T>
    {
        #region Members

        private readonly Stack<T> _backStack;
        private readonly Stack<T> _nextStack;

        private bool _canMoveBack;
        private bool _canMoveNext;
        private int _cursorPosition;
        private int? _maximuSize;
        private int? _relativeTokenPosition;

        #endregion Members

        #region ctor

        public BrowsableList()
        {
            _backStack = new Stack<T>();
            _nextStack = new Stack<T>();
            _cursorPosition = -1;
        }

        #endregion ctor

        #region Property

        /// <summary>
        /// From CursorPosition to firsst inserted (reverse iteration)
        /// </summary>
        public IEnumerable<T> Backs
        {
            get { return _backStack; }
        }

        public bool CanMoveBack { get { return _canMoveBack; } }

        public bool CanMoveNext { get { return _canMoveNext; } }

        public int Count
        {
            get
            {
                int result = _cursorPosition + 1 + _nextStack.Count;
                return result;
            }
        }

        /// <summary>
        /// Index of the next Back element, -1 if any.
        /// </summary>
        public int CursorPosition { get { return _cursorPosition; } }

        public int? MaximumSize
        {
            get { return _maximuSize; }
            set
            {
                if (_maximuSize == value)
                    return;

                if (value.HasValue && ((_maximuSize == null) || (value < _maximuSize)))
                {
                    ShrinkBackStack(value.Value);
                }

                _maximuSize = value;
            }
        }

        /// <summary>
        /// From CursorPosition+1 to last inserted
        /// </summary>
        public IEnumerable<T> Nexts
        {
            get { return _nextStack; }
        }

        public int? RelativeTokenPosition { get { return _relativeTokenPosition; } }

        #endregion Property

        #region Methodes

        public void Clear()
        {
            ClearCore();
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> items = _backStack;
            if (_maximuSize.HasValue)
            {
                int backStackCount = _maximuSize.Value;
                int nextStackCount = _nextStack.Count;
                backStackCount -= nextStackCount;
                items = items.Take(backStackCount);
            }
            items = items.Reverse()
                .Concat(_nextStack);

            return items.GetEnumerator();
        }

        public T MoveBack()
        {
            return MoveBackCore();
        }

        public T MoveNext()
        {
            return MoveNextCore();
        }

        public T PeekBack()
        {
            T result = _backStack.Peek();
            return result;
        }

        public T PeekNext()
        {
            T result = _nextStack.Peek();
            return result;
        }

        public void Push(T item)
        {
            PushCore(item);
        }

        public void ResetToken()
        {
            _relativeTokenPosition = null;
        }

        public void SetToken()
        {
            _relativeTokenPosition = 0;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Methodes

        #region Core

        private void ClearCore()
        {
            _nextStack.Clear();
            _canMoveNext = false;
            _backStack.Clear();
            _canMoveBack = false;
            _cursorPosition = -1;
            _relativeTokenPosition = null;
        }

        private T MoveBackCore()
        {
            if (_cursorPosition == -1)
                throw new FastBuildGenException("Move back not permitted");

            T result = _backStack.Pop();
            _nextStack.Push(result);
            _cursorPosition--;
            _relativeTokenPosition++;
            _canMoveBack = _backStack.Count != 0;
            _canMoveNext = true;  // always

            return result;
        }

        private T MoveNextCore()
        {
            if (_nextStack.Count == 0)
                throw new FastBuildGenException("Move back not permitted");

            T result = _nextStack.Pop();
            _backStack.Push(result);
            _cursorPosition++;
            _relativeTokenPosition--;
            _canMoveBack = true; // always
            _canMoveNext = _nextStack.Count != 0;

            return result;
        }

        private void PushCore(T item)
        {
            _nextStack.Clear();
            _canMoveNext = false; // always

            _cursorPosition++;
            if (_maximuSize.HasValue)
            {
                if (_cursorPosition == _maximuSize)
                    _cursorPosition--;

                if (_backStack.Count >= 2 * _maximuSize)
                {
                    ShrinkBackStack(_cursorPosition + 1);
                    //T[] items = _backStack.Take(_cursorPosition + 1).ToArray();
                    //_backStack.Clear();
                    //for (int i = items.Length - 1; i >= 0; i--)
                    //{
                    //    _backStack.Push(items[i]);
                    //}
                    _nextStack.TrimExcess();
                }
            }

            _backStack.Push(item);
            _canMoveBack = true; // always
            _relativeTokenPosition--;
        }

        private void ShrinkBackStack(int desiredSize)
        {
            int nextStackSize = _nextStack.Count;
            if (desiredSize <= nextStackSize)
                throw new FastBuildGenException("Invalide size");

            int desiredBackSize = desiredSize - nextStackSize;

            T[] items = _backStack.Take(desiredBackSize).ToArray();
            _backStack.Clear();
            for (int i = items.Length - 1; i >= 0; i--)
            {
                _backStack.Push(items[i]);
            }
            _cursorPosition = _backStack.Count - 1;
        }

        #endregion Core

        #region DEBUG

        [Conditional("DEBUG")]
        public static void Test()
        {
            StringBuilder builder = new StringBuilder();
            BrowsableList<int?> list = new BrowsableList<int?>();
            list.MaximumSize = 8;
            builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
            for (int i = 0; i < 14; i++)
            {
                if (i == 2)
                {
                    Debug.WriteLine("SetToken");
                    list.SetToken();
                    builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
                }
                else if (i == 10)
                {
                    Debug.WriteLine("Set MaxSize to 4");
                    list.MaximumSize = 4;
                    builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
                }
                list.Push(i);
                builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
            }

            Debug.WriteLine("SetToken");
            list.SetToken();
            var dummy1 = 0;
            for (int i = 0; i < list.MaximumSize; i++)
            {
                list.MoveBack();
                builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
            }
            try
            {
                list.MoveBack();
                Debug.Fail("Bas news");
            }
            catch (Exception) { }

            dummy1 = 0;
            for (int i = 0; i < list.MaximumSize; i++)
            {
                list.MoveNext();
                builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
            }
            try
            {
                list.MoveNext();
                Debug.Fail("Bas news");
            }
            catch (Exception) { }

            Debug.WriteLine("Clear");
            list.Clear();
            builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);

            Debug.WriteLine("shrink with back");
            for (int i = 0; i < 7; i++)
            {
                list.Push(i);
                builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
            }
            list.MoveBack();
            builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
            try
            {
                list.MaximumSize = 1;
                Debug.Fail("Bas news");
            }
            catch (Exception) { }

            Debug.WriteLine("Set Max Size to 2");
            list.MaximumSize = 2;
            builder.Clear(); list.GetMemoryDump(builder); Debug.WriteLine(builder);
        }

        [Conditional("DEBUG")]
        private void GetMemoryDump(StringBuilder builder)
        {
            string l1 = "";
            string l2 = "";
            string l3 = "";
            T def = default(T);

            int sizeBack = _cursorPosition + 1;
            int offset = (_backStack.Count - sizeBack);

            int token = -1;
            if (_relativeTokenPosition.HasValue)
            {
                token = offset + _cursorPosition + _relativeTokenPosition.Value;
            }

            T[] items = _backStack.Reverse().ToArray();
            int pos = 0;
            for (int i = 0; i < items.Length; i++)
            {
                T v = items[i];

                string ll1 = "+---";
                string ll2 = "|   ";
                string ll3 = "+---";

                if (pos == token)
                {
                    ll1 = "+-T-";
                }
                if (pos < offset)
                {
                    if (false == Object.ReferenceEquals(v, def))
                        ll2 = "| ~ ";
                }
                else
                {
                    ll2 = "| # ";
                    if (pos == offset)
                    {
                        ll3 = "@";
                    }
                    else
                    {
                        ll3 = "+";
                    }
                    if (pos == offset + _cursorPosition)
                    {
                        ll3 += "-^-";
                    }
                    else
                    {
                        ll3 += "---";
                    }
                }
                l1 += ll1;
                l2 += ll2;
                l3 += ll3;

                pos++;
            }

            l1 += "+    ";
            l2 += "|    ";
            l3 += "+    ";

            items = _nextStack.ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                T v = items[i];

                string ll1 = "+---";
                string ll2 = "|   ";
                string ll3 = "+---";

                if (pos == token)
                {
                    ll1 = "+-T-";
                }
                if (pos < offset)
                {
                    if (false == Object.ReferenceEquals(v, def))
                        ll2 = "| ~ ";
                }
                else
                {
                    ll2 = "| # ";
                    if (pos == offset)
                    {
                        ll3 = "@";
                    }
                    else
                    {
                        ll3 = "+";
                    }
                    if (pos == offset + _cursorPosition)
                    {
                        ll3 += "-^-";
                    }
                    else
                    {
                        ll3 += "---";
                    }
                }
                l1 += ll1;
                l2 += ll2;
                l3 += ll3;

                pos++;
            }

            l1 += "+";
            l2 += "|";
            l3 += "+";
            builder.AppendLine(l1);
            builder.AppendLine(l2);
            builder.AppendLine(l3);
        }

        #endregion DEBUG
    }
}