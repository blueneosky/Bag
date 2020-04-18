namespace SatisfactoryModeler.Persistance.Objects
{
    public class Vect2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public static implicit operator Vect2(System.Windows.Point p) => new Vect2 { X = p.X, Y = p.Y };
        public static implicit operator System.Windows.Point(Vect2 v) => new System.Windows.Point(v.X, v.Y);
    }
}