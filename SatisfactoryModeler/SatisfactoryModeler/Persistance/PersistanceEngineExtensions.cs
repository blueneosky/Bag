using System.IO;

namespace SatisfactoryModeler.Persistance
{
    public static class PersistanceEngineExtensions
    {
        public static T RestoreFrom<T>(this IPersistanceEngine<T> engine, string filePath)
            => engine.Restore(File.OpenRead(filePath));

        public static void StoreTo<T>(this IPersistanceEngine<T> engine, string filePath, T root)
            => engine.Store(File.Create(filePath), root);

    }
}
