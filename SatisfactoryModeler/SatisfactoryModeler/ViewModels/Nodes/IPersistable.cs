using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryModeler.ViewModels.Nodes
{
    public interface IPersistable
    {
        Guid Id { get; }

        object Persist(object instance);
    }

    public interface IPersistable<TPersisted> : IPersistable
        where TPersisted : class
    {
        TPersisted Persist(TPersisted instance);
    }
}
