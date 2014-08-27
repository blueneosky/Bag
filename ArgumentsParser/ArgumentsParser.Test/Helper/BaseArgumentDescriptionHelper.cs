using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.ArgumentDescription;
using ArgumentsParser.System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentsParser.Test.Helper
{
    internal class BaseArgumentDescriptionHelper : BaseArgumentDescription
    {

        private PrivateObject _privateObject;

        private Func<string, IArgumentsParserContext, Tuple<object, bool>> _processCoreFunc;

        public BaseArgumentDescriptionHelper(string id, string keyWord, Func<string, IArgumentsParserContext, Tuple<object, bool>> processCoreFunc)
            : base(id, keyWord)
        {
            _processCoreFunc = processCoreFunc;
            _privateObject = new PrivateObject(this);
        }

        protected override bool ProcessCore(string extraArgument, IArgumentsParserContext context, out object value)
        {
            Tuple<object, bool> result = _processCoreFunc(extraArgument, context);

            value = result.Item1;
            return result.Item2;
        }

        public void OnProcessing(object sender, ProcessArgumentDescriptionEventArgs e)
        {
            _privateObject.Invoke("OnProcessing", sender, e);

        }

        public void OnProcessed(object sender, ProcessArgumentDescriptionEventArgs e)
        {
            _privateObject.Invoke("OnProcessed", sender, e);

        }
    }
}
