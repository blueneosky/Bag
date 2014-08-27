using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ArgumentsParser.Config;
using ArgumentsParser.Core;
using ArgumentsParser.System;

namespace ArgumentsParser.ArgumentDescription
{
    /// <summary>
    /// Description for argument pointing to an arguments file.
    /// </summary>
    public sealed class ArgumentDescriptionFileArgument : BaseArgumentDescription, IArgumentDescriptionFileArgument
    {
        #region Fields

        private List<Tuple<string, IArgumentsParserContext>> _values;

        private IArgumentsParserOption _option;
        private IArgumentsParser _parser;
        private IEnumerable<IArgumentDescription> _argumentDescriptions;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionFileArgument"/> class.
        /// An ArgumentsParser will be created for each file parameters analysed. The parser will use Option, Context and ArgumentDescriptions of the class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        public ArgumentDescriptionFileArgument(string id, string keyWord)
            : base(id, keyWord)
        {
            Initialized(null, null, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionFileArgument" /> class.
        /// An ArgumentsParser will be created for each file parameters analysed. The parser will use Option, Context and ArgumentDescriptions of the class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        /// <param name="option">The option.</param>
        /// <param name="argumentDescriptions">The argument descriptions.</param>
        public ArgumentDescriptionFileArgument(string id, string keyWord, IArgumentsParserOption option, IEnumerable<IArgumentDescription> argumentDescriptions)
            : base(id, keyWord)
        {
            Initialized(null, option, argumentDescriptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentDescriptionFileArgument" /> class.
        /// The parser wil be used for each file parameters analysed.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="keyWord">The key word.</param>
        /// <param name="parser">The parser.</param>
        public ArgumentDescriptionFileArgument(string id, string keyWord, IArgumentsParser parser)
            : base(id, keyWord)
        {
            Initialized(parser, null, null);
        }

        private void Initialized(IArgumentsParser parser, IArgumentsParserOption option, IEnumerable<IArgumentDescription> argumentDescriptions)
        {
            if (parser != null)
            {
                _parser = parser;
                _option = null;
                _argumentDescriptions = null;
            }
            else
            {
                _option = option;
                _argumentDescriptions = argumentDescriptions;

                if (_option == null)
                    _option = ArgumentsParserOption.DefaultOption;
                if (_argumentDescriptions == null)
                    _argumentDescriptions = Enumerable.Empty<IArgumentDescription>();
            }

        }

        #endregion

        #region Property

        /// <summary>
        /// Get all contexts by filenamme.
        /// All contexte can be the same if this class has been initialised with a IArgumentsParser.
        /// Note : in this case, IArgumentParser contains the fullfilled context.
        /// </summary>
        public IEnumerable<Tuple<string, IArgumentsParserContext>> Values
        {
            get { return _values; }
        }

        /// <summary>
        /// Get the parser used to parse the file argument, if specified.
        /// </summary>
        public IArgumentsParser Parser
        {
            get { return _parser; }
        }

        /// <summary>
        /// Get the parser's option (if specified), or the current option.
        /// </summary>
        public IArgumentsParserOption Option
        {
            get
            {
                IArgumentsParserOption option = (_parser != null) ? _parser.Option : _option;
                return option;
            }
        }

        /// <summary>
        /// Get the argument descriptions list of the parser (if specified), or the current description list.
        /// </summary>
        public IEnumerable<IArgumentDescription> ArgumentDescription
        {
            get
            {
                IEnumerable<IArgumentDescription> argumentDescriptions = (_parser != null) ? _parser.ArgumentDescriptions : _argumentDescriptions;
                return argumentDescriptions;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [processing file] (before parsing file).
        /// </summary>
        public event EventHandler<ProcessArgumentDescriptionFileArgumentEventArgs> ProcessingFile;

        /// <summary>
        /// Occurs when [processed file] (after parsing file).
        /// </summary>
        public event EventHandler<ProcessArgumentDescriptionFileArgumentEventArgs> ProcessedFile;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProcessingFile(object sender, ProcessArgumentDescriptionFileArgumentEventArgs e)
        {
            EventHandler<ProcessArgumentDescriptionFileArgumentEventArgs> manager = ProcessingFile;
            if (manager != null)
                manager(sender, e);
        }

        private void OnProcessedFile(object sender, ProcessArgumentDescriptionFileArgumentEventArgs e)
        {
            EventHandler<ProcessArgumentDescriptionFileArgumentEventArgs> manager = ProcessedFile;
            if (manager != null)
                manager(sender, e);
        }

        #endregion

        #region ArgumentDescriptionAbstract implementation

        /// <summary>
        /// Core processing.
        /// </summary>
        /// <param name="extraArgument">The extra argument.</param>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected override bool ProcessCore(string extraArgument, IArgumentsParserContext context, out object value)
        {
            bool success = false;
            value = null;

            string filePath = extraArgument;

            if (false == File.Exists(filePath))
            {
                context.AddWarningMessage("Argument file not found :'" + filePath + "'.");
            }
            else
            {
                IArgumentsParserContext fileContext;
                FileArgumentsParser parser;
                if (_parser != null)
                {
                    parser = new FileArgumentsParser(filePath, _parser);
                    fileContext = parser.Context;
                }
                else
                {
                    fileContext = new ArgumentsParserContext();
                    parser = new FileArgumentsParser(filePath, _option, fileContext);
                    foreach (IArgumentDescription argumentDescription in _argumentDescriptions)
                    {
                        parser.AddArgumentDescription(argumentDescription);
                    }
                }

                FileProcessCore(filePath, parser, context, out value);
            }

            return success;
        }

        private bool FileProcessCore(string filePath, FileArgumentsParser parser, IArgumentsParserContext context, out object value)
        {
            bool success = false;

            ProcessArgumentDescriptionFileArgumentEventArgs e = new ProcessArgumentDescriptionFileArgumentEventArgs(filePath, parser);

            // event before Process
            Tools.DoEventWithTryCathAndLog(OnProcessingFile, this, e, "ProcessingFile", context);

            if (e.IsProcessed)
            {
                Tuple<string, IArgumentsParserContext> tuple = e.Value;
                value = tuple;

                _values.Add(tuple);
                context.AddContext(Id, tuple);

                value = tuple;
                success = true;
            }
            else
            {
                parser.Parse();

                Tuple<string, IArgumentsParserContext> tuple = new Tuple<string, IArgumentsParserContext>(filePath, context);
                _values.Add(tuple);
                context.AddContext(Id, tuple);

                value = tuple;
                success = true;
            }

            // event after Process
            Tools.DoEventWithTryCathAndLog(OnProcessedFile, this, e, "ProcessedFile", context);

            return success;
        }

        #endregion

    }
}
