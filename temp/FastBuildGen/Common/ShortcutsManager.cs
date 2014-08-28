using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common;

namespace ImputationH31per.Util
{
    public class ShortcutsManager : IEnumerable<KeyValuePair<Keys, Action>>
    {
        // Implementation in Form
        //
        // ctor()
        // {
        //     _shortcutsManager = new ShortcutsManager()
        //     {
        //         { Keys.Control | Keys.S , _controller.SaveData },
        //         (...)
        //     };
        // }
        //
        // protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        // {
        //     bool traite = _shortcutsManager.ProcessKey(keyData);
        //     if (traite)
        //         return true;
        //
        //     (...)
        //
        //     return base.ProcessCmdKey(ref msg, keyData);
        // }

        private readonly Dictionary<Keys, Action> _actionByShortcuts;
        private readonly Dictionary<Keys, bool> _enabledActionByShortcuts;

        public ShortcutsManager()
            : this((IEnumerable<KeyValuePair<Keys, Action>>)null)
        {
        }

        public ShortcutsManager(IEnumerable<KeyValuePair<Keys, Action>> raccourcis)
        {
            _actionByShortcuts = new Dictionary<Keys, Action>();
            _enabledActionByShortcuts = new Dictionary<Keys, bool>();

            if (raccourcis != null)
            {
                raccourcis.ForEach(Add);
            }
        }

        #region IEnumerable<KeyValuePair<Keys, Action>>

        public IEnumerator<KeyValuePair<Keys, Action>> GetEnumerator()
        {
            return _actionByShortcuts.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable<KeyValuePair<Keys, Action>>

        public void ActivateShortcut(Keys keys)
        {
            SetShortcutEnabled(keys, true);
        }

        public void Add(KeyValuePair<Keys, Action> keysAction)
        {
            Add(keysAction.Key, keysAction.Value);
        }

        public void Add(Keys keys, Action action)
        {
            _actionByShortcuts.Add(keys, action);
            _enabledActionByShortcuts.Add(keys, true);
        }

        public void DeactivateShortcut(Keys keys)
        {
            SetShortcutEnabled(keys, false);
        }

        public bool ProcessKey(Keys keyData)
        {
            bool processed = false;

            bool enabledAction;
            Action action;
            bool success = _enabledActionByShortcuts.TryGetValue(keyData, out enabledAction);

            if (success && enabledAction)
            {
                success = _actionByShortcuts.TryGetValue(keyData, out action);
                Debug.Assert(success);

                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Debug.Fail("Error on shortcut : " + keyData);
                    Trace.WriteLine(e);
                }

                processed = true;
            }

            return processed;
        }

        public void SetShortcutEnabled(Keys keys, bool enable)
        {
            if (_enabledActionByShortcuts.ContainsKey(keys))
            {
                _enabledActionByShortcuts[keys] = true;
            }
        }
    }
}