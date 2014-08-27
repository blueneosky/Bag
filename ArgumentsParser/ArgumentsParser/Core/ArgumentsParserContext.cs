using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.Core
{
    /// <summary>
    /// Base implementation of IArgumentsParserContext.
    /// </summary>
    internal sealed class ArgumentsParserContext : IArgumentsParserContext
    {
        #region Fields

        private Dictionary<string, List<object>> _innerContext;
        private List<string> _warnings;

        private object _innerContextLock = new object();
        private ILookup<string, object> _contextCache;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentsParserContext"/> class.
        /// </summary>
        public ArgumentsParserContext()
        {
            Initialise();
        }

        private void Initialise()
        {
            _innerContext = new Dictionary<string, List<object>> { };
            _warnings = new List<string> { };
        }

        #endregion

        #region IArgumentsParserContext implementation

        /// <summary>
        /// Add a new context with value.
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="contextValue"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void AddContext(string contextId, object contextValue)
        {
            InsertContext(contextId, contextValue, true);
        }

        /// <summary>
        /// Add or Set a context. If the contextId exist more once only the first is modified.
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="contextValue"></param>
        public void SetContext(string contextId, object contextValue)
        {
            InsertContext(contextId, contextValue, false);
        }

        /// <summary>
        /// Get the (first) specified context value.
        /// </summary>
        /// <param name="contextId"></param>
        /// <returns></returns>
        public object GetContext(string contextId)
        {
            object result = null;

            lock (_innerContextLock)
            {
                List<object> values = null;
                if (_innerContext.TryGetValue(contextId, out values))
                {
                    result = values.FirstOrDefault();
                }

                _contextCache = null;
            }

            return result;
        }

        /// <summary>
        /// Remove the (first) specified contexte.
        /// </summary>
        /// <param name="contextId"></param>
        /// <returns></returns>
        public object RemoveContext(string contextId)
        {
            object result = null;

            lock (_innerContextLock)
            {
                List<object> values = null;
                if (_innerContext.TryGetValue(contextId, out values))
                {
                    if (values.Any())
                    {
                        result = values[0];
                        values.RemoveAt(0);
                    }
                }

                _contextCache = null;
            }

            return result;
        }

        /// <summary>
        /// Get all contexts.
        /// </summary>
        public ILookup<string, object> Contexts
        {
            get
            {
                ILookup<string, object> result;

                lock (_innerContextLock)
                {
                    if (_contextCache == null)
                    {
                        _contextCache = _innerContext
                            .SelectMany(kvp => kvp.Value.Select(v => new KeyValuePair<string, object>(kvp.Key, v)))
                            .ToLookup(kvp => kvp.Key, kvp => kvp.Value);
                    }
                    result = _contextCache;
                }

                return result;
            }
        }

        /// <summary>
        /// Adds the warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddWarningMessage(string message)
        {
            _warnings.Add(message);
        }

        /// <summary>
        /// Gets the warning messages.
        /// </summary>
        /// <value>
        /// The warning messages.
        /// </value>
        public IEnumerable<string> WarningMessages
        {
            get { return _warnings; }
        }


        #endregion

        #region Functions

        private void InsertContext(string contextId, object contextValue, bool add)
        {
            lock (_innerContextLock)
            {
                List<object> values = null;
                if (false == _innerContext.TryGetValue(contextId, out values))
                {
                    values = new List<object> { };
                    _innerContext[contextId] = values;
                }

                if (add || (values.Count == 0))
                {
                    values.Add(contextValue);
                }
                else
                {
                    values[0] = contextValue;
                }

                _contextCache = null;
            }
        }

        #endregion
    }
}
