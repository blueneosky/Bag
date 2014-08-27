using ArgumentsParser.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ArgumentsParser.Test.Core
{


    /// <summary>
    ///Classe de test pour ArgumentsParserContextTest, destinée à contenir tous
    ///les tests unitaires ArgumentsParserContextTest
    ///</summary>
    [TestClass()]
    public class ArgumentsParserContextTest
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
        private const string ConstId2 = "Id2";
        private const string ConstValue1 = "Value1";
        private const string ConstValue2 = "Value2";
        private const string ConstValue3 = "Value3";

        /// <summary>
        ///Test pour Constructeur ArgumentsParserContext
        ///</summary>
        [TestMethod()]
        public void ArgumentsParserContextConstructorTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext();
            Assert.IsNotNull(target.Contexts);
            Assert.IsNotNull(target.WarningMessages);

        }

        /// <summary>
        ///Test pour AddContext
        ///</summary>
        [TestMethod()]
        public void AddContextTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext();
            string contextId = ConstId1;
            object contextValue = ConstValue1;
            target.AddContext(contextId, contextValue);
            Assert.IsTrue(target.Contexts.Count == 1);
            Assert.IsTrue(target.Contexts[contextId].Count() == 1);
            Assert.AreSame(target.Contexts[contextId].First(), contextValue);

            contextId = ConstId1;
            contextValue = ConstValue2;
            target.AddContext(contextId, contextValue);
            target.AddContext(contextId, contextValue);
            Assert.IsTrue(target.Contexts.Count == 1);
            Assert.IsTrue(target.Contexts[contextId].Count() == 3);
            Assert.AreSame(target.Contexts[contextId].ElementAt(1), contextValue);
            Assert.AreSame(target.Contexts[contextId].ElementAt(2), contextValue);

            contextId = ConstId2;
            contextValue = ConstValue2;
            target.AddContext(contextId, contextValue);
            Assert.IsTrue(target.Contexts.Count == 2);
            Assert.IsTrue(target.Contexts[contextId].Count() == 1);
            Assert.AreSame(target.Contexts[contextId].First(), contextValue);

        }

        /// <summary>
        ///Test pour AddWarningMessage
        ///</summary>
        [TestMethod()]
        public void AddWarningMessageTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext(); // TODO: initialisez à une valeur appropriée
            string message = Guid.NewGuid().ToString();
            target.AddWarningMessage(message);
            Assert.IsTrue(target.WarningMessages.Any(msg => Object.ReferenceEquals(msg, message)));
        }

        /// <summary>
        ///Test pour GetContext
        ///</summary>
        [TestMethod()]
        public void GetContextTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext(); // TODO: initialisez à une valeur appropriée
            string contextId1 = ConstId1;
            object expected1 = ConstValue1;
            string contextId2 = ConstId2;
            object expected2 = ConstValue3;
            target.AddContext(ConstId1, expected1);
            target.AddContext(ConstId1, ConstValue2);
            target.AddContext(contextId2, expected2);
            object actual;
            actual = target.GetContext(contextId1);
            Assert.AreEqual(expected1, actual);

            actual = target.GetContext(contextId2);
            Assert.AreEqual(expected2, actual);
        }

        /// <summary>
        ///Test pour RemoveContext
        ///</summary>
        [TestMethod()]
        public void RemoveContextTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext(); // TODO: initialisez à une valeur appropriée
            string contextId = ConstId1;
            object expected = ConstValue1;
            object actual;
            target.AddContext(contextId, expected);
            target.AddContext(contextId, ConstValue2);
            target.AddContext(ConstId2, ConstValue3);
            Assert.IsTrue(target.Contexts.Count == 2);
            Assert.IsTrue(target.Contexts[contextId].Count() == 2);
            Assert.IsTrue(target.Contexts[ConstId2].Count() == 1);
            actual = target.RemoveContext(contextId);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.Contexts[contextId].Count() == 1);
            Assert.IsTrue(target.Contexts[ConstId2].Count() == 1);
        }

        /// <summary>
        ///Test pour SetContext
        ///</summary>
        [TestMethod()]
        public void SetContextTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext();
            string contextId = ConstId1;
            object contextValue = ConstValue1;
            target.SetContext(contextId, contextValue);
            Assert.IsTrue(target.Contexts.Count == 1);
            Assert.IsTrue(target.Contexts[contextId].Count() == 1);
            Assert.AreSame(target.Contexts[contextId].First(), contextValue);

            contextId = ConstId1;
            contextValue = ConstValue2;
            target.SetContext(contextId, contextValue);
            target.SetContext(contextId, contextValue);
            Assert.IsTrue(target.Contexts.Count == 1);
            Assert.IsTrue(target.Contexts[contextId].Count() == 1);
            Assert.AreSame(target.Contexts[contextId].First(), contextValue);

            contextId = ConstId2;
            contextValue = ConstValue2;
            target.SetContext(contextId, contextValue);
            Assert.IsTrue(target.Contexts.Count == 2);
            Assert.IsTrue(target.Contexts[contextId].Count() == 1);
            Assert.AreSame(target.Contexts[contextId].First(), contextValue);

        }

        /// <summary>
        ///Test pour Contexts
        ///</summary>
        [TestMethod()]
        public void ContextsTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext();
            target.AddContext(ConstId1, ConstValue1);
            target.AddContext(ConstId1, ConstValue2);
            target.AddContext(ConstId2, ConstValue1);
            ILookup<string, object> actual;
            actual = target.Contexts;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count == 2);
            Assert.IsTrue(actual[ConstId1].Count() == 2);
            Assert.AreSame(actual[ConstId1].ElementAt(0), ConstValue1);
            Assert.AreSame(actual[ConstId1].ElementAt(1), ConstValue2);
            Assert.IsTrue(actual[ConstId2].Count() == 1);
            Assert.AreSame(actual[ConstId2].ElementAt(0), ConstValue1);

            target.AddContext(ConstId2, ConstValue2);
            ILookup<string, object> actual2;
            actual2 = target.Contexts;
            Assert.AreNotSame(actual, actual2);
            Assert.IsNotNull(actual2);
            Assert.IsTrue(actual2.Count == 2);
            Assert.IsTrue(actual2[ConstId1].Count() == 2);
            Assert.AreSame(actual2[ConstId1].ElementAt(0), ConstValue1);
            Assert.AreSame(actual2[ConstId1].ElementAt(1), ConstValue2);
            Assert.IsTrue(actual2[ConstId2].Count() == 2);
            Assert.AreSame(actual2[ConstId2].ElementAt(0), ConstValue1);
            Assert.AreSame(actual2[ConstId2].ElementAt(1), ConstValue2);

        }

        /// <summary>
        ///Test pour WarningMessages
        ///</summary>
        [TestMethod()]
        public void WarningMessagesTest()
        {
            ArgumentsParserContext target = new ArgumentsParserContext(); // TODO: initialisez à une valeur appropriée
            IEnumerable<string> actual;
            actual = target.WarningMessages;
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count() == 0);

            string message = Guid.NewGuid().ToString();
            target.AddWarningMessage(message);
            IEnumerable<string> actual2;
            actual2 = target.WarningMessages;
            Assert.AreSame(actual, actual2);
            Assert.IsTrue(actual2.Count() == 1);
            Assert.AreSame(actual2.First(), message);
        }
    }
}
