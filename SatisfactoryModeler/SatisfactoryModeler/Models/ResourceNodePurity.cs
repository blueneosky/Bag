using System;

namespace SatisfactoryModeler.Models
{
    public enum ResourceNodePurity
    {
        Impure = 1,
        Normal = 2,
        Pure = 3,
    }

    public static class NodePurityExtensions
    {
        public static double ToFactor(this ResourceNodePurity value)
                => Math.Pow(2, (int)value - 1) / 2.0;
    }
}
