using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.Core;
using ArgumentsParser.ArgumentDescription;

namespace ArgumentsParser
{
    /// <summary>
    /// Manager for ArgumentsParser
    /// </summary>
    public static class ArgumentsParserManager
    {

        public const string ConstArgumentDescriptionDefaultId = ConstArgumentDescription.ConstArgumentDescriptionDefaultId;

        /// <summary>
        /// Get an argument parser.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static IArgumentsParser NewArgumentParser(IEnumerable<string> args)
        {
            IArgumentsParser result = new StringArgumentsParser(args);

            return result;
        }
    }
}
