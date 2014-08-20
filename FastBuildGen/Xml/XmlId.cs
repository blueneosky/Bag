using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastBuildGen.Common;

namespace FastBuildGen.Xml
{
    [Serializable]
    [XmlType("XmlId")]
    public class XmlId
    {
        private int? _idCache;

        public XmlId()
        {
        }

        public XmlId(int id)
        {
            Id = id;
        }

        [XmlIgnore]
        public int Id
        {
            get
            {
                if (null == _idCache)
                    _idCache = Convert(Xml01Id);
                return _idCache.Value;
            }
            private set
            {
                _idCache = value;
                Xml01Id = Convert(value);
            }
        }

        [XmlAttribute("RefId")]
        public string Xml01Id { get; set; }

        #region override

        public override bool Equals(object obj)
        {
            XmlId xmlId = obj as XmlId;
            if (xmlId == null)
                return false;

            return xmlId == this;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Convert(Id);
        }

        #endregion override

        #region Convert

        private static int Convert(string value)
        {
            bool success = false;
            int result = -1;

            if (value.StartsWith("#"))
            {
                value = value.Substring(1);
                success = Int32.TryParse(value, out result);
            }

            if (!success)
                throw new FastBuildGenException("Invalide #Id");

            return result;
        }

        private static string Convert(int value)
        {
            return "#" + value;
        }

        #endregion Convert

        #region operator

        public static explicit operator int(XmlId value)
        {
            if (value == null)
                return -1;
            return value.Id;
        }

        public static explicit operator string(XmlId value)
        {
            if (value == null)
                return null;
            return Convert(value.Id);
        }

        public static explicit operator XmlId(int value)
        {
            return new XmlId(value);
        }

        public static explicit operator XmlId(string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return new XmlId(Convert(value));
        }

        public static bool operator !=(XmlId xmlId1, XmlId xmlId2)
        {
            return !(xmlId1 == xmlId2);
        }

        public static bool operator ==(XmlId xmlId1, XmlId xmlId2)
        {
            if (Object.ReferenceEquals(xmlId1, xmlId2))
                return true;

            if (Object.ReferenceEquals(xmlId1, null) || Object.ReferenceEquals(xmlId2, null))
                return false;

            return xmlId1.Id == xmlId2.Id;
        }

        #endregion operator
    }
}