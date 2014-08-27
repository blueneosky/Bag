using ArgumentsParser.ArgumentDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArgumentsParser;
using ArgumentsParser.Config;
using System.Collections.Generic;
using ArgumentsParser.Core;
using ArgumentsParser.System;
using ArgumentsParser.Test.Helper;

namespace ArgumentsParser.Test.ArgumentDescription
{


    /// <summary>
    ///Classe de test pour ArgumentDescriptionFileArgumentTest, destinée à contenir tous
    ///les tests unitaires ArgumentDescriptionFileArgumentTest
    ///</summary>
    [TestClass()]
    public class ArgumentDescriptionFileArgumentTest
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
        ///Test pour Constructeur ArgumentDescriptionFileArgument
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionFileArgumentConstructorTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            IArgumentsParser parser = null; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord, parser);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour Constructeur ArgumentDescriptionFileArgument
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionFileArgumentConstructorTest1()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            IArgumentsParserOption option = null; // TODO: initialisez à une valeur appropriée
            IEnumerable<IArgumentDescription> argumentDescriptions = null; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord, option, argumentDescriptions);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour Constructeur ArgumentDescriptionFileArgument
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionFileArgumentConstructorTest2()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord);
            Assert.Inconclusive("TODO: implémentez le code pour vérifier la cible");
        }

        /// <summary>
        ///Test pour FileProcessCore
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void FileProcessCoreTest()
        {
            ArgumentDescriptionFileArgument param0 = null; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument_Accessor target = new ArgumentDescriptionFileArgument_Accessor(param0); // TODO: initialisez à une valeur appropriée
            string filePath = string.Empty; // TODO: initialisez à une valeur appropriée
            FileArgumentsParser parser = null; // TODO: initialisez à une valeur appropriée
            IArgumentsParserContext context = null; // TODO: initialisez à une valeur appropriée
            object value = null; // TODO: initialisez à une valeur appropriée
            object valueExpected = null; // TODO: initialisez à une valeur appropriée
            bool expected = false; // TODO: initialisez à une valeur appropriée
            bool actual;
            actual = target.FileProcessCore(filePath, parser, context, out value);
            Assert.AreEqual(valueExpected, value);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour OnProcessedFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void OnProcessedFileTest()
        {
            ArgumentDescriptionFileArgument param0 = null; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument_Accessor target = new ArgumentDescriptionFileArgument_Accessor(param0); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            ProcessArgumentDescriptionFileArgumentEventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.OnProcessedFile(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour OnProcessingFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void OnProcessingFileTest()
        {
            ArgumentDescriptionFileArgument param0 = null; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument_Accessor target = new ArgumentDescriptionFileArgument_Accessor(param0); // TODO: initialisez à une valeur appropriée
            object sender = null; // TODO: initialisez à une valeur appropriée
            ProcessArgumentDescriptionFileArgumentEventArgs e = null; // TODO: initialisez à une valeur appropriée
            target.OnProcessingFile(sender, e);
            Assert.Inconclusive("Une méthode qui ne retourne pas une valeur ne peut pas être vérifiée.");
        }

        /// <summary>
        ///Test pour ProcessCore
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ArgumentsParser.dll")]
        public void ProcessCoreTest()
        {
            ArgumentDescriptionFileArgument param0 = null; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument_Accessor target = new ArgumentDescriptionFileArgument_Accessor(param0); // TODO: initialisez à une valeur appropriée
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

        /// <summary>
        ///Test pour ArgumentDescription
        ///</summary>
        [TestMethod()]
        public void ArgumentDescriptionTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord); // TODO: initialisez à une valeur appropriée
            IEnumerable<IArgumentDescription> actual;
            actual = target.ArgumentDescription;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Option
        ///</summary>
        [TestMethod()]
        public void OptionTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord); // TODO: initialisez à une valeur appropriée
            IArgumentsParserOption actual;
            actual = target.Option;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Parser
        ///</summary>
        [TestMethod()]
        public void ParserTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord); // TODO: initialisez à une valeur appropriée
            IArgumentsParser actual;
            actual = target.Parser;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }

        /// <summary>
        ///Test pour Values
        ///</summary>
        [TestMethod()]
        public void ValuesTest()
        {
            string id = string.Empty; // TODO: initialisez à une valeur appropriée
            string keyWord = string.Empty; // TODO: initialisez à une valeur appropriée
            ArgumentDescriptionFileArgument target = new ArgumentDescriptionFileArgument(id, keyWord); // TODO: initialisez à une valeur appropriée
            IEnumerable<Tuple<string, IArgumentsParserContext>> actual;
            actual = target.Values;
            Assert.Inconclusive("Vérifiez l\'exactitude de cette méthode de test.");
        }
    }
}
