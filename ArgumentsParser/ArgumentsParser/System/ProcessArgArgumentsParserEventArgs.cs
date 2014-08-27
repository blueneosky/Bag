using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// EventArgs for processing an Arg of ArgumentParser.
    /// </summary>
    public sealed class ProcessArgArgumentsParserEventArgs : BaseArgumentsParserEventArgs
    {

        #region Fields

        private string _arg;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessArgArgumentsParserEventArgs" /> class.
        /// </summary>
        /// <param name="arg">The arg.</param>
        public ProcessArgArgumentsParserEventArgs(string arg)
        {
            _arg = arg;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Arg analysed.
        /// </summary>
        public string Arg
        {
            get { return _arg; }
        }


        #endregion
    }
}
