using ArgumentsParser.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArgumentsParser.System;
using ArgumentsParser;
using ArgumentsParser.Test.Helper;
using System.Linq;

namespace ArgumentsParser.Test.Core
{


    /// <summary>
    ///Classe de test pour ToolsTest, destinée à contenir tous
    ///les tests unitaires ToolsTest
    ///</summary>
    [TestClass()]
    public class ToolsTest
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


        [TestMethod()]
        public void DoEventWithTryCathAndLogTest()
        {
            bool isOkInAction = false;

            object sender = new object();
            BaseArgumentsParserEventArgs e = new ProcessArgArgumentsParserEventArgs(String.Empty);
            string eventName = string.Empty;
            IArgumentsParserContext context = new ArgumentsParserContext();

            // Control is action is lunch
            Action<object, BaseArgumentsParserEventArgs> doEventFunc = (pSender, pE) =>
            {
                Assert.AreSame(sender, pSender);
                Assert.AreSame(e, pE);
                isOkInAction = true;
            };
            Tools.DoEventWithTryCathAndLog(doEventFunc, sender, e, eventName, context);
            Assert.IsTrue(isOkInAction);

            // Control warning management - no message
            eventName = Guid.NewGuid().ToString();
            context = new ArgumentsParserContext();
            Assert.IsTrue(context.WarningMessages.Count() == 0);
            doEventFunc = (pSender, pE) =>
            {
                e.WarningOccurs = true;
            };
            Tools.DoEventWithTryCathAndLog(doEventFunc, sender, e, eventName, context);
            Assert.IsTrue(context.WarningMessages.Count() == 1);
            Assert.IsTrue(context.WarningMessages.First().Contains(eventName));

            // Control warning management - no message
            eventName = Guid.NewGuid().ToString();
            context = new ArgumentsParserContext();
            string message = Guid.NewGuid().ToString();
            Assert.IsTrue(context.WarningMessages.Count() == 0);
            doEventFunc = (pSender, pE) =>
            {
                e.WarningOccurs = true;
                e.WarningDescription = message;
            };
            Tools.DoEventWithTryCathAndLog(doEventFunc, sender, e, eventName, context);
            Assert.IsTrue(context.WarningMessages.Count() == 1);
            Assert.IsTrue(context.WarningMessages.First() == message);

            // Control exception management
            Exception exception = new Exception();
            context = new ArgumentsParserContext();
            Assert.IsTrue(context.WarningMessages.Count() == 0);
            doEventFunc = (pSender, pE) =>
            {
                throw exception;
            };
            try
            {
                Tools.DoEventWithTryCathAndLog(doEventFunc, sender, e, eventName, context);
                Assert.Fail();
            }
            catch (ExternalArgumentsParserException externalArgumentsParserException)
            {
                Assert.IsNotNull(externalArgumentsParserException);
                Assert.AreSame(externalArgumentsParserException.InnerException, exception);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

    }
}
