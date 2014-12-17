using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using FastBuildGen.Common;

namespace FastBuildGen.Control.Faq
{
    internal class FaqModel : INotifyPropertyChanged
    {
        #region Members

        private string _sourcePage;

        #endregion Members

        #region ctor

        public FaqModel()
        {
            List<Tuple<string, string>> qaList = new List<Tuple<string, string>> { };
            using (TextReader reader = new StringReader(Properties.Resources.Faq))
            {
                while (true)
                {
                    string questionLine = reader.ReadLine();
                    string answerLine = reader.ReadLine();
                    string blankLine = reader.ReadLine();

                    if (String.IsNullOrEmpty(answerLine))
                        break;

                    qaList.Add(Tuple.Create(questionLine, answerLine));
                }
            }

            StringBuilder builder = new StringBuilder();

            // Head and body start
            builder
                .AppendLine("<!DOCTYPE html>")
                .AppendLine("<html>")
                .AppendLine("<head><title>FAQ</title></head>")
                .AppendLine("<body>")
                ;

            // Questions
            builder
                .AppendLine("<p>")
                .AppendLine("<h1>Questions</h1>")
                ;
            for (int i = 0; i < qaList.Count; i++)
            {
                builder
                    .AppendFormat("<b><a href=\"#{0}\">{1}</a></b><br>", i, XmlEscape(qaList[i].Item1))
                    .AppendLine()
                    ;
            }
            builder.AppendLine("</p><p /><hr />");

            // Answers
            builder.AppendLine("<h1>Réponses</h1>");
            for (int i = 0; i < qaList.Count; i++)
            {
                builder
                    .AppendFormat("<h2 id={0}>{1}</h2>", i, XmlEscape(qaList[i].Item1))
                    .AppendLine()
                    .AppendLine("<p>")
                    .AppendFormat(XmlEscape(qaList[i].Item2))
                    .AppendLine("</p>")
                    ;
            }

            // body close
            builder
                .AppendLine("</body>")
                .AppendLine("</html>")
                ;

            SourcePage = builder.ToString();
        }

        #endregion ctor

        #region Properties

        public string SourcePage
        {
            get { return _sourcePage; }
            set
            {
                _sourcePage = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(ConstFaqModelEvent.ConstSourcePage));
            }
        }

        #endregion Properties

        #region Methods

        private static string XmlEscape(string unescaped)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node = doc.CreateElement("root");
            node.InnerText = unescaped;
            return node.InnerXml;
        }

        #endregion Methods

        #region INotifyPropertyChanged Membres

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notify(sender, e);
        }

        #endregion INotifyPropertyChanged Membres
    }
}