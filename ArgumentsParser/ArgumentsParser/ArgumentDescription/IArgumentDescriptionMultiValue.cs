using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.ArgumentDescription
{
    public interface IArgumentDescriptionMultiValue : IArgumentDescription
    {

        /// <summary>
        /// Values
        /// </summary>
        IEnumerable<string> Values { get; }

    }
}
