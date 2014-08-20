using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastBuildGen.Xml
{
    public interface IXmlObjectId
    {
        XmlId XmlId { get; set; }
    }

    public interface IXmlObjectId<in TInstance> : IXmlObjectId
        where TInstance : class
    {
        void CopyTo(TInstance instance);

        void Deserialize(XmlSession session);

        void Serialize(TInstance instance, XmlSession session);
    }
}