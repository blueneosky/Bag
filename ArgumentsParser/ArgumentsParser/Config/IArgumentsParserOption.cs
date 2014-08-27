using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.Config
{
    public interface IArgumentsParserOption
    {
        /// <summary>
        /// Used to definied the char used for argument préfixe.
        /// </summary>
        EnumArgumentSwitchChar ArgumentSwitchChar { get; set; }

        /// <summary>
        /// Used to definied the char between the argument prefix and the extra argument.
        /// </summary>
        EnumArgumentMultiValueDelimiter ArgumentMultiValueDelimiter { get; set; }


    }
}
