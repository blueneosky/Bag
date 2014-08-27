using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.Config;
using ArgumentsParser.Core;

namespace ArgumentsParser.Core
{

    /// <summary>
    /// Used to parse an argument file.
    /// </summary>
    public sealed class FileArgumentsParser : BaseArgumentsParser
    {

        #region Fields

        private string _filePath;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="FileArgumentsParser" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        internal FileArgumentsParser(string filePath)
            : base()
        {
            Initialize(filePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileArgumentsParser" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="option">The option.</param>
        /// <param name="context">The context.</param>
        internal FileArgumentsParser(string filePath, IArgumentsParserOption option, IArgumentsParserContext context)
            : base(option, context)
        {
            Initialize(filePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileArgumentsParser" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="parser">The parser.</param>
        internal FileArgumentsParser(string filePath, IArgumentsParser parser)
            : base(parser)
        {
            Initialize(filePath);
        }

        private void Initialize(string filePath)
        {
            _filePath = filePath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }

        #endregion

        #region BaseArgumentDescription implementation

        /// <summary>
        /// Process the parsing.
        /// </summary>
        /// <returns></returns>
        public override IArgumentsParserContext Parse()
        {
#warning Add open file and close
            IArgumentsParserContext  result = base.Parse();

            return result;
        }

        /// <summary>
        /// Gets the args list.
        /// </summary>
        protected override IEnumerable<string> Args
        {
            get
            {
#warning TODO
                return null;
            }
        }

        #endregion
    }
}
