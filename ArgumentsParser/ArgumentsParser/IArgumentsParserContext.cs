using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser
{
    public interface IArgumentsParserContext
    {

        /// <summary>
        /// Add a new context with value.
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="contextValue"></param>
        void AddContext(string contextId, object contextValue);

        /// <summary>
        /// Add or Set a context. If the contextId exist more once only the first is modified.
        /// </summary>
        /// <param name="contextId"></param>
        /// <param name="contextValue"></param>
        void SetContext(string contextId, object contextValue);

        /// <summary>
        /// Get the (first) specified context value.
        /// </summary>
        /// <param name="contextId"></param>
        /// <returns></returns>
        object GetContext(string contextId);

        /// <summary>
        /// Remove the (first) specified contexte.
        /// </summary>
        /// <param name="contextId"></param>
        /// <returns></returns>
        object RemoveContext(string contextId);

        /// <summary>
        /// Get all contexts.
        /// </summary>
        ILookup<string, object> Contexts { get; }

        /// <summary>
        /// Add warning message.
        /// </summary>
        /// <param name="message"></param>
        void AddWarningMessage(string message);

        /// <summary>
        /// All Warning messages.
        /// </summary>
        IEnumerable<string> WarningMessages { get; }

    }
}
