using System;
using System.IO;
using System.Xml.Serialization;

namespace SatisfactoryModeler.Persistance
{
    public class XmlPersistaanceEngine<T> : IPersistanceEngine<T>
    {
        private XmlSerializer CurrentXmlSerializer { get; } = new XmlSerializer(typeof(T));

        public T Restore(Stream stream)
            => CurrentXmlSerializer.Deserialize(stream).CastTo<T>();

        public void Store(Stream stream, T o)
            => CurrentXmlSerializer.Serialize(stream, o);
    }
}
