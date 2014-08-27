using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.Core;
using ArgumentsParser.Config;

namespace ArgumentsParser.Test.Helper
{
    internal class BaseArgumentsParserHelper : BaseArgumentsParser
    {

        private IEnumerable<string> _args;
        private Action<string, IArgumentsParserContext> _actionParseCore;

        public BaseArgumentsParserHelper(IEnumerable<string> args, Action<string, IArgumentsParserContext> actionParseCore)
            : base()
        {
            _args = args;
            _actionParseCore = actionParseCore;
        }

        public BaseArgumentsParserHelper(IEnumerable<string> args, Action<string, IArgumentsParserContext> actionParseCore, IArgumentsParserOption option, IArgumentsParserContext context)
            : base(option, context)
        {
            _args = args;
            _actionParseCore = actionParseCore;
        }

        public BaseArgumentsParserHelper(IEnumerable<string> args, Action<string, IArgumentsParserContext> actionParseCore, IArgumentsParser parser)
            : base(parser)
        {
            _args = args;
            _actionParseCore = actionParseCore;
        }

        protected override IEnumerable<string> Args
        {
            get
            {
                return _args;
            }
        }

        protected override void ParseCore(string arg)
        {
            _actionParseCore(arg, Context);
        }
    }
}
