using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for argument like /t=10 for a 10s timer.
    /// More than once argument encounter replace the new value (with a warning)
    /// Value is expected.
    /// </summary>
    public sealed class ArgumentDescriptionSingleValue : BaseArgumentDescription
    {

        #region Fields

        private string _value;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionSingleValue"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        public ArgumentDescriptionSingleValue(string id, string keyWord)
            : base(id, keyWord)
        {
            Initialize();

        }

        private void Initialize()
        {
            _value = null;

        }

        #endregion

        #region Propertues

        /// <summary>
        /// The value or null if not defined.
        /// </summary>
        public string Value
        {
            get { return _value; }
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
                _value = extraArgument;
                context.SetContext(Id, _value);

                value = _value;
                success = true;
            }

            return success;
        }

        #endregion

    }
}
