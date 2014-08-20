using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("Target")]
    public class XmlParamDescriptionHeoTarget : XmlParamDescription<FastBuildGen.BusinessModel.IParamDescriptionHeoTarget>
    {
        public XmlParamDescriptionHeoTarget()
        {
        }

        //[XmlArray("Dependencies")]
        [XmlElement("Module")]
        public XmlId[] Xml04IdDependencies { get; set; }

        internal IEnumerable<XmlParamDescriptionHeoModule> Dependencies
        {
            get
            {
                return (Xml04IdDependencies ?? new XmlId[0])
                    .Select(Session.GetXmlObjectId<XmlParamDescriptionHeoModule>);
            }
        }

        internal bool Equals(BusinessModel.IParamDescriptionHeoTarget target)
        {
            IEnumerable<XmlParamDescriptionHeoModule> xmlDependencies = Dependencies.ToArray();  // execute more than once

            bool result = base.Equals(target)
                && (xmlDependencies.Count() == target.Dependencies.Count());

            result = result && target.Dependencies
                .GroupJoin(
                    xmlDependencies
                    , (module) => module.Name
                    , (xmlModule) => xmlModule.Name
                    , (module, xmlModules) => (xmlModules.Count() == 1) && (xmlModules.First().Equals(module))
                )
                .All(s => s);

            return result;
        }

        protected override void CopyToCore(BusinessModel.IParamDescriptionHeoTarget instance)
        {
            base.CopyToCore(instance);

            instance.ClearDependencies();

            IEnumerable<BusinessModel.IParamDescriptionHeoModule> dependencies = (Xml04IdDependencies ?? new XmlId[0])
                    .Select(xmlId => Session.GetInstance(xmlId))
                    .OfType<FastBuildGen.BusinessModel.IParamDescriptionHeoModule>();
            instance.AddDependencies(dependencies);
        }

        protected override void DeserializeCore()
        {
            base.DeserializeCore();

            // nothing
        }

        protected override void SerializeCore(FastBuildGen.BusinessModel.IParamDescriptionHeoTarget instance)
        {
            base.SerializeCore(instance);

            Xml04IdDependencies = instance.Dependencies
                .Select(Session.GetOrCreateXmlParamDescriptionHeoModule)
                .Select(xmlObjectId => xmlObjectId.XmlId)
                .ToArray();
        }
    }
}