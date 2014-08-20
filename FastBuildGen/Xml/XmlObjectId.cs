using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastBuildGen.Common;

namespace FastBuildGen.Xml
{
    public abstract class XmlObjectId<TInstance> : IXmlObjectId<TInstance>
        where TInstance : class
    {
        private bool _isXmlIdCached;
        private XmlId _xmlIdCache;
        private bool? isSerialized;

        public XmlObjectId()
        {
        }

        [XmlAttribute("XmlId")]
        public string Xml00Id { get; set; }

        [XmlIgnore]
        public XmlId XmlId
        {
            get
            {
                if (_isXmlIdCached == false)
                {
                    _xmlIdCache = (XmlId)Xml00Id;
                    _isXmlIdCached = true;
                }
                return _xmlIdCache;
            }
            set
            {
                _isXmlIdCached = false;
                Xml00Id = (string)value;
            }
        }

        protected XmlSession Session { get; private set; }

        public void CopyTo(TInstance instance)
        {
            if (isSerialized == null)
                throw new FastBuildGenException("Need to be Deserializes first");

            if (true == isSerialized)
                throw new FastBuildGenException("Mixed mode not allowed");

            Session.Register(this.XmlId, instance);
            CopyToCore(instance);
        }

        public void Deserialize(XmlSession session)
        {
            if (true == isSerialized)
                throw new FastBuildGenException("Mixed mode not allowed");

            if (isSerialized.HasValue)
                return; // already done
            isSerialized = false;

            Session = session;
            Session.Register(this);
            DeserializeCore();
        }

        public void Serialize(TInstance instance, XmlSession session)
        {
            if (false == isSerialized)
                throw new FastBuildGenException("Mixed mode not allowed");

            if (isSerialized.HasValue)
                return; // already done
            isSerialized = true;

            Session = session;
            SerializeCore(instance);
        }

        protected abstract void CopyToCore(TInstance instance);

        protected abstract void DeserializeCore();

        protected abstract void SerializeCore(TInstance instance);
    }
}