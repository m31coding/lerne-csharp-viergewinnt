using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VierGewinnt.Spiel;

namespace VierGewinntWpfApp
{
    public class ZellenViewModel
    {
        private readonly Rectangle hintergrund;
        private readonly Ellipse innererKreis;
        private readonly Ellipse äußererKreis;

        public ZellenViewModel(Rectangle hintergrund, Ellipse innererKreis, Ellipse outerDisc)
        {
            this.hintergrund = hintergrund;
            this.innererKreis = innererKreis;
            this.äußererKreis = outerDisc;
            Hervorgehoben = false;
            Farbe = Farbe.Keine;
        }

        public bool Hervorgehoben
        {
            set
            {
                hintergrund.Fill = value ? Brushes.Gray : Brushes.LightGray;
            }
        }

        public Farbe Farbe
        {
            set
            {
                switch(value)
                {
                    case Farbe.Keine:
                        innererKreis.Visibility = Visibility.Hidden;
                        äußererKreis.Visibility = Visibility.Hidden;
                        break;

                    case Farbe.Gelb:
                        innererKreis.Fill = Brushes.Yellow;
                        äußererKreis.Fill = Brushes.Orange;
                        innererKreis.Visibility = Visibility.Visible;
                        äußererKreis.Visibility = Visibility.Visible;
                        break;

                    case Farbe.Rot:
                        innererKreis.Fill = Brushes.Red;
                        äußererKreis.Fill = Brushes.DarkRed;
                        innererKreis.Visibility = Visibility.Visible;
                        äußererKreis.Visibility = Visibility.Visible;
                        break;

                    default:
                        throw new ArgumentException($"Unknown color: {value}.");
                }
            }
        }
    }
}
