using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SkyrimUserSwitch.Config
{
    [Serializable]
    [XmlRoot("User")]
    public class UserXml
    {
        #region ctor

        public UserXml()
        {
        }

        public UserXml(Model.User user)
        {
            Name = user.Name;
            Avatar = user.Avatar;
        }

        #endregion ctor

        #region Properties

        [XmlIgnore]
        public Image Avatar { get; set; }

        [XmlIgnore]
        public MemoryStream ControlMap_CustomTxtContents { get; set; }

        [XmlIgnore]
        public string Name { get; set; }

        [XmlIgnore]
        public MemoryStream SkyrimIniContents { get; set; }

        [XmlIgnore]
        public MemoryStream SkyrimPrefsIniContents { get; set; }

        #region Serialized

        [XmlElement("Avatar")]
        public string XmlB64Avatar
        {
            get { return Base64.ImageToBase64String(Avatar, ImageFormat.Png); }
            set { Avatar = Base64.ImageFromBase64String(value); }
        }

        [XmlElement("ControlMap_CustomTxtContents")]
        public string XmlB64ControlMap_CustomTxtContents
        {
            get { return Base64.StreamToBase64String(ControlMap_CustomTxtContents); }
            set { ControlMap_CustomTxtContents = Base64.StreamFromBase64String(value); }
        }

        [XmlElement("SkyrimIniContents")]
        public string XmlB64SkyrimIniContents
        {
            get { return Base64.StreamToBase64String(SkyrimIniContents); }
            set { SkyrimIniContents = Base64.StreamFromBase64String(value); }
        }

        [XmlElement("SkyrimPrefsIniContents")]
        public string XmlB64SkyrimPrefsIniContents
        {
            get { return Base64.StreamToBase64String(SkyrimPrefsIniContents); }
            set { SkyrimPrefsIniContents = Base64.StreamFromBase64String(value); }
        }

        [XmlAttribute("Name")]
        public string XmlName
        {
            get { return Name; }
            set { Name = value; }
        }

        #endregion Serialized

        #endregion Properties

        #region Comparison

        public bool Equals(UserXml userXml, bool withContents)
        {
            if (userXml == null)
                return false;

            bool isEquals = (this.Name == userXml.Name)
                && SameAvatar(this.Avatar, userXml.Avatar);
            if (isEquals && withContents)
            {
                isEquals = SameContents(this.ControlMap_CustomTxtContents, userXml.ControlMap_CustomTxtContents)
                    && SameContents(this.SkyrimIniContents, userXml.SkyrimIniContents)
                    && SameContents(this.SkyrimPrefsIniContents, userXml.SkyrimPrefsIniContents);
            }

            return isEquals;
        }

        private bool SameAvatar(Image x, Image y)
        {
            if (x == y)
                return true;

            if ((x == null) || (y == null))
                return false;

            int xWidth = x.Width;
            int yWidth = y.Width;
            int xHeight = x.Height;
            int yHeight = y.Height;
            if ((xWidth != yWidth) || (xHeight != yHeight))
                return false;

            bool isEquals = false;
            using (Bitmap xBitmap = new Bitmap(x))
            using (Bitmap yBitmap = new Bitmap(y))
            {
                BitmapData xBitmapData = xBitmap.LockBits(new Rectangle(0, 0, xWidth, xHeight), ImageLockMode.ReadOnly, xBitmap.PixelFormat);
                BitmapData yBitmapData = yBitmap.LockBits(new Rectangle(0, 0, yWidth, yHeight), ImageLockMode.ReadOnly, yBitmap.PixelFormat);

                int xSize = xBitmapData.Stride * xBitmapData.Height;
                int ySize = yBitmapData.Stride * yBitmapData.Height;
                isEquals = (xSize == ySize);

                if (isEquals)
                {
                    byte[] xData = new byte[xSize];
                    byte[] yData = new byte[ySize];
                    System.Runtime.InteropServices.Marshal.Copy(xBitmapData.Scan0, xData, 0, xSize);
                    System.Runtime.InteropServices.Marshal.Copy(yBitmapData.Scan0, yData, 0, ySize);

                    for (int i = 0; (i < xSize) && isEquals; i++)
                    {
                        isEquals = (xData[i] == yData[i]);
                    }
                }

                xBitmap.UnlockBits(xBitmapData);
                yBitmap.UnlockBits(yBitmapData);
            }

            return isEquals;
        }

        private bool SameContents(MemoryStream x, MemoryStream y)
        {
            if (x == null)
                return false;

            return x.SameContents(y);
        }

        #endregion Comparison
    }
}