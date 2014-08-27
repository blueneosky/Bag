using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.Config;
using ArgumentsParser.System;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for argument pointing to an arguments file.
    /// </summary>
    public interface IArgumentDescriptionFileArgument
    {

        /// <summary>
        /// Get all contexts by filenamme.
        /// All contexte can be the same if this class has been initialised with a IArgumentsParser.
        /// Note : in this case, IArgumentParser contains the fullfilled context.
        /// </summary>
        IEnumerable<Tuple<string, IArgumentsParserContext>> Values { get; }

        /// <summary>
        /// Get the parser used to parse the file argument, if specified.
        /// </summary>
        IArgumentsParser Parser { get; }

        /// <summary>
        /// Get the parser's option (if specified), or the current option.
        /// </summary>
        IArgumentsParserOption Option { get; }

        /// <summary>
        /// Get the argument descriptions list of the parser (if specified), or the current description list.
        /// </summary>
        IEnumerable<IArgumentDescription> ArgumentDescription { get; }

        /// <summary>
        /// Occurs when [processing file] (before parsing file).
        /// </summary>
        event EventHandler<ProcessArgumentDescriptionFileArgumentEventArgs> ProcessingFile;

        /// <summary>
        /// Occurs when [processed file] (after parsing file).
        /// </summary>
        event EventHandler<ProcessArgumentDescriptionFileArgumentEventArgs> ProcessedFile;
    }
}
