using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// Base of all exceptions from Argument Parser
    /// </summary>
    public abstract class BaseArgumentsParserException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParserException"/> class.
        /// </summary>
        internal protected BaseArgumentsParserException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParserException"/> class.
        /// </summary>
        /// <param name="message">Message décrivant l'erreur.</param>
        internal BaseArgumentsParserException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParserException"/> class.
        /// </summary>
        /// <param name="message">Message d'erreur expliquant la raison de l'exception.</param>
        /// <param name="innerException">Exception à l'origine de l'exception en cours ou référence null (Nothing en Visual Basic) si aucune exception interne n'est spécifiée.</param>
        internal BaseArgumentsParserException(string message, Exception innerException) : base(message, innerException) { }

    }
}
