using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for multiple argument base system.
    /// Value is expected.
    /// </summary>
    public abstract class BaseArgumentDescriptionMultiValue : BaseArgumentDescription, IArgumentDescriptionMultiValue
    {
        #region Fields

        private List<string> _values;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionMultiValue"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        internal BaseArgumentDescriptionMultiValue(string id, string keyWord)
            : base(id, keyWord)
        {
            Initialize();

        }

        private void Initialize()
        {
            _values = new List<string> { };

        }

        #endregion

        #region Properties

        /// <summary>
        /// List of values.
        /// </summary>
        protected List<string> ValuesList
        {
            get { return _values; }
        }

        #endregion

        #region IArgumentDescriptionMultiValue implementation

        /// <summary>
        /// Values
        /// </summary>
        public IEnumerable<string> Values
        {
            get { return _values; }
        }

        #endregion

        #region ArgumentDescriptionAbstract implementation

        /// <summary>
        /// Core processing.
        /// </summary>
        /// <param name="extraArgument">The extra argument.</param>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected override bool ProcessCore(string extraArgument, IArgumentsParserContext context, out object value)
        {
            bool success = false;
            value = null;

            if (String.IsNullOrEmpty(extraArgument))
            {
                // extra arg needed => warning
                context.AddWarningMessage("Extra value expected for " + KeyWord + ". Argument ignored");
            }
            else
            {
                _values.Add(extraArgument);
                context.AddContext(Id, extraArgument);

                value = _values;
                success = true;
            }

            return success;
        }

        #endregion
    }
}
