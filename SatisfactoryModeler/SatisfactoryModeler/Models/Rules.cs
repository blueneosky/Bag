using SatisfactoryModeler.Models;
using SatisfactoryModeler.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SatisfactoryModeler.Persistance.Configurations
{
    public class Rules
    {
        public static Rules Instance { get; } = new Rules();

        public CfgConfiguration Configuration { get; }

        private readonly Lazy<FluideType> _oilExtractingFluideTypeCache;
        private readonly Lazy<FluideType> _waterExtractingFluideTypeCache;

        private Rules()
        {
            // extract configuration
            this.Configuration = PersistanceFactory.Instance.Get<CfgConfiguration>("XML")
                .Restore(new MemoryStream(Encoding.UTF8.GetBytes(Resources.Configuration)));

            // some cache value (or post loaded)
            this._oilExtractingFluideTypeCache = new Lazy<FluideType>(() => FluideType.ById(this.Configuration.OilExtractingFluideType));
            this._waterExtractingFluideTypeCache = new Lazy<FluideType>(() => FluideType.ById(this.Configuration.WaterExtractingFluideType));
        }

        public IEnumerable<ItemType> ItemTypes => ItemType.All;
        public IEnumerable<FluideType> FluideTypes => FluideType.All;
        public IEnumerable<MiningOreType> MiningOreTypes => MiningOreType.All;
        public FluideType OilExtractingFluideType => _oilExtractingFluideTypeCache.Value;
        public FluideType WaterExtractingFluideType => _waterExtractingFluideTypeCache.Value;
    }
}
