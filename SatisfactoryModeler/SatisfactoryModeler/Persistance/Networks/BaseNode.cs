using SatisfactoryModeler.Persistance.Objects;
using System;

namespace SatisfactoryModeler.Persistance.Networks
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; }

        public Vect2 Position { get; set; }
        public bool IsCollapsed { get; set; }

        public InputPort[] Inputs { get; set; }
        public OutputPort[] Outputs { get; set; }
    }
}