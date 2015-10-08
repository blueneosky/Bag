using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gitfs.Util
{
    internal static class Output
    {
        public static void Write(string text)
        {
            Console.Write(text);
            Debug.Write(text);
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
            Debug.WriteLine(text);
        }

        static int? _cursorTop;
        static int? _cursorLeft;
        static int? _lastMaxTextLength;

        public static void InitiateCursorPosition(string text)
        {
            Write(text);
            _cursorLeft = Console.CursorLeft;
            _cursorTop = Console.CursorTop;
            _lastMaxTextLength = 0;
        }

        public static void WriteAtCursor(string text)
        {
            WriteAtCursor(text, false);
        }

        public static void LastWriteAtCursor(string text)
        {
            WriteAtCursor(text, true);
        }

        private static void WriteAtCursor(string text, bool lastWrite)
        {
            Console.SetCursorPosition(_cursorLeft.Value, _cursorTop.Value);
            Console.Write(text);
            int textLength = text.Length;
            if (textLength > _lastMaxTextLength.Value)
            {
                _lastMaxTextLength = textLength;
            }
            else
            {
                Output.Write(new String(' ', _lastMaxTextLength.Value - textLength));
            }
            if (lastWrite)
            {
                Debug.Write(text);
                _cursorTop = null;
                _cursorLeft = null;
                _lastMaxTextLength = null;
            }
        }

    }
}