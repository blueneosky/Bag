using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.ArgumentDescription;
using ArgumentsParser.Core;
using ArgumentsParser.Config;

namespace ArgumentsParser.Core
{
    /// <summary>
    /// Parser for command line arguments.
    /// </summary>
    public sealed class StringArgumentsParser : BaseArgumentsParser, IArgumentsParser
    {

        #region Fields

        private IEnumerable<string> _args;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentsParser" /> class.
        /// </summary>
        /// <param name="args">The args.</param>
        internal StringArgumentsParser(IEnumerable<string> args)
        {
            Initialize(args);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentsParser" /> class.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="option">The option.</param>
        /// <param name="context">The context.</param>
        internal StringArgumentsParser(IEnumerable<string> args, IArgumentsParserOption option, IArgumentsParserContext context)
            : base(option, context)
        {
            Initialize(args);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentsParser"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="parser">The parser.</param>
        internal StringArgumentsParser(IEnumerable<string> args, IArgumentsParser parser)
            : base(parser)
        {
            Initialize(args);
        }

        private void Initialize(IEnumerable<string> args)
        {
            _args = args;
        }

        #endregion

        #region Properties

        #endregion

        #region BaseArgumentDescription implementation

        /// <summary>
        /// Gets the args list.
        /// </summary>
        protected override IEnumerable<string> Args
        {
            get
            {
                return _args;
            }
        }

        #endregion

    }
}
