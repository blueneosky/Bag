using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for argument like /ig:file1 /ig:file2 for ignored file1 and file2.
    /// Value is expected.
    /// </summary>
    public sealed class ArgumentDescriptionMultiValue : BaseArgumentDescriptionMultiValue
    {

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionMultiValue"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        public ArgumentDescriptionMultiValue(string id, string keyWord)
            : base(id, keyWord)
        {

        }

        #endregion

    }
}
