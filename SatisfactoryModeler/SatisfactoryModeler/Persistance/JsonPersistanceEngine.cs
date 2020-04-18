using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Stream Store(T root)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            {
                Store(root, writer);
            }
            stream.Position = 0;

            return stream;
        }

        private void Store(T root, TextWriter textWriter)
        {
            using (JsonTextWriter writer = new JsonTextWriter(textWriter))
            {
                DefaultSerializer.Serialize(writer, root, typeof(T));
            }
        }

        public void StoreTo(T root, string filePath) => Store(root, File.CreateText(filePath));
    }
}
