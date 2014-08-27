using ArgumentsParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using ArgumentsParser.ArgumentDescription;

namespace ArgumentsParser.Test
{


    /// <summary>
    ///Classe de test pour ArgumentsParserManagerTest, destinée à contenir tous
    ///les tests unitaires ArgumentsParserManagerTest
    ///</summary>
    [TestClass()]
    public class ArgumentsParserManagerTest
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

        private const string ConstId1 = "Id1";
        private const string ConstKW1 = "kw1";
        private const string ConstArg1 = "kjgkg";
        private const string ConstArg2 = "kozcg";
        private const string ConstValueArg3 = "kozcg";
        private const string ConstArg3 = "/" + ConstKW1 + ":" + ConstValueArg3;

        /// <summary>
        ///Test pour NewArgumentParser
        ///</summary>
        [TestMethod()]
        public void NewArgumentParserTest()
        {
            IEnumerable<string> args = Enumerable.Empty<string>();
            IArgumentsParser actual;
            actual = ArgumentsParserManager.NewArgumentParser(args);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///Test pour ArgumentParser
        ///</summary>
        [TestMethod()]
        public void ArgumentParserTest1()
        {
            IEnumerable<string> args = new[] { ConstArg1, ConstArg2, };
            IArgumentsParser target;
            target = ArgumentsParserManager.NewArgumentParser(args);
            target.Parse();
            Assert.IsTrue(target.Context.Contexts.Count == 1);
            Assert.IsTrue(target.Context.Contexts.First().Count() == 2);
            Assert.AreSame(target.Context.GetContext(ArgumentsParserManager.ConstArgumentDescriptionDefaultId), ConstArg1);
            Assert.AreSame(target.Context.Contexts[ArgumentsParserManager.ConstArgumentDescriptionDefaultId].ElementAt(1), ConstArg2);
        }

        /// <summary>
        ///Test pour ArgumentParser
        ///</summary>
        [TestMethod()]
        public void ArgumentParserTest2()
        {
            IEnumerable<string> args = new[] { ConstArg3, };
            IArgumentsParser target;
            target = ArgumentsParserManager.NewArgumentParser(args);
            target.AddArgumentDescription(new ArgumentDescriptionSingleValue(ConstId1, ConstKW1));
            target.Parse();
            Assert.IsTrue(target.Context.Contexts.Count == 1);
            Assert.IsTrue(target.Context.Contexts.First().Count() == 1);
            Assert.AreEqual(target.Context.GetContext(ConstId1), ConstValueArg3);
        }

        /// <summary>
        ///Test pour ArgumentParser
        ///</summary>
        [TestMethod()]
        public void ArgumentParserTest3()
        {
            IEnumerable<string> args = new[] { "/dsqsq:dqdq:dqd", };
            IArgumentsParser target;
            target = ArgumentsParserManager.NewArgumentParser(args);
            target.AddArgumentDescription(new ArgumentDescriptionSingleValue(ConstId1, ConstKW1));
            target.Parse();
            Assert.IsTrue(target.Context.Contexts.Count == 0);
            Assert.IsTrue(target.Context.WarningMessages.Count() == 1);
        }

    }
}
