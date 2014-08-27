using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// Base implementation of all EventArgs of ArgumentsParser.
    /// </summary>
    public abstract class BaseArgumentsParserEventArgs : EventArgs
    {

        #region Fields

        private bool _isProcessed;
        private bool _warningOccurs;
        private string _warningDescription;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentsParserEventArgs"/> class.
        /// </summary>
        protected BaseArgumentsParserEventArgs()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// State of the processing.
        /// </summary>
        public bool IsProcessed
        {
            get { return _isProcessed; }
            set { _isProcessed = value; }
        }

        /// <summary>
        /// <c>true if warning occurs</c>
        /// </summary>
        public bool WarningOccurs
        {
            get { return _warningOccurs; }
            set { _warningOccurs = value; }
        }

        /// <summary>
        /// Description for the warning.
        /// </summary>
        public string WarningDescription
        {
            get { return _warningDescription; }
            set { _warningDescription = value; }
        }

        #endregion

    }
}
