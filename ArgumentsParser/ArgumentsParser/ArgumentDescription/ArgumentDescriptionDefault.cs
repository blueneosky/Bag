using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for all values and unrecognized arguments.
    /// </summary>
    internal sealed class ArgumentDescriptionDefault : BaseArgumentDescriptionMultiValue, IArgumentDescriptionDefault
    {

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionDefault"/> class.
        /// </summary>
        internal ArgumentDescriptionDefault()
            : base(ConstArgumentDescription.ConstArgumentDescriptionDefaultId, null)
        {

        }

        #endregion

    }
}
