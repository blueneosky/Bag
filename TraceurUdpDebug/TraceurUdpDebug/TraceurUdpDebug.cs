using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace System.Diagnostics
{
    /// <summary>
    /// Classe TraceurUdpDebug
    /// </summary>
    [Obsolete("Ne pas laisser cette classe dans le produit livré")]
    static public class TraceurUdpDebug
    {
        private const int ConstantePortEnvoie = 9990;
        private static readonly IPAddress ConstanteAdresseEnvoie = IPAddress.Loopback;
        private static bool _doitIndenterTrace = true;

        private static IPEndPoint _endPoint = new IPEndPoint(ConstanteAdresseEnvoie, ConstantePortEnvoie);

        /// <summary>
        /// Envoi le texte vers la fenetre "udpDebugConsole"
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="addPreSaut"></param>
        /// <param name="addPostSaut"></param>
        /// <param name="tagTime"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Feature C#4.0")]
        public static void Tracer(String txt, bool addPreSaut = false, bool addPostSaut = true, bool tagTime = true)	// pour debug: envoi a la console udp // TODO: a virer a la livraison du module
        {
            DateTime dt = DateTime.Now;

            StringBuilder sb = new StringBuilder();

            if (addPreSaut)
            {
                sb.AppendLine();
                _doitIndenterTrace = true;
            }

            if (_doitIndenterTrace)
            {
                sb.Append(ObtenirIndentation());

                if (tagTime)
                {
                    //long t = dt.Ticks;
                    //t = t / 10000; // DateTime.Now.Ticks = ms * 10000; /10000 => obtenir des ms
                    //String dtString = dt.ToLongTimeString();
                    //tagTimeString = string.Format(System.Globalization.CultureInfo.InvariantCulture, "[{0}ms({1})]: ", t, dtString);
                    //String tagTimeString = dt.ToLocalTime().ToString("O") + ": ";   // "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffzzz"
                    String tagTimeString = dt.ToLocalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff", CultureInfo.CurrentCulture) + ": ";
                    sb.Append(tagTimeString);
                }

                _doitIndenterTrace = false;
            }

            sb.Append(txt);

            if (addPostSaut)
            {
                sb.AppendLine();
                _doitIndenterTrace = true;
            }

            String texte = sb.ToString();
            Byte[] buffer = System.Text.Encoding.Unicode.GetBytes(texte);
            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.Send(buffer, buffer.Length, _endPoint);
            }
        }

        /// <summary>
        /// Envoie la pile des appels vers la fenêtre "udpDebugConsole"
        /// </summary>
        /// <param name="message"></param>
        /// <param name="watch"></param>
        public static void Tracer(string message, Stopwatch watch)
        {
            Debug.Assert(watch != null);
            Tracer(message, watch.Elapsed);
        }

        /// <summary>
        /// Envoie la pile des appels vers la fenêtre "udpDebugConsole"
        /// </summary>
        /// <param name="message"></param>
        /// <param name="temps"></param>
        public static void Tracer(string message, TimeSpan temps)
        {
            Tracer(message + " : " + Math.Round(temps.TotalMilliseconds) + "ms");
        }

        /// <summary>
        /// Envoie la liste de messages la fenetre "udpDebugConsole"
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="tagTime"></param>
        public static void Tracer(IEnumerable<string> messages, bool tagTime = true)
        {
            foreach (string message in messages)
            {
                Tracer(message, tagTime: tagTime);
            }
        }

        /// <summary>
        /// Envoie un ensemble de mesure avec stat vers la fenêtre "udpDebugConsole"
        /// </summary>
        /// <param name="tempsMesures"></param>
        /// <param name="preMessage"></param>
        /// <param name="avecDetail"></param>
        /// <param name="tagTime"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Feature C#4.0")]
        public static void Tracer(IEnumerable<TimeSpan> tempsMesures, string preMessage = null, bool avecDetail = true, bool tagTime = true)
        {
            IEnumerable<string> messages = ObtenirTrace(tempsMesures, preMessage, avecDetail);
            Tracer(messages, tagTime: tagTime);
        }

        /// <summary>
        /// Envoie la pile des appels vers la fenêtre "udpDebugConsole"
        /// </summary>
        /// <param name="commentaire"></param>
        /// <param name="maxFrames">-1 pour pas de limite</param>
        /// <param name="avecIndentation"></param>
        /// <param name="tagTime"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Feature C#4.0")]
        public static void TracerPileDAppels(string commentaire, int maxFrames = -1, bool avecIndentation = true, bool tagTime = true)
        {
            string texte = "Pile ";
            if (false == String.IsNullOrWhiteSpace(commentaire))
                texte += "(" + commentaire + ") ";
            texte += ":";
            Tracer(texte, false, true, tagTime);

            StackTrace stackTrace = new StackTrace(1, true);
            IEnumerable<StackFrame> stackFrames = stackTrace.GetFrames();
            if (maxFrames > 0)
                stackFrames = stackFrames.Take(maxFrames);
            if (avecIndentation)
                Indenter();
            foreach (StackFrame stackFrame in stackFrames)
            {
                Tracer(stackFrame.ToString(), false, false, false);
            }
            if (avecIndentation)
                Desindenter();
        }

        private static IEnumerable<string> ObtenirTrace(IEnumerable<TimeSpan> tempsMesures, string preMessage, bool avecDetail)
        {
            preMessage = String.IsNullOrEmpty(preMessage) ? String.Empty : String.Concat(preMessage, " ");

            IEnumerable<double?> tempsMsMesures = tempsMesures
                .Select(ts => (double?)ts.TotalMilliseconds)
                .ToArray();

            if (avecDetail)
            {
                foreach (double? ms in tempsMsMesures)
                {
                    string message = String.Concat(preMessage, "{ Temps : ", ms, "ms }");
                    yield return message;
                }
            }

            int count = tempsMsMesures.Count();
            double sum = tempsMsMesures.Sum() ?? 0;
            string stat = String.Format(
                preMessage + "Summary : count({0}), min({1}ms), max({2}ms), moy({3}ms), med({4}ms), total({5}ms)"
                , count
                , Math.Round(tempsMsMesures.Min() ?? 0)
                , Math.Round(tempsMsMesures.Max() ?? 0)
                , Math.Round(sum / (double)Math.Max(1, count))
                , Math.Round(tempsMsMesures.OrderBy(i => i).ElementAtOrDefault(count / 2) ?? 0)
                , Math.Round(sum)
                );
            yield return stat;
        }

        #region Variables temporaires

        private static Dictionary<string, object> _variableParNoms = new Dictionary<string, object> { };

        /// <summary>
        /// Definirs the variable.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="nom">The nom.</param>
        /// <param name="valeur">The valeur.</param>
        public static void DefinirVariable<TType>(string nom, TType valeur)
        {
            _variableParNoms[nom] = valeur;
        }

        /// <summary>
        /// Obtenirs the valeur.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="nom">The nom.</param>
        /// <returns></returns>
        public static TType ObtenirValeur<TType>(string nom)
        {
            TType retour = default(TType);

            object valeur;
            if (_variableParNoms.TryGetValue(nom, out valeur))
            {
                retour = (TType)valeur;
            }

            return retour;
        }

        #endregion Variables temporaires

        #region Indentation

        private static int _compteurIndentation = 0;

        private static string ConstanteIndentation0 = String.Empty;

        private static string ConstanteIndentation1 = ConstanteIndentation0 + "    ";

        private static string ConstanteIndentation2 = ConstanteIndentation1 + ConstanteIndentation1;

        private static string ConstanteIndentation3 = ConstanteIndentation2 + ConstanteIndentation1;

        private static string ConstanteIndentation4 = ConstanteIndentation3 + ConstanteIndentation1;

        /// <summary>
        /// Desindente la trace.
        /// </summary>
        public static void Desindenter()
        {
            _compteurIndentation--;
            if (_compteurIndentation < 0)
                _compteurIndentation = 0;
        }

        /// <summary>
        /// Indente la trace.
        /// </summary>
        public static void Indenter()
        {
            _compteurIndentation++;
        }

        private static string ObtenirIndentation()
        {
            string result;

            switch (_compteurIndentation)
            {
                case 0:
                    result = ConstanteIndentation0;
                    break;

                case 1:
                    result = ConstanteIndentation1;
                    break;

                case 2:
                    result = ConstanteIndentation2;
                    break;

                case 3:
                    result = ConstanteIndentation3;
                    break;

                case 4:
                    result = ConstanteIndentation4;
                    break;

                default:
                    result = ConstanteIndentation0;
                    for (int i = 0; i < _compteurIndentation; i++)
                    {
                        result += ConstanteIndentation1;
                    }
                    break;
            }

            return result;
        }

        #endregion Indentation

        #region Ecouteur de Debug et Trace

        private static bool _estEcouteActive;

        /// <summary>
        /// Place des écouteurs pour Tracer Debug et Trace
        /// </summary>
        /// <param name="tagTime"></param>
        public static void EcouterDebugEtTrace(bool tagTime = true)
        {
            if (_estEcouteActive)
                return;
            _estEcouteActive = true;

            TraceListener traceListener = new TraceurUdpDebugTraceListener(tagTime);
            Debug.Listeners.Add(traceListener);
            Trace.Listeners.Add(traceListener);
        }

        #region TraceurUdpDebugTraceListener

        private class TraceurUdpDebugTraceListener : TraceListener
        {
            private readonly bool _tagTime;

            public TraceurUdpDebugTraceListener(bool tagTime)
            {
                _tagTime = tagTime;
            }

            public override void Write(string message)
            {
                TraceurUdpDebug.Tracer(message, addPostSaut: false, tagTime: _tagTime);
            }

            public override void WriteLine(string message)
            {
                TraceurUdpDebug.Tracer(message, tagTime: _tagTime);
            }
        }

        #endregion TraceurUdpDebugTraceListener

        #endregion Ecouteur de Debug et Trace
    }
}