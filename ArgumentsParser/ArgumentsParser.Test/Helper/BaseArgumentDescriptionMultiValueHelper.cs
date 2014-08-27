using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.ArgumentDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentsParser.Test.Helper
{
    internal class BaseArgumentDescriptionMultiValueHelper : BaseArgumentDescriptionMultiValue
    {

        private PrivateObject _privateObject;

        public BaseArgumentDescriptionMultiValueHelper(string id, string keyWord)
            : base(id, keyWord)
        {
            _privateObject = new PrivateObject(this);

        }



        internal bool ProcessCore(string extraArgument, IArgumentsParserContext context, out object value)
        {
            value = null;
            object[] args = new object[]{extraArgument, context, value};
            object result = _privateObject.Invoke("ProcessCore", args);

#warning TODO : Need tests
            value = args[2];
            bool success = (bool)result;

            return success;
        }

        public List<string> ValuesListHelper
        {
            get
            {
                object result = _privateObject.GetProperty("ValuesListHelper");
                List<string> value = result as List<string>;

                return value;
            }
        }
    }
}
