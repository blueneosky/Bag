using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.Config;
using ArgumentsParser.ArgumentDescription;
using ArgumentsParser.System;
using System.Text.RegularExpressions;

namespace ArgumentsParser.Core
{
    public abstract class BaseArgumentsParser : IArgumentsParser
    {

        #region Fields

        private IArgumentsParserContext _context;
        private IArgumentsParserOption _option;
        private Dictionary<string, IArgumentDescription> _argumentDescriptionByIdArgumentDescriptions;
        private Dictionary<string, IArgumentDescription> _argumentDescriptionByKeywords;

        private IArgumentDescriptionDefault _argumentDescriptionDefault;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParser"/> class.
        /// </summary>
        internal protected BaseArgumentsParser()
        {
            Initialize(
                ArgumentsParserOption.DefaultOption
                , new ArgumentsParserContext()
                , new ArgumentDescriptionDefault()
                , Enumerable.Empty<IArgumentDescription>()
            );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParser"/> class.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="context">The context.</param>
        internal protected BaseArgumentsParser(IArgumentsParserOption option, IArgumentsParserContext context)
        {
            Initialize(
                option
                , context
                , new ArgumentDescriptionDefault()
                , Enumerable.Empty<IArgumentDescription>()
            );

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParser"/> class.
        /// </summary>
        /// <param name="parser">The parser.</param>
        internal protected BaseArgumentsParser(IArgumentsParser parser)
        {
            Initialize(
                parser.Option
                , parser.Context
                , parser.ArgumentDescriptionDefault
                , parser.ArgumentDescriptions
            );
        }

        private void Initialize(IArgumentsParserOption option, IArgumentsParserContext context, IArgumentDescriptionDefault argumentDescriptionDefault, IEnumerable<IArgumentDescription> argumentDescriptions)
        {
            _option = option;
            _context = context;
            _argumentDescriptionDefault = argumentDescriptionDefault;

            _argumentDescriptionByIdArgumentDescriptions = new Dictionary<string, IArgumentDescription> { };
            _argumentDescriptionByKeywords = new Dictionary<string, IArgumentDescription> { };
            foreach (IArgumentDescription argumentDescription in argumentDescriptions)
            {
                InsertArgumentDescriptionCore(argumentDescription, true);
            }
        }

        #endregion

        #region IArgumentsParser

        #region Properties

        /// <summary>
        /// Contexte filled by the parser.
        /// </summary>
        public IArgumentsParserContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Option for the parser.
        /// </summary>
        public IArgumentsParserOption Option
        {
            get { return _option; }
        }

        /// <summary>
        /// ArgumentDescriptions used to parse arguments.
        /// </summary>
        public IEnumerable<IArgumentDescription> ArgumentDescriptions
        {
            get { return _argumentDescriptionByIdArgumentDescriptions.Values; }
        }

        #endregion

        #region Initialisation

        /// <summary>
        /// IArgumentDescriptionDefault used to parse defaut argument (without prefixe).
        /// </summary>
        public IArgumentDescriptionDefault ArgumentDescriptionDefault
        {
            get { return _argumentDescriptionDefault; }
        }

        /// <summary>
        /// Add a new Argument Description for the parser.
        /// </summary>
        /// <param name="argumentDescription"></param>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        public void AddArgumentDescription(IArgumentDescription argumentDescription)
        {
            InsertArgumentDescriptionCore(argumentDescription, true);
        }

        /// <summary>
        /// Add or set a new Argument Description for the parser.
        /// </summary>
        /// <param name="argumentDescription"></param>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        public void SetArgumentDescription(IArgumentDescription argumentDescription)
        {
            InsertArgumentDescriptionCore(argumentDescription, false);
        }

        /// <summary>
        /// Remove the Argument Description from the parser.
        /// </summary>
        /// <param name="argumentDescription">The argument description.</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        public bool RemoveArgumentDescription(IArgumentDescription argumentDescription)
        {
            return RemoveArgumentDescriptionCore(argumentDescription);
        }

        /// <summary>
        /// Remove the Argument Description from the parser.
        /// </summary>
        /// <param name="argumentDescriptionId">The argument description id.</param>
        /// <returns></returns>
        /// <exception cref="StringArgumentsParser.System.ConfigurationArgumentsParserException"></exception>
        public bool RemoveArgumentDescription(string argumentDescriptionId)
        {
            return RemoveArgumentDescriptionCore(argumentDescriptionId);
        }

        /// <summary>
        /// Clear all Argument Description of the parser (except the default - not in the list)
        /// </summary>
        public void ClearArgumentDescriptions()
        {
            ClearArgumentDescriptionsCore();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [parsing args] (before global parsing).
        /// </summary>
        public event EventHandler<ProcessArgsArgumentsParserEventArgs> ParsingArgs;

        /// <summary>
        /// Occurs when [parsed args] (after global parsing).
        /// </summary>
        public event EventHandler<ProcessArgsArgumentsParserEventArgs> ParsedArgs;

        /// <summary>
        /// Occurs when [parsing arg] (before parsing arg).
        /// </summary>
        public event EventHandler<ProcessArgArgumentsParserEventArgs> ParsingArg;

        /// <summary>
        /// Occurs when [parsed arg] (after parsing arg).
        /// </summary>
        public event EventHandler<ProcessArgArgumentsParserEventArgs> ParsedArg;

        /// <summary>
        /// Called when [parsing args] (before global parsing).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProcessArgsArgumentsParserEventArgs"/> instance containing the event data.</param>
        protected virtual void OnParsingArgs(object sender, ProcessArgsArgumentsParserEventArgs e)
        {
            EventHandler<ProcessArgsArgumentsParserEventArgs> manager = ParsingArgs;
            if (manager != null)
                manager(sender, e);
        }

        /// <summary>
        /// Called when [parsed args] (after global parsing).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProcessArgsArgumentsParserEventArgs"/> instance containing the event data.</param>
        protected virtual void OnParsedArgs(object sender, ProcessArgsArgumentsParserEventArgs e)
        {
            EventHandler<ProcessArgsArgumentsParserEventArgs> manager = ParsedArgs;
            if (manager != null)
                manager(sender, e);
        }

        /// <summary>
        /// Called when [parsing arg] (before parsing arg).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProcessArgArgumentsParserEventArgs"/> instance containing the event data.</param>
        protected virtual void OnParsingArg(object sender, ProcessArgArgumentsParserEventArgs e)
        {
            EventHandler<ProcessArgArgumentsParserEventArgs> manager = ParsingArg;
            if (manager != null)
                manager(sender, e);
        }

        /// <summary>
        /// Called when [parsed arg] (after parsing arg).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProcessArgArgumentsParserEventArgs"/> instance containing the event data.</param>
        protected virtual void OnParsedArg(object sender, ProcessArgArgumentsParserEventArgs e)
        {
            EventHandler<ProcessArgArgumentsParserEventArgs> manager = ParsedArg;
            if (manager != null)
                manager(sender, e);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Process the parsing.
        /// </summary>
        /// <returns></returns>
        public virtual IArgumentsParserContext Parse()
        {
            bool success = false;

            try
            {
                IEnumerable<string> args = this.Args;
                ParseCore(args);
                success = true;
            }
            catch (BaseArgumentsParserException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                ExternalArgumentsParserException externalArgumentsParserException = new ExternalArgumentsParserException("Exception during Parse() function.", exception);
                throw externalArgumentsParserException;
            }

            IArgumentsParserContext result = (success) ? _context : null;
            return result;
        }

        #endregion

        #endregion

        #region Parsing function

        /// <summary>
        /// Analyse arguments and call pre and post events.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void ParseCore(IEnumerable<string> args)
        {
            ProcessArgsArgumentsParserEventArgs e = new ProcessArgsArgumentsParserEventArgs(args);

            // event before Parsing
            Tools.DoEventWithTryCathAndLog(OnParsingArgs, this, e, "ParsingArgs", _context);

            if (false == e.IsProcessed)
            {
                foreach (string arg in args)
                {
                    Parse(arg);
                }
                e.IsProcessed = true;
            }

            // event after Parsing
            Tools.DoEventWithTryCathAndLog(OnParsedArgs, this, e, "ParsedArgs", _context);

        }

        /// <summary>
        /// Analyse the argument and call pre and post events.
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void Parse(string arg)
        {
            ProcessArgArgumentsParserEventArgs e = new ProcessArgArgumentsParserEventArgs(arg);

            // event before Parsing
            Tools.DoEventWithTryCathAndLog(OnParsingArg, this, e, "ParsingArg", _context);

            if (false == e.IsProcessed)
            {
                ParseCore(arg);
                e.IsProcessed = true;
            }

            // event after Parsing
            Tools.DoEventWithTryCathAndLog(OnParsedArg, this, e, "ParsedArg", _context);

        }

        /// <summary>
        /// Analyse the argument.
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void ParseCore(string arg)
        {
            char[] switchChars = ArgumentsParserOption.GetArgumentSwitchChars(Option.ArgumentSwitchChar);
            bool isSwitchArgument = false;
            foreach (char c in switchChars)
            {
                isSwitchArgument = arg.StartsWith("" + c, StringComparison.Ordinal);
                if (isSwitchArgument)
                    break;
            }

            if (isSwitchArgument)
            {
                arg = arg.Substring(1);
                char[] delimiterChars = ArgumentsParserOption.GetArgumentMultiValueDelimiterChars(Option.ArgumentMultiValueDelimiter);
                string[] values = arg.Split(delimiterChars, 2);
                string keyword = values.Length > 0 ? values[0] : null;
                string extraArgs = values.Length > 1 ? values[1] : null;

                IArgumentDescription argumentDescription;
                bool exist = _argumentDescriptionByKeywords.TryGetValue(keyword, out argumentDescription);
                if (exist)
                {
                    argumentDescription.Process(extraArgs, _context);
                }
                else
                {
                    _context.AddWarningMessage("'" + keyword + "' is not a valide keyword switch option. Argument ignored.");
                }
            }
            else
            {
                ArgumentDescriptionDefault.Process(arg, _context);
            }
        }

        /// <summary>
        /// Gets the args list.
        /// </summary>
        protected abstract IEnumerable<string> Args { get; }


        #endregion

        #region Functions

        private void InsertArgumentDescriptionCore(IArgumentDescription argumentDescription, bool add)
        {
            // check IArgumentDescription
            if (argumentDescription == null)
                throw new ConfigurationArgumentsParserException("Null parameter not allowed.");

            if (argumentDescription is IArgumentDescriptionDefault)
                throw new ConfigurationArgumentsParserException("IArgumentDescriptionDefault not accepted for ArgumentDescriptions list.");

            string id = argumentDescription.Id;
            if (id == ConstArgumentDescription.ConstArgumentDescriptionDefaultId)
                throw new ConfigurationArgumentsParserException("Id of ArgumentDescription reserved for Default argument parser.");

            if (String.IsNullOrWhiteSpace(id))
                throw new ConfigurationArgumentsParserException("Id of ArgumentDescription must be defined.");

            string keyword = argumentDescription.KeyWord;
            if (String.IsNullOrWhiteSpace(keyword))
                throw new ConfigurationArgumentsParserException("KeyWord of ArgumentDescription must be defined.");

            if (add)
            {
                // check IArgumentDescription for insert
                if (_argumentDescriptionByIdArgumentDescriptions.ContainsKey(id))
                    throw new ConfigurationArgumentsParserException("Id of ArgumentDescription already added.");

                bool isKeyWorkAlreadyUsed = _argumentDescriptionByIdArgumentDescriptions.Values
                    .Where(ad => String.CompareOrdinal(ad.KeyWord, keyword) == 0)
                    .Any();
                if (isKeyWorkAlreadyUsed)
                    throw new ConfigurationArgumentsParserException("Keyword of ArgumentDescription already added.");
            }
            else
            {
                bool isKeyWorkAlreadyUsed = _argumentDescriptionByIdArgumentDescriptions.Values
                    .Where(ad => String.CompareOrdinal(ad.KeyWord, keyword) == 0)
                    .Where(ad => String.CompareOrdinal(ad.Id, id) != 0)
                    .Any();
                if (isKeyWorkAlreadyUsed)
                    throw new ConfigurationArgumentsParserException("Keyword of ArgumentDescription already used by an other ArgumentDescription.");
            }

            _argumentDescriptionByIdArgumentDescriptions[id] = argumentDescription;
            _argumentDescriptionByKeywords[keyword] = argumentDescription;
        }

        private bool RemoveArgumentDescriptionCore(IArgumentDescription argumentDescription)
        {
            // check IArgumentDescription
            if (argumentDescription == null)
                throw new ConfigurationArgumentsParserException("Null parameter not allowed.");

            string id = argumentDescription.Id;
            if (String.IsNullOrWhiteSpace(id))
                throw new ConfigurationArgumentsParserException("Id of ArgumentDescription must be defined.");

            bool success = RemoveArgumentDescriptionCore(id);

            return success;
        }

        private bool RemoveArgumentDescriptionCore(string id)
        {
            // check id
            if (String.IsNullOrWhiteSpace(id))
                throw new ConfigurationArgumentsParserException("Null parameter not allowed.");

            IArgumentDescription argumentDescription;
            if (_argumentDescriptionByIdArgumentDescriptions.TryGetValue(id, out argumentDescription))
            {
                _argumentDescriptionByKeywords.Remove(argumentDescription.KeyWord);
            }
            bool success = _argumentDescriptionByIdArgumentDescriptions.Remove(id);

            return success;
        }

        private void ClearArgumentDescriptionsCore()
        {
            _argumentDescriptionByIdArgumentDescriptions.Clear();
            _argumentDescriptionByKeywords.Clear();
        }

        #endregion
    }
}
