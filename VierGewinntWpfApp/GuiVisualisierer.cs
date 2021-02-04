using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using VierGewinnt.Spiel;
using VierGewinnt.Visualisierer;

namespace VierGewinntWpfApp
{
    public class GuiVisualisierer : IVisualisierer
    {
        private readonly ZellenViewModel[,] zellen;
        private readonly Dispatcher dispatcher;
        public event Action<int>? AufSpalteGeklickt;

        private GuiVisualisierer(
            ZellenViewModel[,] zellen,
            Dispatcher dispatcher,
            List<Hitbox> hitboxen,
            bool mitInteraktion)
        {
            this.zellen = zellen;
            this.dispatcher = dispatcher;

            if(mitInteraktion)
            {
                foreach (Hitbox hitbox in hitboxen)
                {
                    hitbox.Rechteck.MouseEnter += (s, e) => HebeSpalteHervor(hitbox.Spalte);
                    hitbox.Rechteck.MouseLeave += (s, e) => HebeHervorhebungDerSpalteAuf(hitbox.Spalte);
                    hitbox.Rechteck.MouseLeftButtonDown += (s, e) => AufSpalteGeklickt?.Invoke(hitbox.Spalte);
                }
            }
        }

        public static GuiVisualisierer ErstelleVisualisierer(Canvas canvas, Dispatcher dispatcher, bool mitInteraktion)
        {
            ViewModelFactory factory = new ViewModelFactory(canvas);
            ZellenViewModel[,] zellen = new ZellenViewModel[Spielstellung.AnzahlZeilen, Spielstellung.AnzahlSpalten];

            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                for (int j = 0; j < Spielstellung.AnzahlSpalten; j++)
                {
                    ZellenViewModel model = factory.ErstelleZellenModell(i, j);
                    zellen[i, j] = model;
                }
            }

            List<Hitbox> hitboxen = Enumerable.Range(0, Spielstellung.AnzahlSpalten)
                .Select(j => new Hitbox(j, factory.ErhalteHitbox(j))).ToList();

            return new GuiVisualisierer(zellen, dispatcher, hitboxen, mitInteraktion);
        }

        private void HebeSpalteHervor(int j)
        {
            for(int i = 0; i< Spielstellung.AnzahlZeilen; i++)
            {
                zellen[i, j].Hervorgehoben = true;
            }
        }

        private void HebeHervorhebungDerSpalteAuf(int j)
        {
            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                zellen[i, j].Hervorgehoben = false;
            }
        }

        public void Visualisiere(Spielstellung stellung)
        {
            dispatcher.Invoke(() => VisualisiereIntern(stellung));
        }

        private void VisualisiereIntern(Spielstellung stellung)
        {
            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                for (int j = 0; j < Spielstellung.AnzahlSpalten; j++)
                {
                    zellen[i, j].Farbe = stellung.SpielsteinFarbe(i, j);
                }
            }
        }
    }
}
