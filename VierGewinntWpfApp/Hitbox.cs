using System.Windows.Shapes;

namespace VierGewinntWpfApp
{
    public class Hitbox
    {
        public Hitbox(int spalte, Rectangle rechteck)
        {
            Spalte = spalte;
            Rechteck = rechteck;
        }

        public int Spalte { get; }
        public Rectangle Rechteck { get; }
    }
}
