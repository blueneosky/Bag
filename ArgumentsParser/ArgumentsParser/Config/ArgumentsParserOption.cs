using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArgumentsParser.Config
{
    public class ArgumentsParserOption : IArgumentsParserOption
    {

        #region static

        private static IArgumentsParserOption _defaultOption;

        /// <summary>
        /// Default options
        /// </summary>
        public static IArgumentsParserOption DefaultOption
        {
            get
            {
                if (_defaultOption == null)
                {
                    _defaultOption = new ArgumentsParserOption()
                    {
                        ArgumentSwitchChar = EnumArgumentSwitchChar.WindowsStyle,
                        ArgumentMultiValueDelimiter = EnumArgumentMultiValueDelimiter.All,
                    };
                }
                return _defaultOption;
            }
        }


        private static Dictionary<EnumArgumentSwitchChar, char[]> _argumentSwitchCharsByEnumArgumentSwitchChar = new Dictionary<EnumArgumentSwitchChar, char[]> { };

        /// <summary>
        /// Gets the argument switch chars.
        /// </summary>
        /// <param name="argumentSwitchChar">The argument switch char.</param>
        /// <returns></returns>
        public static char[] GetArgumentSwitchChars(EnumArgumentSwitchChar argumentSwitchChar)
        {
            char[] result;

            bool exist = _argumentSwitchCharsByEnumArgumentSwitchChar.TryGetValue(argumentSwitchChar, out result);

            if (false == exist)
            {
                List<char> chars = new List<char> { };
                if (argumentSwitchChar.HasFlag(EnumArgumentSwitchChar.SlashChar))
                {
                    chars.Add('/');
                }
                if (argumentSwitchChar.HasFlag(EnumArgumentSwitchChar.DashChar))
                {
                    chars.Add('-');
                }
                result = chars.ToArray();
                _argumentSwitchCharsByEnumArgumentSwitchChar[argumentSwitchChar] = result;
            }

            return result;
        }

        private static Dictionary<EnumArgumentMultiValueDelimiter, char[]> _argumentMultiValueDelimiterCharsByEnumArgumentMultiValueDelimiter = new Dictionary<EnumArgumentMultiValueDelimiter, char[]> { };

        /// <summary>
        /// Gets the argument switch chars.
        /// </summary>
        /// <param name="argumentMultiValueDelimiter">The argument switch char.</param>
        /// <returns></returns>
        public static char[] GetArgumentMultiValueDelimiterChars(EnumArgumentMultiValueDelimiter argumentMultiValueDelimiter)
        {
            char[] result;

            bool exist = _argumentMultiValueDelimiterCharsByEnumArgumentMultiValueDelimiter.TryGetValue(argumentMultiValueDelimiter, out result);

            if (false == exist)
            {
                List<char> chars = new List<char> { };
                if (argumentMultiValueDelimiter.HasFlag(EnumArgumentMultiValueDelimiter.EqualsChar))
                {
                    chars.Add('=');
                }
                if (argumentMultiValueDelimiter.HasFlag(EnumArgumentMultiValueDelimiter.ColonChar))
                {
                    chars.Add(':');
                }
                result = chars.ToArray();
                _argumentMultiValueDelimiterCharsByEnumArgumentMultiValueDelimiter[argumentMultiValueDelimiter] = result;
            }

            return result;
        }



        #endregion

        #region Fields

        private EnumArgumentSwitchChar _argumentSwitchChar;
        private EnumArgumentMultiValueDelimiter _argumentMultiValueDelimiter;

        #endregion

        #region IArgumentsParserOption implementation


        /// <summary>
        /// Used to definied the char used for argument préfixe.
        /// </summary>
        public EnumArgumentSwitchChar ArgumentSwitchChar
        {
            get
            {
                return _argumentSwitchChar;
            }
            set
            {
                _argumentSwitchChar = value;
            }
        }

        /// <summary>
        /// Used to definied the char between the argument prefix and the extra argument.
        /// </summary>
        public EnumArgumentMultiValueDelimiter ArgumentMultiValueDelimiter
        {
            get
            {
                return _argumentMultiValueDelimiter;
            }
            set
            {
                _argumentMultiValueDelimiter = value;
            }
        }

        #endregion

    }
}
