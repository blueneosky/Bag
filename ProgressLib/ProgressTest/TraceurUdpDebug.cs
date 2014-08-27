// TODO (JMP) : supprimer ce code à la livraison
#define TRACEUR_UDP_DEBUG

#if TRACEUR_UDP_DEBUG && DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Heo.Systeme.Service.Performance
{
    /// <summary>
    /// Classe TraceurUdpDebug
    /// </summary>   
    [Obsolete("Ne pas laisser cette classe dans le produit livré")]
    static public class TraceurUdpDebug
    {

        private static readonly IPAddress ConstanteAdresseEnvoie = IPAddress.Loopback;
        private const int ConstantePortEnvoie = 9990;

        static UdpClient udpClient;
        static IPEndPoint endPoint;

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
            // usage: tempClmSendUdp(string [, true[, false]]);
            long t = DateTime.Now.Ticks;
            t = t / 10000; // DateTime.Now.Ticks = ms * 10000; /10000 => obtenir des ms
            //long t2 = t / 1000;
            DateTime dt = DateTime.Now;
            String dtString = dt.ToLongTimeString();

            String addPreSautString = String.Empty;
            String addPostSautString = String.Empty;
            if (addPreSaut)
            {
                addPreSautString = "\n";
            }
            if (addPostSaut)
            {
                addPostSautString = "\n";
            }
            String tagTimeString = String.Empty;
            if (tagTime)
            {
                tagTimeString = string.Format(System.Globalization.CultureInfo.InvariantCulture, "[{0}ms({1})]: ", t, dtString);
            }

            if (udpClient == null)
            {
                endPoint = new IPEndPoint(ConstanteAdresseEnvoie, ConstantePortEnvoie);
                udpClient = new System.Net.Sockets.UdpClient();
            }

            String tagTxt = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}{2}{3}", addPreSautString, tagTimeString, txt, addPostSautString);

            Byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(tagTxt);

            udpClient.Send(sendBytes, sendBytes.Length, endPoint);
        }

        /// <summary>
        /// Envoie la pile des appels vers la fenêtre "udpDebugConsole"
        /// </summary>
        /// <param name="commentaire"></param>
        /// <param name="maxFrames">-1 pour pas de limite</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Feature C#4.0")]
        public static void TracerPileDAppels(string commentaire, int maxFrames = -1)
        {
            string texte = "Pile ";
            if (false == String.IsNullOrWhiteSpace(commentaire))
                texte += "(" + commentaire + ") ";
            texte += ":";
            Tracer(texte, true, true, true);

            StackTrace stackTrace = new StackTrace(1, true);
            IEnumerable<StackFrame> stackFrames = stackTrace.GetFrames();
            if (maxFrames > 0)
                stackFrames = stackFrames.Take(maxFrames);
            foreach (StackFrame stackFrame in stackFrames)
            {
                Tracer("\t" + stackFrame.ToString(), false, false, false);
            }
        }

    }
}
#endif
