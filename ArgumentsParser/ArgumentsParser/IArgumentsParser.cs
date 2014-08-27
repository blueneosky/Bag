using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.ArgumentDescription;
using ArgumentsParser.Config;
using ArgumentsParser.System;

namespace ArgumentsParser
{
    /// <summary>
    /// Base interface of an Argument Parser.
    /// </summary>
    public interface IArgumentsParser
    {

        #region Properties

        /// <summary>
        /// Contexte filled by the parser.
        /// </summary>
        IArgumentsParserContext Context { get; }

        /// <summary>
        /// Option for the parser.
        /// </summary>
        IArgumentsParserOption Option { get; }

        /// <summary>
        /// ArgumentDescriptions used to parse arguments.
        /// </summary>
        IEnumerable<IArgumentDescription> ArgumentDescriptions { get; }

        /// <summary>
        /// IArgumentDescriptionDefault used to parse defaut argument (without prefixe).
        /// </summary>
        IArgumentDescriptionDefault ArgumentDescriptionDefault { get; }

        #endregion

        #region Initialisation

        /// <summary>
        /// Add a new Argument Description for the parser.
        /// </summary>
        /// <param name="argumentDescription"></param>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        void AddArgumentDescription(IArgumentDescription argumentDescription);

        /// <summary>
        /// Add or set a new Argument Description for the parser.
        /// </summary>
        /// <param name="argumentDescription"></param>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        void SetArgumentDescription(IArgumentDescription argumentDescription);

        /// <summary>
        /// Remove the Argument Description from the parser.
        /// </summary>
        /// <param name="argumentDescription">The argument description.</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        bool RemoveArgumentDescription(IArgumentDescription argumentDescription);

        /// <summary>
        /// Remove the Argument Description from the parser.
        /// </summary>
        /// <param name="argumentDescriptionId">The argument description id.</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        bool RemoveArgumentDescription(string argumentDescriptionId);

        /// <summary>
        /// Clear all Argument Description of the parser (except the default - not in the list)
        /// </summary>
        void ClearArgumentDescriptions();

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [parsing args] (before global parsing).
        /// </summary>
        event EventHandler<ProcessArgsArgumentsParserEventArgs> ParsingArgs;

        /// <summary>
        /// Occurs when [parsed args] (after global parsing).
        /// </summary>
        event EventHandler<ProcessArgsArgumentsParserEventArgs> ParsedArgs;

        /// <summary>
        /// Occurs when [parsing arg] (before parsing arg).
        /// </summary>
        event EventHandler<ProcessArgArgumentsParserEventArgs> ParsingArg;

        /// <summary>
        /// Occurs when [parsed arg] (after parsing arg).
        /// </summary>
        event EventHandler<ProcessArgArgumentsParserEventArgs> ParsedArg;

        #endregion

        #region Functions

        /// <summary>
        /// Process the parsing.
        /// </summary>
        /// <returns></returns>
        IArgumentsParserContext Parse();

        #endregion
    }
}
