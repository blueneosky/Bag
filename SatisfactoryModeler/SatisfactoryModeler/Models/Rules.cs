using SatisfactoryModeler.Properties;
using System.IO;
using System.Text;

namespace SatisfactoryModeler.Persistance.Configurations
{
    public class Rules
    {
        public static Rules Instance { get; } = new Rules();

        public CfgConfiguration Configuration { get; }

        private Rules()
        {
            // extract configuration
            this.Configuration = PersistanceFactory.Instance.Get<CfgConfiguration>("XML")
                .Restore(new MemoryStream(Encoding.UTF8.GetBytes(Resources.Configuration))); ;
        }

        public CfgItemType[] ItemTypes => Configuration.ItemTypes;
        public CfgFluideType[] FluideTypes => Configuration.FluideTypes;
    }
}
