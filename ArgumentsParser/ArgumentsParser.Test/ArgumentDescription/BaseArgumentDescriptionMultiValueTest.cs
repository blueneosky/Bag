using ArgumentsParser.ArgumentDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArgumentsParser;
using System.Collections.Generic;
using ArgumentsParser.Test.Helper;

namespace ArgumentsParser.Test.ArgumentDescription
{


    /// <summary>
    ///Classe de test pour BaseArgumentDescriptionMultiValueTest, destinée à contenir tous
    ///les tests unitaires BaseArgumentDescriptionMultiValueTest
    ///</summary>
    [TestClass()]
    public class BaseArgumentDescriptionMultiValueTest
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


        internal virtual BaseArgumentDescriptionMultiValueHelper CreateBaseArgumentDescriptionMultiValueHelper(string id, string keyWord)
        {
            BaseArgumentDescriptionMultiValueHelper target = new BaseArgumentDescriptionMultiValueHelper(id, keyWord);
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

        /// <summary>
        ///Test pour ProcessCore
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void ProcessCoreTest()
        {
            string id = CreateId();
            string keyWord = CreateKeyWord();
            BaseArgumentDescriptionMultiValueHelper target = CreateBaseArgumentDescriptionMultiValueHelper(id, keyWord); // TODO: initialisez à une valeur appropriée
            string extraArgument = string.Empty; // TODO: initialisez à une valeur appropriée
            IArgumentsParserContext context = null; // TODO: initialisez à une valeur appropriée
            object value = null; // TODO: initialisez à une valeur appropriée
            object valueExpected = null; // TODO: initialisez à une valeur appropriée
            bool expected = false; // TODO: initialisez à une valeur appropriée
            bool actual;
            actual = target.ProcessCore(extraArgument, context, out value);
            Assert.AreEqual(valueExpected, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        internal virtual BaseArgumentDescriptionMultiValue CreateBaseArgumentDescriptionMultiValue()
        {
            // TODO: instanciez une classe concrète appropriée.
            BaseArgumentDescriptionMultiValue target = null;
            return target;
        }

        /// <summary>
        ///Test pour Values
        ///</summary>
        [TestMethod()]
        public void ValuesTest()
        {
            BaseArgumentDescriptionMultiValue target = CreateBaseArgumentDescriptionMultiValue(); // TODO: initialisez à une valeur appropriée
            IEnumerable<string> actual;
            actual = target.Values;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour ValuesList
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void ValuesListTest()
        {
            string id = CreateId();
            string keyWord = CreateKeyWord();
            BaseArgumentDescriptionMultiValueHelper target = CreateBaseArgumentDescriptionMultiValueHelper(id, keyWord); // TODO: initialisez à une valeur appropriée
            List<string> actual;
            actual = target.ValuesListHelper;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }
    }
}
