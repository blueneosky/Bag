using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.System
{
    /// <summary>
    /// Contains the contexte for the processing of an ArgumentDescription.
    /// </summary>
    public sealed class ProcessArgumentDescriptionEventArgs : BaseProcessArgumentDescriptionEventArgs<object>
    {

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessArgumentDescriptionEventArgs"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProcessArgumentDescriptionEventArgs(IArgumentsParserContext context)
            : base(context)
        {

        }

        #endregion

    }
}
