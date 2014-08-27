using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// For all exceptions from parser Configuration fonction.
    /// </summary>
    public class ConfigurationArgumentsParserException : BaseArgumentsParserException
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationArgumentsParserException"/> class.
        /// </summary>
        /// <param name="message">Message décrivant l'erreur.</param>
        internal ConfigurationArgumentsParserException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationArgumentsParserException"/> class.
        /// </summary>
        /// <param name="message">Message d'erreur expliquant la raison de l'exception.</param>
        /// <param name="innerException">Exception à l'origine de l'exception en cours ou référence null (Nothing en Visual Basic) si aucune exception interne n'est spécifiée.</param>
        internal ConfigurationArgumentsParserException(string message, Exception innerException) : base(message, innerException) { }

    }
}
