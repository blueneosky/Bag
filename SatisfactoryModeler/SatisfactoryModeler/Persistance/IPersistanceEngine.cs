using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.Persistance
{
    public interface IPersistanceEngine<T>
    {
        Stream Store(T root);
        T Restore(Stream src);
    }
}
