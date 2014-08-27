using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// Contains the contexte for the processing of an ArgumentDescriptionFileArgument.
    /// </summary>
    public sealed class ProcessArgumentDescriptionFileArgumentEventArgs : BaseProcessArgumentDescriptionEventArgs<Tuple<string, IArgumentsParserContext>>
    {

        #region Fields

        private string _filePath;
        private IArgumentsParser _parser;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessArgumentDescriptionEventArgs" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="parser">The parser.</param>
        public ProcessArgumentDescriptionFileArgumentEventArgs(string filePath, IArgumentsParser parser)
            : base(parser.Context)
        {
            _filePath = filePath;
            _parser = parser;

        }

        #endregion

        #region Properties

        /// <summary>
        /// The file path of the arguments file.
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
        }

        /// <summary>
        /// The parser used to parse the file.
        /// </summary>
        public IArgumentsParser Parser
        {
            get { return _parser; }
        }

        #endregion


    }
}
