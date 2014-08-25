using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("Param")]
    public class XmlFastBuildParam : XmlObjectId<BusinessModel.Old.IFastBuildParamModel>
    {
        public XmlFastBuildParam()
        {
        }

        [XmlArray("Modules")]
        public XmlParamDescriptionHeoModule[] Xml01Modules { get; set; }

        [XmlArray("Targets")]
        public XmlParamDescriptionHeoTarget[] Xml02Targets { get; set; }

        internal bool Equals(BusinessModel.Old.IFastBuildParamModel model)
        {
            bool result = (model != null)
                && (Xml01Modules != null) && (Xml01Modules.Count() == model.HeoModuleParams.Count())
                && (Xml02Targets != null) && (Xml02Targets.Count() == model.HeoTargetParams.Count());

            // grouping by name (considered as key)

            result = result && model.HeoModuleParams
                .GroupJoin(
                    (Xml01Modules ?? new XmlParamDescriptionHeoModule[0])
                    , module => module.Name
                    , xmlModule => xmlModule.Name
                    , (module, xmlModules) => (xmlModules.Count() == 1) && (xmlModules.First().Equals(module))
                )
                .All(s => s);

            result = result && model.HeoTargetParams
                .GroupJoin(
                    (Xml02Targets ?? new XmlParamDescriptionHeoTarget[0])
                    , target => target.Name
                    , xmlTarget => xmlTarget.Name
                    , (target, xmlTargets) => (xmlTargets.Count() == 1) && (xmlTargets.First().Equals(target))
                )
                .All(s => s);

            return result;
        }

        protected override void CopyToCore(BusinessModel.Old.IFastBuildParamModel instance)
        {
            instance.ClearHeoTargetParams();
            instance.ClearHeoModuleParams();

            foreach (XmlParamDescriptionHeoModule xmlModule in (Xml01Modules ?? new XmlParamDescriptionHeoModule[0]))
            {
                BusinessModel.Old.IParamDescriptionHeoModule module = instance.AddHeoModuleParam(xmlModule.Name, xmlModule.Keyword);
                xmlModule.CopyTo(module);
            }
            foreach (XmlParamDescriptionHeoTarget xmlTarget in (Xml02Targets ?? new XmlParamDescriptionHeoTarget[0]))
            {
                BusinessModel.Old.IParamDescriptionHeoTarget target = instance.AddHeoTargetParam(xmlTarget.Name, xmlTarget.Keyword);
                xmlTarget.CopyTo(target);
            }
        }

        protected override void DeserializeCore()
        {
            XmlSession xmlSession = Session;

            foreach (XmlParamDescriptionHeoModule xmlModule in (Xml01Modules ?? new XmlParamDescriptionHeoModule[0]))
            {
                xmlSession.Deserialize(xmlModule);
            }
            foreach (XmlParamDescriptionHeoTarget xmlTarget in (Xml02Targets ?? new XmlParamDescriptionHeoTarget[0]))
            {
                xmlSession.Deserialize(xmlTarget);
            }
        }

        protected override void SerializeCore(BusinessModel.Old.IFastBuildParamModel instance)
        {
            Xml01Modules = instance.HeoModuleParams
                .Select(m => Session.GetOrCreateXmlParamDescriptionHeoModule(m))
                .ToArray();
            Xml02Targets = instance.HeoTargetParams
                .Select(t => Session.GetOrCreateXmlParamDescriptionHeoTarget(t))
                .ToArray();
        }
    }
}