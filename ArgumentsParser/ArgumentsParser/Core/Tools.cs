using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.System;

namespace ArgumentsParser.Core
{
    public static class Tools
    {

        public static void DoEventWithTryCathAndLog<TEventArgs>(Action<object, TEventArgs> doEventFunc, object sender, TEventArgs e, string eventName, IArgumentsParserContext context)
            where TEventArgs : BaseArgumentsParserEventArgs
        {
            try
            {
                // lunch event
                doEventFunc(sender, e);

                if (e.WarningOccurs && context != null)
                {
                    string warningDescription = e.WarningDescription;
                    if (String.IsNullOrEmpty(warningDescription))
                    {
                        context.AddWarningMessage("An unknow warning occurs from " + eventName + " event.");
                    }
                    else
                    {
                        context.AddWarningMessage(e.WarningDescription);
                    }
                    e.WarningOccurs = false;
                    e.WarningDescription = null;
                }
            }
            catch (Exception exception)
            {
                ExternalArgumentsParserException externalArgumentsParserException = new ExternalArgumentsParserException("Exception on " + eventName + " event.", exception);
                throw externalArgumentsParserException;
            }
        }

    }
}
