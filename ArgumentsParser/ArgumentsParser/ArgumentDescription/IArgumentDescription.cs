using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.System;

namespace ArgumentsParser.ArgumentDescription
{
    public interface IArgumentDescription
    {

        /// <summary>
        /// Unique ID of the descriptor.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// One line texte used to describe the argument.
        /// </summary>
        string HelpInfo { get; }

        /// <summary>
        /// Keywork used by the parser.
        /// </summary>
        string KeyWord { get; }

        /// <summary>
        /// Occurs when [processing] (first step of Process(...) function).
        /// </summary>
        event EventHandler<ProcessArgumentDescriptionEventArgs> Processing;

        /// <summary>
        /// Occurs when [processed] (last step of Process(...) function).
        /// </summary>
        event EventHandler<ProcessArgumentDescriptionEventArgs> Processed;

        /// <summary>
        /// Process the argument, extraArgument if necessary or used and complete the contexte.
        /// </summary>
        /// <param name="extraArgument"></param>
        /// <param name="context"></param>
        void Process(string extraArgument, IArgumentsParserContext context);

    }
}
