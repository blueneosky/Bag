using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace System
{
    public static class Base64
    {
        private const Base64FormattingOptions ConstBase64FormattingOptions = Base64FormattingOptions.InsertLineBreaks;

        public static Image ImageFromBase64String(string base64String)
        {
            if (String.IsNullOrEmpty(base64String))
                return null;

            Image result = null;
            try
            {
                byte[] imageBuffer = Convert.FromBase64String(base64String);
                using (MemoryStream stream = new MemoryStream(imageBuffer))
                {
                    result = Image.FromStream(stream);
                }
            }
            catch (Exception) { }

            return result;
        }

        public static string ImageToBase64String(Image image, ImageFormat imageFormat)
        {
            if (image == null)
                return null;

            string result = null;

            try
            {
                byte[] imageBuffer = null;
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, imageFormat);

                    imageBuffer = stream.ToArray();
                }
                result = Convert.ToBase64String(imageBuffer, ConstBase64FormattingOptions);
            }
            catch (Exception) { }

            return result;
        }

        public static MemoryStream StreamFromBase64String(string base64String)
        {
            if (String.IsNullOrEmpty(base64String))
                return null;

            MemoryStream result = null;
            try
            {
                byte[] buffer = Convert.FromBase64String(base64String);
                result = new MemoryStream(buffer);
            }
            catch (Exception) { }

            return result;
        }

        public static string StreamToBase64String(Stream stream)
        {
            if (stream == null)
                return null;

            string result = null;

            try
            {
                byte[] buffer = null;
                using (MemoryStream streamBuffer = new MemoryStream())
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(streamBuffer);

                    buffer = streamBuffer.ToArray();
                }
                result = Convert.ToBase64String(buffer, ConstBase64FormattingOptions);
            }
            catch (Exception) { }

            return result;
        }
    }
}