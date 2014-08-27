using ArgumentsParser.ArgumentDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArgumentsParser.System;
using ArgumentsParser;
using ArgumentsParser.Test.Helper;
using ArgumentsParser.Core;

namespace ArgumentsParser.Test.ArgumentDescription
{


    /// <summary>
    ///Classe de test pour BaseArgumentDescriptionTest, destinée à contenir tous
    ///les tests unitaires BaseArgumentDescriptionTest
    ///</summary>
    [TestClass()]
    public class BaseArgumentDescriptionTest
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

        internal delegate Tuple<object, bool> ProcessCoreFuncDelegate(string extraArg, IArgumentsParserContext context);

        internal virtual BaseArgumentDescriptionHelper CreateBaseArgumentDescription(string id, string keyWord)
        {
            Func<string, IArgumentsParserContext, Tuple<object, bool>> func = delegate { return null; };
            BaseArgumentDescriptionHelper target = new BaseArgumentDescriptionHelper(id, keyWord, func);
            return target;
        }

        internal virtual BaseArgumentDescription CreateBaseArgumentDescription(string id, string keyWord, ProcessCoreFuncDelegate processCoreFunc)
        {
            Func<string, IArgumentsParserContext, Tuple<object, bool>> func = (extraArg, context) => processCoreFunc(extraArg, context);
            BaseArgumentDescription target = new BaseArgumentDescriptionHelper(id, keyWord, func);
            return target;
        }

        internal string CreateId()
        {
            return Guid.NewGuid().ToString();
        }

        internal string CreateKeyWord()
        {
            return Guid.NewGuid().ToString().Replace("{", "").Split(new[] { '-' })[0];
        }

        internal IArgumentsParserContext CreateContext()
        {
            return new ArgumentsParserContext();
        }

        /// <summary>
        ///Test pour OnProcessed
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void OnProcessedTest()
        {
            string id = CreateId();
            string keyWord = CreateKeyWord();
            IArgumentsParserContext context = CreateContext();
            object sender = new object();
            ProcessArgumentDescriptionEventArgs e = new ProcessArgumentDescriptionEventArgs(context);
            bool isOkInFunc = false;
            EventHandler<ProcessArgumentDescriptionEventArgs> manager = (pSender, pE) =>
            {
                isOkInFunc = true;
                isOkInFunc = isOkInFunc && Object.ReferenceEquals(sender, pSender);
                isOkInFunc = isOkInFunc && Object.ReferenceEquals(e, pE);
            };
            BaseArgumentDescriptionHelper target = CreateBaseArgumentDescription(id, keyWord);
            target.Processed += manager;
            target.OnProcessed(sender, e);

            Assert.IsTrue(isOkInFunc);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour OnProcessing
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void OnProcessingTest()
        {
            string id = CreateId();
            string keyWord = CreateKeyWord();
            IArgumentsParserContext context = CreateContext();
            object sender = new object();
            ProcessArgumentDescriptionEventArgs e = new ProcessArgumentDescriptionEventArgs(context);
            bool isOkInFunc = false;
            EventHandler<ProcessArgumentDescriptionEventArgs> manager = (pSender, pE) =>
            {
                isOkInFunc = true;
                isOkInFunc = isOkInFunc && Object.ReferenceEquals(sender, pSender);
                isOkInFunc = isOkInFunc && Object.ReferenceEquals(e, pE);
            };
            BaseArgumentDescriptionHelper target = CreateBaseArgumentDescription(id, keyWord);
            target.Processing += manager;
            target.OnProcessing(sender, e);

            Assert.IsTrue(isOkInFunc);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        internal virtual BaseArgumentDescription CreateBaseArgumentDescription()
        {
            // TODO: instanciez une classe concrète appropriée.
            BaseArgumentDescription target = null;
            return target;
        }

        /// <summary>
        ///Test pour Process
        ///</summary>
        [TestMethod()]
        public void ProcessTest()
        {
            BaseArgumentDescription target = CreateBaseArgumentDescription(); // TODO: initialisez à une valeur appropriée
            string extraArgument = string.Empty; // TODO: initialisez à une valeur appropriée
            IArgumentsParserContext context = null; // TODO: initialisez à une valeur appropriée
            target.Process(extraArgument, context);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ProcessCore
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void ProcessCoreTest()
        {
            // Impossible de trouver un accesseur private pour ProcessCore. Régénérez le projet contenant ou exécutez Publicize.exe manuellement.
            Assert.Inconclusive("Impossible de trouver un accesseur private pour ProcessCore. Régénérez le projet " +
                    "contenant ou exécutez Publicize.exe manuellement.");
        }

        /// <summary>
        ///Test pour HelpInfo
        ///</summary>
        [TestMethod()]
        public void HelpInfoTest()
        {
            BaseArgumentDescription target = CreateBaseArgumentDescription(); // TODO: initialisez à une valeur appropriée
            string expected = string.Empty; // TODO: initialisez à une valeur appropriée
            string actual;
            target.HelpInfo = expected;
            actual = target.HelpInfo;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Id
        ///</summary>
        [TestMethod()]
        public void IdTest()
        {
            BaseArgumentDescription target = CreateBaseArgumentDescription(); // TODO: initialisez à une valeur appropriée
            string actual;
            actual = target.Id;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour KeyWord
        ///</summary>
        [TestMethod()]
        public void KeyWordTest()
        {
            BaseArgumentDescription target = CreateBaseArgumentDescription(); // TODO: initialisez à une valeur appropriée
            string actual;
            actual = target.KeyWord;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }
    }
}
