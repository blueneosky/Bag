using SatisfactoryModeler.Properties;
using Splat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Assets
{
    public class IconsManager
    {
        public static IconsManager Current = new IconsManager();

        private IconsManager()
        {
        }

        private static IBitmap Convert(Bitmap bitmap)
        {
            using (var buffer = new MemoryStream())
            {
                bitmap.Save(buffer, ImageFormat.Png);
                buffer.Position = 0;
                var loading = BitmapLoader.Current.Load(buffer, null, null);
                loading.Wait();
                return loading.Result;
            }
        }

        private Lazy<IBitmap> _minerCache = new Lazy<IBitmap>(() => Convert(Resources.Miner_Mk_3));
        public IBitmap Miner => _minerCache.Value;
    }
}
