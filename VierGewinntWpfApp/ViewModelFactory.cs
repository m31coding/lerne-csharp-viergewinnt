using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VierGewinnt.Spiel;

namespace VierGewinntWpfApp
{
    public class ViewModelFactory
    {
        private readonly double zellHöhe;
        private readonly double zellBreite;
        private readonly Canvas canvas;

        public ViewModelFactory(Canvas canvas)
        {
            zellHöhe = canvas.Height / Spielstellung.AnzahlZeilen;
            zellBreite = canvas.Width / Spielstellung.AnzahlSpalten;
            this.canvas = canvas;
        }

        public ZellenViewModel ErstelleZellenModell(int zeile, int spalte)
        {
            Rectangle hintergrund = ErhalteHintergrund(zeile, spalte);
            Ellipse äußererKreis = ErhalteKreis(zeile, spalte, 0.8);
            Ellipse innererKreis = ErhalteKreis(zeile, spalte, 0.7);

            // die Reihenfolge ist entscheidend (z-value)
            canvas.Children.Add(hintergrund);
            canvas.Children.Add(äußererKreis);
            canvas.Children.Add(innererKreis);

            return new ZellenViewModel(hintergrund, innererKreis, äußererKreis);
        }

        public Rectangle ErhalteHitbox(int spalte)
        {
            Rectangle hitbox = new Rectangle()
            {
                Height = zellHöhe * Spielstellung.AnzahlSpalten,
                Width = zellBreite,
                // Fill = new SolidColorBrush(Colors.Blue) { Opacity = 0.5 }
                Fill = Brushes.Transparent
            };

            Canvas.SetTop(hitbox, 0);
            Canvas.SetLeft(hitbox, spalte * zellBreite);

            canvas.Children.Add(hitbox);

            return hitbox;
        }

        private Rectangle ErhalteHintergrund(int zeile, int spalte)
        {
            double factor = 0.9;

            Rectangle hintergrund = new Rectangle()
            {
                Height = zellHöhe * factor,
                Width = zellBreite * factor
            };

            Canvas.SetTop(hintergrund, zeile * zellHöhe + 0.5 * (1 - zellHöhe * factor));
            Canvas.SetLeft(hintergrund, spalte * zellBreite + 0.5 * (1 - zellBreite * factor));

            return hintergrund;
        }

        private Ellipse ErhalteKreis(int zeile, int spalte, double faktor)
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = zellHöhe * faktor,
                Width = zellBreite * faktor
            };

            Canvas.SetTop(ellipse, zeile * zellHöhe + 0.5 * (1- zellHöhe * faktor));
            Canvas.SetLeft(ellipse, spalte * zellBreite + 0.5 * (1 - zellBreite * faktor));

            return ellipse;
        }
    }
}
