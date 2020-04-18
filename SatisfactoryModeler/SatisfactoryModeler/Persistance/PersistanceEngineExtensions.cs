using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Persistance
{
    public static class PersistanceEngineExtensions
    {
        public static T RestoreFrom<T>(this IPersistanceEngine<T> engine, string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            {
                return engine.Restore(fileStream);
            }
        }

        public static void StoreTo<T>(this IPersistanceEngine<T> engine, T root, string filePath)
        {
            using (var fileStream = File.Create(filePath))
            using(var stream = engine.Store(root))                
            {
                stream.CopyTo(fileStream);
            }
        }
            
    }
}
