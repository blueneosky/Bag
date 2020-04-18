using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace SatisfactoryModeler.Persistance
{
    public class JsonPersistanceEngine<T> : IPersistanceEngine<T>
    {
        private static JsonSerializer DefaultSerializer { get; }

        static JsonPersistanceEngine()
        {
            DefaultSerializer = JsonSerializer.Create(
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented,
                    ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                });
            DefaultSerializer.Converters.Add(new StringEnumConverter());
        }

        public T Restore(Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                return DefaultSerializer.Deserialize<T>(reader);
            }
        }

        public void Store(Stream stream, T o)
        {
            using (var writer = new JsonTextWriter(new StreamWriter(stream)))
            {
                DefaultSerializer.Serialize(writer, o, typeof(T));
            }
        }
    }
}
