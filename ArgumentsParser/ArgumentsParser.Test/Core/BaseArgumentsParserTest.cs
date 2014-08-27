using ArgumentsParser.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArgumentsParser.ArgumentDescription;
using ArgumentsParser.Config;
using ArgumentsParser;
using System.Collections.Generic;
using ArgumentsParser.System;
using System.Linq;
using ArgumentsParser.Test.Helper;

namespace ArgumentsParser.Test.Core
{


    /// <summary>
    ///Classe de test pour BaseArgumentsParserTest, destinée à contenir tous
    ///les tests unitaires BaseArgumentsParserTest
    ///</summary>
    [TestClass()]
    public class BaseArgumentsParserTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private const string ConstLocalId = "local_id";
        private static object ConstLocalObject = new object();

        private const string ConstId1 = "Id1";
        private const string ConstId2 = "Id2";

        private const string ConstKW1 = "kw1";
        private const string ConstKW2 = "kw2";

        private const string ConstArg1 = "arg1";
        private const string ConstArg2 = "arg2";
        private const string ConstArgKW1 = "/" + ConstKW1 + ":argKW1";
        private const string ConstArgKW2 = "/" + ConstKW2 + ":argKW2";

        private static IEnumerable<string> ConstArgs = new[] {
            ConstArg1
            , ConstArg2
            , ConstArgKW1
            , ConstArgKW2
        };

        private const string ConstWarningMessage = "my warning";

        private BaseArgumentsParser CreateBaseArgumentsParser(IEnumerable<string> args, Action<string, IArgumentsParserContext> actionParseCore)
        {
            return new BaseArgumentsParserHelper(args, actionParseCore);
        }

        private BaseArgumentsParser CreateBaseArgumentsParser(IEnumerable<string> args, Action<string, IArgumentsParserContext> actionParseCore, IArgumentsParserOption option, IArgumentsParserContext context)
        {
            return new BaseArgumentsParserHelper(args, actionParseCore, option, context);
        }

        private BaseArgumentsParser CreateBaseArgumentsParser(IEnumerable<string> args, Action<string, IArgumentsParserContext> actionParseCore, IArgumentsParser parser)
        {
            return new BaseArgumentsParserHelper(args, actionParseCore, parser);
        }

