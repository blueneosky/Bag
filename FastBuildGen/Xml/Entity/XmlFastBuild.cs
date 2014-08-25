using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("FastBuild")]
    public class XmlFastBuild : XmlObjectId<IFastBuildModel>
    {
        public XmlFastBuild()
        {
        }

        [XmlElement("Param")]
        public XmlFastBuildParam Xml01FastBuildParam { get; set; }

        [XmlElement("InternalVar")]
        public XmlFastBuildInternalVar Xml02FastBuildInternalVar { get; set; }

        [XmlElement("WithEchoOff")]
        public bool Xml03WithEchoOff { get; set; }

        internal bool Equals(IFastBuildModel model)
        {
            bool result = (model != null)
                && (Xml01FastBuildParam != null) && Xml01FastBuildParam.Equals(model.FastBuildParamModel)
                && (Xml02FastBuildInternalVar != null) && Xml02FastBuildInternalVar.Equals(model.FastBuildInternalVarModel);

            return result;
        }

        protected override void CopyToCore(IFastBuildModel instance)
        {
            if (Xml01FastBuildParam != null)
                Xml01FastBuildParam.CopyTo(instance.FastBuildParamModel);
            if (Xml02FastBuildInternalVar != null)
                Xml02FastBuildInternalVar.CopyTo(instance.FastBuildInternalVarModel);
            instance.WithEchoOff = Xml03WithEchoOff;
        }

        protected override void DeserializeCore()
        {
            if (Xml01FastBuildParam != null)
                Session.Deserialize(Xml01FastBuildParam);
            if (Xml02FastBuildInternalVar != null)
                Session.Deserialize(Xml02FastBuildInternalVar);
        }

        protected override void SerializeCore(IFastBuildModel instance)
        {
            Xml01FastBuildParam = Session.GetOrCreateXmlFastBuildParam(instance.FastBuildParamModel);
            Xml02FastBuildInternalVar = Session.GetOrCreateXmlFastBuildInternalVar(instance.FastBuildInternalVarModel);
            Xml03WithEchoOff = instance.WithEchoOff;
        }
    }
}