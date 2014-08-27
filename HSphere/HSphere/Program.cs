using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HSphere.Engine;
using System.Diagnostics;
using System.Threading;

namespace HSphere
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //ViewPort.TestGetLineClipping(true);
            //return;

            Thread.Sleep(1000);
            Stopwatch sw = Stopwatch.StartNew();
            int count = 10000;
            for (int i = 0; i < count; i++)
            {
                ViewPort.TestGetLineClipping(false);

            }
            sw.Stop();
            double perItem = (double)sw.ElapsedMilliseconds / ((double)count * 122) * 1000.0;
            Console.WriteLine(sw.ElapsedMilliseconds + " ms :" + perItem + " ns/perItem ");

            // Release v1 : 140ms
            // Release v2 : 75ms
            // Release v3 : 63ms
            // Release v4 : 53ms
            // Release v5 : 51ms
            // Release v6 : 50ms
            // Release v7 : 48ms

            return;

            // dark empty scene
            Scene scene = new Scene();
            scene.BackgroundColor = Color.Black;
            //scene.Camera.Perspective = false;
            //scene.Camera.SetZoomByScreen(100);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(scene));
        }
    }
}