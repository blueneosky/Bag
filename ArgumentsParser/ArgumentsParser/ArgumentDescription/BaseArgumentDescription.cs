using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.System;
using ArgumentsParser.Core;

namespace ArgumentsParser.ArgumentDescription
{
    public abstract class BaseArgumentDescription : IArgumentDescription
    {
        #region Fields

        private string _id;
        private string _keyWord;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArgumentDescription"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        internal BaseArgumentDescription(string id, string keyWord)
        {
            _id = id;
            _keyWord = keyWord;
        }

        #endregion

        #region Property

        /// <summary>
        /// Unique ID of the descriptor.
        /// </summary>
        public string Id
        {
            get { return _id; }
        }

        /// <summary>
        /// One line texte used to describe the argument.
        /// </summary>
        public virtual string HelpInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Keywork used by the parser.
        /// </summary>
        public string KeyWord
        {
            get { return _keyWord; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Process the argument, extraArgument if necessary or used and complete the contexte.
        /// </summary>
        /// <param name="extraArgument"></param>
        /// <param name="context"></param>
        public void Process(string extraArgument, IArgumentsParserContext context)
        {
            ProcessArgumentDescriptionEventArgs e = new ProcessArgumentDescriptionEventArgs(context);

            // event before Process
            Tools.DoEventWithTryCathAndLog(OnProcessing, this, e, "Processing", context);

            if (false == e.IsProcessed)
            {
                try
                {
                    // Process
                    object value = null;
                    bool success = ProcessCore(extraArgument, context, out value);

                    if (success)
                    {
                        e.IsProcessed = true;
                        e.Value = value;
                    }
                    else
                    {
                        e.IsProcessed = false;
                        e.Value = null;
                    }

                }
                catch (Exception exception)
                {
                    ProcessArgumentsParserException processArgumentsParserException = new ProcessArgumentsParserException("ProcessCore exception.", exception);
                    throw processArgumentsParserException;
                }
            }

            // event after Process
            Tools.DoEventWithTryCathAndLog(OnProcessed, this, e, "Processed", context);

        }

        /// <summary>
        /// Core processing.
        /// </summary>
        /// <param name="extraArgument">The extra argument.</param>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected abstract bool ProcessCore(string extraArgument, IArgumentsParserContext context, out object value);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [processing] (first step of Process(...) function).
        /// </summary>
        public event EventHandler<ProcessArgumentDescriptionEventArgs> Processing;

        /// <summary>
        /// Occurs when [processed] (last step of Process(...) function).
        /// </summary>
        public event EventHandler<ProcessArgumentDescriptionEventArgs> Processed;

        /// <summary>
        /// Called when [processing] (first step of Process(...) function).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProcessArgumentDescriptionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnProcessing(object sender, ProcessArgumentDescriptionEventArgs e)
        {
            EventHandler<ProcessArgumentDescriptionEventArgs> manager = Processing;
            if (manager != null)
                manager(sender, e);
        }

        /// <summary>
        /// Called when [processed] (last step of Process(...) function).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ProcessArgumentDescriptionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnProcessed(object sender, ProcessArgumentDescriptionEventArgs e)
        {
            EventHandler<ProcessArgumentDescriptionEventArgs> manager = Processed;
            if (manager != null)
                manager(sender, e);
        }



        #endregion

    }
}
