using System.IO;

namespace SatisfactoryModeler.Persistance
{
    public interface IPersistanceEngine<T>
    {
        void Store(Stream stream, T o);
        T Restore(Stream src);
    }
}
