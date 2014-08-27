using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// EventArgs for processing all args of ArgumentParser.
    /// </summary>
    public sealed class ProcessArgsArgumentsParserEventArgs : BaseArgumentsParserEventArgs
    {
        #region Fields

        private IEnumerable<string> _args;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessArgsArgumentsParserEventArgs" /> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public ProcessArgsArgumentsParserEventArgs(IEnumerable<string> args)
        {
            _args = args;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Args analysed.
        /// </summary>
        public IEnumerable<string> Args
        {
            get { return _args; }
        }


        #endregion

    }
}
