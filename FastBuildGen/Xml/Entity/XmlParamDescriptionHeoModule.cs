using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("Module")]
    public class XmlParamDescriptionHeoModule : XmlParamDescription<FastBuildGen.BusinessModel.Old.IParamDescriptionHeoModule>
    {
        private static EnumConverter ConstEnumPlatformConverter = new EnumConverter(typeof(BusinessModel.Old.EnumPlatform));

        public XmlParamDescriptionHeoModule()
        {
        }

        [XmlIgnore]
        public string MSBuildTarget
        {
            get { return Xml05MSBuildTarget; }
        }

        public BusinessModel.Old.EnumPlatform Platform
        {
            get { return (BusinessModel.Old.EnumPlatform)ConstEnumPlatformConverter.ConvertFromString(Xml04Platform); }
        }

        [XmlAttribute("Platform")]
        public string Xml04Platform { get; set; }

        [XmlAttribute("Target")]
        public string Xml05MSBuildTarget { get; set; }

        internal bool Equals(BusinessModel.Old.IParamDescriptionHeoModule module)
        {
            bool result = base.Equals(module)
                && module.Platform == Platform
                && module.MSBuildTarget == MSBuildTarget;
            ;

            return result;
        }

        protected override void CopyToCore(BusinessModel.Old.IParamDescriptionHeoModule instance)
        {
            base.CopyToCore(instance);

            instance.Platform = Platform;
            instance.MSBuildTarget = MSBuildTarget;
        }

        protected override void DeserializeCore()
        {
            base.DeserializeCore();

            // nothing
        }

        protected override void SerializeCore(BusinessModel.Old.IParamDescriptionHeoModule instance)
        {
            base.SerializeCore(instance);

            Xml04Platform = ConstEnumPlatformConverter.ConvertToString(instance.Platform);
            Xml05MSBuildTarget = instance.MSBuildTarget;
        }
    }
}