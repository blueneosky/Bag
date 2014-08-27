using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImputationH31per.Util
{
    public class GestionnaireRaccourcis : IEnumerable<KeyValuePair<Keys, Action>>
    {
        private readonly Dictionary<Keys, bool> _actionActiveeParRaccourcis;
        private readonly Dictionary<Keys, Action> _actionParRaccourcis;

        public GestionnaireRaccourcis()
            : this((IEnumerable<KeyValuePair<Keys, Action>>)null)
        {
        }

        public GestionnaireRaccourcis(IEnumerable<KeyValuePair<Keys, Action>> raccourcis)
        {
            _actionParRaccourcis = new Dictionary<Keys, Action>();
            _actionActiveeParRaccourcis = new Dictionary<Keys, bool>();

            if (raccourcis != null)
            {
                raccourcis.ForEach(Add);
            }
        }

        #region IEnumerable<KeyValuePair<Keys, Action>>

        public IEnumerator<KeyValuePair<Keys, Action>> GetEnumerator()
        {
            return _actionParRaccourcis.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable<KeyValuePair<Keys, Action>>

        public void ActiverRaccourci(Keys keys)
        {
            DefinirActivationRaccourci(keys, true);
        }

        public void Add(KeyValuePair<Keys, Action> keysAction)
        {
            Add(keysAction.Key, keysAction.Value);
        }

        public void Add(Keys keys, Action action)
        {
            _actionParRaccourcis.Add(keys, action);
            _actionActiveeParRaccourcis.Add(keys, true);
        }

        public void DefinirActivationRaccourci(Keys keys, bool active)
        {
            if (_actionActiveeParRaccourcis.ContainsKey(keys))
            {
                _actionActiveeParRaccourcis[keys] = true;
            }
        }

        public void DesactiverRaccourci(Keys keys)
        {
            DefinirActivationRaccourci(keys, false);
        }

        public bool ProcessKey(Keys keyData)
        {
            bool traite = false;

            bool actionActivee;
            Action action;
            bool succes = _actionActiveeParRaccourcis.TryGetValue(keyData, out actionActivee);

            if (succes && actionActivee)
            {
                succes = _actionParRaccourcis.TryGetValue(keyData, out action);
                Debug.Assert(succes);

                try
                {
                    action();
                }
                catch (Exception e)
                {
                    Debug.Fail("Erreur sur raccourci : " + keyData);
                    Trace.WriteLine(e);
                }

                traite = true;
            }

            return traite;
        }
    }
}