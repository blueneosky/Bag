using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("Target")]
    public class XmlParamDescriptionHeoTarget : XmlParamDescription<IParamDescriptionHeoTarget>
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

        internal bool Equals(IParamDescriptionHeoTarget target)
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

        protected override void CopyToCore(IParamDescriptionHeoTarget instance)
        {
            base.CopyToCore(instance);

            instance.ClearDependencies();

            IEnumerable<IParamDescriptionHeoModule> dependencies = (Xml04IdDependencies ?? new XmlId[0])
                    .Select(xmlId => Session.GetInstance(xmlId))
                    .OfType<IParamDescriptionHeoModule>();
            instance.AddDependencies(dependencies);
        }

        protected override void DeserializeCore()
        {
            base.DeserializeCore();

            // nothing
        }

        protected override void SerializeCore(IParamDescriptionHeoTarget instance)
        {
            base.SerializeCore(instance);

            Xml04IdDependencies = instance.Dependencies
                .Select(Session.GetOrCreateXmlParamDescriptionHeoModule)
                .Select(xmlObjectId => xmlObjectId.XmlId)
                .ToArray();
        }
    }
}