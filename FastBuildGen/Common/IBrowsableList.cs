using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Common
{
    public interface IBrowsableList<T> : IEnumerable<T>
    {
        /// <summary>
        /// From CursorPosition to firsst inserted (reverse iteration)
        /// </summary>
        IEnumerable<T> Backs { get; }

        bool CanMoveBack { get; }

        bool CanMoveNext { get; }

        int Count { get; }

        /// <summary>
        /// Index of the next Back element, -1 if any.
        /// </summary>
        int CursorPosition { get; }

        int? MaximumSize { get; set; }

        /// <summary>
        /// From CursorPosition+1 to last inserted
        /// </summary>
        IEnumerable<T> Nexts { get; }

        int? RelativeTokenPosition { get; }

        void Clear();

        T MoveBack();

        T MoveNext();

        T PeekBack();

        T PeekNext();

        void Push(T item);

        void ResetToken();

        void SetToken();
    }
}