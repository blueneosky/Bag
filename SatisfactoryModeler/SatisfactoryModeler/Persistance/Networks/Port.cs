using System;

namespace SatisfactoryModeler.Persistance.Networks
{
    public abstract class Port
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public bool WithValue { get; set; }
        public object Value { get; set; }
    }
}