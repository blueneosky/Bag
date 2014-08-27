using ArgumentsParser.ArgumentDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArgumentsParser;

namespace ArgumentsParser.Test.ArgumentDescription
{
    
    
    /// <summary>
    ///Classe de test pour ArgumentDescriptionSingleValueTest, destinée à contenir tous
    ///les tests unitaires ArgumentDescriptionSingleValueTest
    ///</summary>
    [TestClass()]
    public class ArgumentDescriptionSingleValueTest
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


        /// <summary>
        ///Test pour Constructeur ArgumentDescriptionSingleValue
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionSingleValueConstructorTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionSingleValue target = new ArgumentDescriptionSingleValue(id, keyWord);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour ProcessCore
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void ProcessCoreTest()
        {
            //PrivateObject param0 = null; // TODO: initialisez à une valeur appropriée
            //ArgumentDescriptionSingleValue_Accessor target = new ArgumentDescriptionSingleValue_Accessor(param0); // TODO: initialisez à une valeur appropriée
            //string extraArgument = string.Empty; // TODO: initialisez à une valeur appropriée
            //IArgumentsParserContext context = null; // TODO: initialisez à une valeur appropriée
            //object value = null; // TODO: initialisez à une valeur appropriée
            //object valueExpected = null; // TODO: initialisez à une valeur appropriée
            //bool expected = false; // TODO: initialisez à une valeur appropriée
            //bool actual;
            //actual = target.ProcessCore(extraArgument, context, out value);
            //Assert.AreEqual(valueExpected, value);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Value
        ///</summary>
        [TestMethod()]
        public void ValueTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionSingleValue target = new ArgumentDescriptionSingleValue(id, keyWord); // TODO: initialisez à une valeur appropriée
            string actual;
            actual = target.Value;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }
    }
}
