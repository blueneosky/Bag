using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for argument like /q for quiet.
    /// Can also be used like /v /v /v for very verbose (counter = 3).
    /// </summary>
    public sealed class ArgumentDescriptionSwitch : BaseArgumentDescription
    {

        #region Fields

        private int _activateCounter;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionSwitch" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        public ArgumentDescriptionSwitch(string id, string keyWord)
            : base(id, keyWord)
        {
            Initialize();

        }

        private void Initialize()
        {
            _activateCounter = 0;

        }

        #endregion

        #region Properties

        /// <summary>
        /// The switch is activated
        /// </summary>
        public bool IsActivated
        {
            get { return _activateCounter > 0; }
        }

        /// <summary>
        /// The number of activation of the switch.
        /// </summary>
        public int ActivateCounter
        {
            get { return _activateCounter; }
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

            if (false == String.IsNullOrEmpty(extraArgument))
            {
                // no extra arg for swith arg => warning
                context.AddWarningMessage("No extra value expected for " + KeyWord + ". Value '" + extraArgument + "' ignored"); ;
            }

            _activateCounter++;
            context.SetContext(Id, _activateCounter);

            value = _activateCounter;
            success = true;

            return success;
        }

        #endregion

    }
}