        /// <summary>
        /// Test pour Ctor
        /// </summary>
        [TestMethod()]
        public void BaseArgumentsParserCtorTest()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null);
            Assert.IsNotNull(target.Context);
            Assert.AreSame(target.Option, ArgumentsParserOption.DefaultOption);
            Assert.IsNotNull(target.ArgumentDescriptionDefault);
            Assert.IsNotNull(target.ArgumentDescriptions);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 0);
        }

        /// <summary>
        /// Test pour Ctor
        /// </summary>
        [TestMethod()]
        public void BaseArgumentsParserCtorTest1()
        {
            IArgumentsParserContext expectedContext = new ArgumentsParserContext();
            IArgumentsParserOption expectedOption = new ArgumentsParserOption();
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null, expectedOption, expectedContext);
            IArgumentsParserContext actualContext = target.Context;
            IArgumentsParserOption actualOption = target.Option;
            Assert.AreSame(expectedContext, actualContext);
            Assert.AreSame(expectedOption, actualOption);
            Assert.IsNotNull(target.ArgumentDescriptionDefault);
            Assert.IsNotNull(target.ArgumentDescriptions);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 0);

        }

        /// <summary>
        /// Test pour Ctor
        /// </summary>
        [TestMethod()]
        public void BaseArgumentsParserCtorTest2()
        {
            BaseArgumentsParser parser = CreateBaseArgumentsParser(null, null);
            parser.AddArgumentDescription(new ArgumentDescriptionSwitch(ConstId1, ConstKW1));

            IArgumentsParserContext expectedContext = parser.Context;
            IArgumentsParserOption expectedOption = parser.Option;
            IEnumerable<IArgumentDescription> expectedArgumentDescriptions = parser.ArgumentDescriptions;
            IArgumentDescriptionDefault expectedArgumentDescriptionDefault = parser.ArgumentDescriptionDefault;
            Assert.IsNotNull(expectedArgumentDescriptionDefault);
            Assert.IsNotNull(expectedArgumentDescriptions);
            Assert.IsTrue(expectedArgumentDescriptions.Count() == 1);
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null, parser);
            IArgumentsParserContext actualContext = target.Context;
            IArgumentsParserOption actualOption = target.Option;
            IEnumerable<IArgumentDescription> actualArgumentDescriptions = target.ArgumentDescriptions;
            IArgumentDescriptionDefault actualArgumentDescriptionDefault = target.ArgumentDescriptionDefault;
            Assert.AreSame(expectedContext, actualContext);
            Assert.AreSame(expectedOption, actualOption);
            Assert.AreSame(expectedArgumentDescriptionDefault, actualArgumentDescriptionDefault);
            Assert.AreNotSame(expectedArgumentDescriptions, actualArgumentDescriptions);
            Assert.IsTrue(expectedArgumentDescriptions.Count() == actualArgumentDescriptions.Count());
            Assert.IsTrue(expectedArgumentDescriptions.Zip(actualArgumentDescriptions, (eAD, aAD) => Object.ReferenceEquals(eAD, aAD)).All(ad => ad));

        }

        /// <summary>
        ///Test pour ClearArgumentDescriptions
        ///</summary>
        [TestMethod()]
        public void ClearArgumentDescriptionsTest()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null);
            target.AddArgumentDescription(new ArgumentDescriptionSwitch(ConstId1, ConstKW1));
            Assert.IsTrue(target.ArgumentDescriptions.Any());
            target.ClearArgumentDescriptions();
            Assert.IsTrue(target.ArgumentDescriptions.Any() == false);
        }

        /// <summary>
        ///Test pour AddArgumentDescription
        ///</summary>
        [TestMethod()]
        public void AddArgumentDescriptionTest()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null);
            IArgumentDescription argumentDescription = new ArgumentDescriptionSwitch(ConstId1, ConstKW1);
            target.AddArgumentDescription(argumentDescription);
            Assert.IsTrue(target.ArgumentDescriptions.Contains(argumentDescription));

            Action action = delegate { target.AddArgumentDescription(argumentDescription); };
            bool success;

            // test null
            argumentDescription = null;
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test IArgumentDescriptionDefault
            argumentDescription = new ArgumentDescriptionDefault();
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test default id of default parser option
            argumentDescription = new ArgumentDescriptionSingleValue(ConstArgumentDescription.ConstArgumentDescriptionDefaultId, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed id - null
            argumentDescription = new ArgumentDescriptionSingleValue(null, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed id - empty
            argumentDescription = new ArgumentDescriptionSingleValue(String.Empty, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed id - white space
            argumentDescription = new ArgumentDescriptionSingleValue(" ", ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed keyword - null
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, null);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed keyword - empty
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, String.Empty);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed keyword - whitespace
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, " ");
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test if Id already inserted
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId1, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test if Keyword already inserted
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, ConstKW1);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // Add Id2
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, ConstKW2);
            action();
        }

        private bool TryActionWhichThrowConfigurationArgumentsParserException(Action action)
        {
            bool succes = true;
            try
            {
                action();
                succes = false;
            }
            catch (ConfigurationArgumentsParserException) { }
            catch (Exception)
            {
                succes = false;
            }

            return succes;
        }

        /// <summary>
        ///Test pour Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            IEnumerable<string> args = ConstArgs;
            IArgumentsParserContext expected = null;
            int counter = 0;
            bool isOKInAction = true;
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) =>
            {
                expected = context;
                isOKInAction = isOKInAction && Object.ReferenceEquals(args.ElementAt(counter), arg);
                counter++;
            };
            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            IArgumentsParserContext actual;
            actual = target.Parse();
            Assert.AreSame(expected, actual);
            Assert.AreEqual(counter, args.Count());
            Assert.IsTrue(isOKInAction);

            // Controle exception management - basic exception
            Exception exception = new Exception();
            actionParserCore = (arg, context) =>
            {
                throw exception;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (ExternalArgumentsParserException e)
            {
                Assert.AreSame(e.InnerException, exception);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            // Controle exception management - lib exception
            exception = new ProcessArgumentsParserException(String.Empty, null);
            actionParserCore = (arg, context) =>
            {
                throw exception;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (BaseArgumentsParserException e)
            {
                Assert.AreSame(e, exception);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }

        /// <summary>
        ///Test pour RemoveArgumentDescription
        ///</summary>
        [TestMethod()]
        public void RemoveArgumentDescriptionTest()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null);
            string argumentDescriptionId = ConstId1;
            bool expected = false;
            bool actual;
            actual = target.RemoveArgumentDescription(argumentDescriptionId);
            Assert.AreEqual(expected, actual);

            target.AddArgumentDescription(new ArgumentDescriptionSwitch(argumentDescriptionId, ConstKW1));
            expected = true;
            actual = target.RemoveArgumentDescription(argumentDescriptionId);
            Assert.AreEqual(expected, actual);

            Action action = delegate
            {
                target.RemoveArgumentDescription(argumentDescriptionId);
            };

            // Control exception - null parameter
            argumentDescriptionId = null;
            bool success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);

            // Control exception - empty parameter
            argumentDescriptionId = String.Empty;
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);

            // Control exception - whitespace parameter
            argumentDescriptionId = " ";
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
        }

        /// <summary>
        ///Test pour RemoveArgumentDescription
        ///</summary>
        [TestMethod()]
        public void RemoveArgumentDescriptionTest1()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null);
            string argumentDescriptionId = ConstId1;
            IArgumentDescription argumentDescription = new ArgumentDescriptionSwitch(argumentDescriptionId, ConstKW1);
            bool expected = false;
            bool actual;
            actual = target.RemoveArgumentDescription(argumentDescription);
            Assert.AreEqual(expected, actual);

            target.AddArgumentDescription(argumentDescription);
            expected = true;
            actual = target.RemoveArgumentDescription(argumentDescription);
            Assert.AreEqual(expected, actual);

            Action action = delegate
            {
                target.RemoveArgumentDescription((IArgumentDescription)null);
            };

            // Control exception - null parameter
            bool success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);


            action = delegate
            {
                target.RemoveArgumentDescription(new ArgumentDescriptionSwitch(argumentDescriptionId, ConstKW1));
            };

            // Control exception - null id
            argumentDescriptionId = null;
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);

            // Control exception - empty id
            argumentDescriptionId = String.Empty;
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);

            // Control exception - whitespace id
            argumentDescriptionId = " ";
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);

        }

        /// <summary>
        ///Test pour SetArgumentDescription
        ///</summary>
        [TestMethod()]
        public void SetArgumentDescriptionTest()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null);
            IArgumentDescription argumentDescription = new ArgumentDescriptionSwitch(ConstId1, ConstKW1);
            target.AddArgumentDescription(argumentDescription);
            Assert.IsTrue(target.ArgumentDescriptions.Contains(argumentDescription));

            Action action = delegate { target.SetArgumentDescription(argumentDescription); };
            bool success;

            // test null
            argumentDescription = null;
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test IArgumentDescriptionDefault
            argumentDescription = new ArgumentDescriptionDefault();
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test default id of default parser option
            argumentDescription = new ArgumentDescriptionSingleValue(ConstArgumentDescription.ConstArgumentDescriptionDefaultId, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed id - null
            argumentDescription = new ArgumentDescriptionSingleValue(null, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed id - empty
            argumentDescription = new ArgumentDescriptionSingleValue(String.Empty, ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed id - white space
            argumentDescription = new ArgumentDescriptionSingleValue(" ", ConstKW2);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed keyword - null
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, null);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed keyword - empty
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, String.Empty);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test of malformed keyword - whitespace
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, " ");
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // test if Keyword already inserted for an other Id
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, ConstKW1);
            success = TryActionWhichThrowConfigurationArgumentsParserException(action);
            Assert.IsTrue(success);
            Assert.IsTrue(target.ArgumentDescriptions.Count() == 1);

            // Set again Id1
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId1, ConstKW1);
            action();

            // Set Id2
            argumentDescription = new ArgumentDescriptionSingleValue(ConstId2, ConstKW2);
            action();

        }

        /// <summary>
        ///Test pour ArgumentDescriptionDefault
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionDefaultTest()
        {
            BaseArgumentsParser target = CreateBaseArgumentsParser(null, null); // TODO: initialisez à une valeur appropriée
            IArgumentDescriptionDefault actual;
            actual = target.ArgumentDescriptionDefault;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Id == ConstArgumentDescription.ConstArgumentDescriptionDefaultId);
        }

        /// <summary>
        ///Test pour ArgumentDescriptions
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionsTest()
        {
            IEnumerable<string> args = ConstArgs;
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) => { };

            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            IEnumerable<IArgumentDescription> actual;
            actual = target.ArgumentDescriptions;
            Assert.IsNotNull(actual);

            target.AddArgumentDescription(new ArgumentDescriptionSingleValue(ConstId1, ConstArgKW1));
            target.AddArgumentDescription(new ArgumentDescriptionSingleValue(ConstId2, ConstArgKW2));

            Assert.IsTrue(actual.Count() == 2);
            Assert.IsTrue(actual.Any(ad => ad.Id == ConstId1));
            Assert.IsTrue(actual.Any(ad => ad.Id == ConstId2));
        }

        /// <summary>
        ///Test pour Context
        ///</summary>
        [TestMethod()]
        public void ContextTest()
        {
            IEnumerable<string> args = ConstArgs;

            IArgumentsParserContext expected = null;
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) =>
            {
                expected = context;
            };
            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            IArgumentsParserContext actual;
            actual = target.Context;
            Assert.IsNotNull(actual);
            target.Parse();
            Assert.AreSame(actual, expected);

            actionParserCore = (arg, context) => { };
            expected = new ArgumentsParserContext();
            target = CreateBaseArgumentsParser(args, actionParserCore, ArgumentsParserOption.DefaultOption, expected);
            actual = target.Context;
            Assert.IsNotNull(actual);
            target.Parse();
            Assert.AreSame(actual, expected);
        }

        /// <summary>
        ///Test pour Option
        ///</summary>
        [TestMethod()]
        public void OptionTest()
        {
            IEnumerable<string> args = ConstArgs;
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) => { };

            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            IArgumentsParserOption actual;
            actual = target.Option;
            Assert.IsNotNull(actual);
            IArgumentsParserOption expected = ArgumentsParserOption.DefaultOption;
            Assert.AreSame(actual, expected);

            expected = new ArgumentsParserOption();
            target = CreateBaseArgumentsParser(args, actionParserCore, expected, new ArgumentsParserContext());
            actual = target.Option;
            Assert.IsNotNull(actual);
            target.Parse();
            Assert.AreSame(actual, expected);

        }

        /// <summary>
        /// Test pour ParsingArgs
        /// </summary>
        [TestMethod()]
        public void ParsingArgsTest()
        {
            bool isOKInAction = true;

            IEnumerable<string> args = ConstArgs;

            // Basic test
            IEnumerable<string> expected = args;
            IEnumerable<string> actual = null;
            EventHandler<ProcessArgsArgumentsParserEventArgs> eventManager = (sender, e) =>
            {
                actual = e.Args;
                isOKInAction = isOKInAction && (false == e.IsProcessed);
            };
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) => { };
            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArgs += eventManager;
            target.Parse();
            Assert.AreEqual(actual.Count(), expected.Count());
            Assert.IsTrue(actual.Zip(expected, (a1, a2) => Object.ReferenceEquals(a1, a2)).All(b => b));
            Assert.IsTrue(isOKInAction);

            // Control parsing test
            isOKInAction = true;
            eventManager = (sender, e) =>
            {
                e.IsProcessed = true;
            };
            actionParserCore = (arg, context) =>
            {
                isOKInAction = false;   // should not be call
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArgs += eventManager;
            target.Parse();
            Assert.IsTrue(isOKInAction);

            // Control Warnings - no message
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArgs += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);

            // Control Warnings with event processing - no message
            eventManager = (sender, e) =>
            {
                e.IsProcessed = true;
                e.WarningOccurs = true;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArgs += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);

            // Control Warnings - no message
            string message = ConstWarningMessage;
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
                e.WarningDescription = message;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArgs += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);
            Assert.IsTrue(target.Context.WarningMessages.Count() == 1);
            Assert.AreSame(target.Context.WarningMessages.First(), message);

            // Control exception management
            Exception exception = new Exception();
            eventManager = (sender, e) =>
            {
                throw exception;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArgs += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }

        }

        /// <summary>
        /// Test pour ParsedArgs
        /// </summary>
        [TestMethod()]
        public void ParsedArgsTest()
        {
            bool isOKInAction = true;

            IEnumerable<string> args = ConstArgs;

            // Basic test
            IEnumerable<string> expected = args;
            IEnumerable<string> actual = null;
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) => { };
            EventHandler<ProcessArgsArgumentsParserEventArgs> eventManager = (sender, e) =>
            {
                actual = e.Args;
                isOKInAction = isOKInAction && (true == e.IsProcessed);
            };
            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            target.Parse();
            Assert.AreEqual(actual.Count(), expected.Count());
            Assert.IsTrue(actual.Zip(expected, (a1, a2) => Object.ReferenceEquals(a1, a2)).All(b => b));
            Assert.IsTrue(isOKInAction);

            // Control parsing test
            isOKInAction = false;
            actionParserCore = (arg, context) =>
            {
                isOKInAction = true;   // should be call
            };
            eventManager = (sender, e) =>
            {
                isOKInAction = isOKInAction && e.IsProcessed;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            target.Parse();
            Assert.IsTrue(isOKInAction);

            // Control Warnings - no message
            actionParserCore = (arg, context) => { };
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);

            // Control Warnings - with message
            string message = ConstWarningMessage;
            actionParserCore = (arg, context) => { };
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
                e.WarningDescription = message;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);
            Assert.IsTrue(target.Context.WarningMessages.Count() == 1);
            Assert.AreSame(target.Context.WarningMessages.First(), message);

            // Control exception management
            actionParserCore = (arg, context) => { };
            Exception exception = new Exception();
            eventManager = (sender, e) =>
            {
                throw exception;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }

            // Control post action not call exception in ParserCore - basic exception
            isOKInAction = true;
            exception = new Exception();
            actionParserCore = (arg, context) =>
            {
                throw exception;
            };
            eventManager = (sender, e) =>
            {
                isOKInAction = false;   // should not be called
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.IsTrue(isOKInAction);

            // Control post action not call exception in ParserCore - lib exception
            isOKInAction = true;
            exception = new ProcessArgumentsParserException(String.Empty, null);
            actionParserCore = (arg, context) =>
            {
                throw exception;
            };
            eventManager = (sender, e) =>
            {
                isOKInAction = false;   // should not be called
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArgs += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.IsTrue(isOKInAction);

        }

        /// <summary>
        /// Test pour ParsingArg
        /// </summary>
        [TestMethod()]
        public void ParsingArgTest()
        {
            bool isOKInAction = true;

            IEnumerable<string> args = ConstArgs;

            // Basic test
            int counter = 0;
            EventHandler<ProcessArgArgumentsParserEventArgs> eventManager = (sender, e) =>
            {
                isOKInAction = isOKInAction && Object.ReferenceEquals(args.ElementAt(counter), e.Arg);
                counter++;
                isOKInAction = isOKInAction && (false == e.IsProcessed);
            };
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) => { };
            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArg += eventManager;
            target.Parse();
            Assert.IsTrue(isOKInAction);

            // Control parsing test
            isOKInAction = true;
            eventManager = (sender, e) =>
            {
                e.IsProcessed = true;
            };
            actionParserCore = (arg, context) =>
            {
                isOKInAction = false;   // should not be call
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArg += eventManager;
            target.Parse();
            Assert.IsTrue(isOKInAction);

            // Control Warnings - no message
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArg += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);

            // Control Warnings with event processing - no message
            eventManager = (sender, e) =>
            {
                e.IsProcessed = true;
                e.WarningOccurs = true;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArg += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);

            // Control Warnings - no message
            string message = ConstWarningMessage;
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
                e.WarningDescription = message;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArg += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);
            Assert.IsTrue(target.Context.WarningMessages.Count() == args.Count());
            Assert.AreSame(target.Context.WarningMessages.First(), message);

            // Control exception management
            Exception exception = new Exception();
            eventManager = (sender, e) =>
            {
                throw exception;
            };
            actionParserCore = (arg, context) => { };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsingArg += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Test pour ParsedArg
        /// </summary>
        [TestMethod()]
        public void ParsedArgTest()
        {
            bool isOKInAction = true;

            IEnumerable<string> args = ConstArgs;

            // Basic test
            int counter = 0;
            Action<string, IArgumentsParserContext> actionParserCore = (arg, context) => { };
            EventHandler<ProcessArgArgumentsParserEventArgs> eventManager = (sender, e) =>
            {
                isOKInAction = isOKInAction && Object.ReferenceEquals(args.ElementAt(counter), e.Arg);
                isOKInAction = isOKInAction && (true == e.IsProcessed);
                counter++;
            };
            BaseArgumentsParser target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            target.Parse();
            Assert.IsTrue(isOKInAction);

            // Control parsing test
            isOKInAction = false;
            actionParserCore = (arg, context) =>
            {
                isOKInAction = true;   // should be call
            };
            eventManager = (sender, e) =>
            {
                isOKInAction = isOKInAction && e.IsProcessed;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            target.Parse();
            Assert.IsTrue(isOKInAction);

            // Control Warnings - no message
            actionParserCore = (arg, context) => { };
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);

            // Control Warnings - with message
            string message = ConstWarningMessage;
            actionParserCore = (arg, context) => { };
            eventManager = (sender, e) =>
            {
                e.WarningOccurs = true;
                e.WarningDescription = message;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            target.Parse();
            Assert.IsTrue(target.Context.WarningMessages.Count() > 0);
            Assert.IsTrue(target.Context.WarningMessages.Count() == args.Count());
            Assert.AreSame(target.Context.WarningMessages.First(), message);

            // Control exception management
            actionParserCore = (arg, context) => { };
            Exception exception = new Exception();
            eventManager = (sender, e) =>
            {
                throw exception;
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }

            // Control post action not call exception in ParserCore - basic exception
            isOKInAction = true;
            exception = new Exception();
            actionParserCore = (arg, context) =>
            {
                throw exception;
            };
            eventManager = (sender, e) =>
            {
                isOKInAction = false;   // should not be called
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.IsTrue(isOKInAction);

            // Control post action not call exception in ParserCore - lib exception
            isOKInAction = true;
            exception = new ProcessArgumentsParserException(String.Empty, null);
            actionParserCore = (arg, context) =>
            {
                throw exception;
            };
            eventManager = (sender, e) =>
            {
                isOKInAction = false;   // should not be called
            };
            target = CreateBaseArgumentsParser(args, actionParserCore);
            target.ParsedArg += eventManager;
            try
            {
                target.Parse();
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.IsTrue(isOKInAction);

        }

    }
}
