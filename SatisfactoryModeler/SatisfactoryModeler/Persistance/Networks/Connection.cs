using System;

namespace SatisfactoryModeler.Persistance.Networks
{
    public class Connection
    {
        public Guid InputId { get; set; }
        public string InputPortName { get; set; }

        public Guid OutputId { get; set; }
        public string OutputPortName { get; set; }
    }
}