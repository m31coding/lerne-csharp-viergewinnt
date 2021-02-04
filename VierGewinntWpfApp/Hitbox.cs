using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace VierGewinntWpfApp
{
    public class Hitbox
    {
        public readonly int Spalte;
        public readonly Rectangle Rechteck;

        public Hitbox(int spalte, Rectangle rechteck)
        {
            Spalte = spalte;
            Rechteck = rechteck;
        }
    }
}
