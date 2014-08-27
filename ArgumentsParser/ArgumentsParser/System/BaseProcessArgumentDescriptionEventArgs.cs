using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// Contains the base contexte for the processing of an ArgumentDescription.
    /// </summary>
    public abstract class BaseProcessArgumentDescriptionEventArgs<TValue> : BaseArgumentsParserEventArgs
    {

        #region Fields

        private IArgumentsParserContext _context;
        private TValue _value;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessArgumentDescriptionEventArgs"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseProcessArgumentDescriptionEventArgs(IArgumentsParserContext context)
        {
            _context = context;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Current context of the parser.
        /// </summary>
        public IArgumentsParserContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Value of the process.
        /// </summary>
        public TValue Value
        {
            get { return _value; }
            set { _value = value; }
        }

       #endregion

    }
}
