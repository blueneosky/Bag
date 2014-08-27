using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArgumentsParser.ArgumentDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentsParser.Test.Helper
{
    public class ArgumentDescriptionFileArgument_Accessor
    {

        private PrivateObject _privateObject;
        private ArgumentDescriptionFileArgument _argumentDescriptionFileArgument;

        public ArgumentDescriptionFileArgument_Accessor(ArgumentDescriptionFileArgument argumentDescriptionFileArgument)
        {
            _argumentDescriptionFileArgument = argumentDescriptionFileArgument;
            _privateObject = new PrivateObject(argumentDescriptionFileArgument);

        }

        internal bool FileProcessCore(string filePath, ArgumentsParser.Core.FileArgumentsParser parser, IArgumentsParserContext context, out object value)
        {
            throw new NotImplementedException();
        }

        internal void OnProcessedFile(object sender, System.ProcessArgumentDescriptionFileArgumentEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void OnProcessingFile(object sender, System.ProcessArgumentDescriptionFileArgumentEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal bool ProcessCore(string extraArgument, IArgumentsParserContext context, out object value)
        {
            throw new NotImplementedException();
        }
    }
}
