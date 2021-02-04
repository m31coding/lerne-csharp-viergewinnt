using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VierGewinnt;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinnt.Visualisierer;

namespace VierGewinntWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GuiSpieler? aktuellerGuiSpieler;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void WindowContentRendered(object sender, EventArgs e)
        {
            await NeuesSpiel();
        }

        private async void NeuesSpielButtonClicked(object sender, RoutedEventArgs e)
        {
            if(aktuellerGuiSpieler != null)
            {
                aktuellerGuiSpieler.Abbruch();
            }

            await NeuesSpiel();
        }

        private async Task NeuesSpiel()
        {
            Canvas canvas = new Canvas()
            {
                Height=Spielstellung.AnzahlZeilen,
                Width=Spielstellung.AnzahlSpalten
            };

            Viewbox.Child = canvas;

            GuiVisualisierer visualisierer = GuiVisualisierer.ErstelleVisualisierer(canvas, Dispatcher, true);
            aktuellerGuiSpieler = new GuiSpieler();
            visualisierer.AufSpalteGeklickt += spalte => aktuellerGuiSpieler.WähleSpalte(spalte);

            Partie partie = new Partie(aktuellerGuiSpieler, ErhalteKI(), visualisierer);
            Spielstand spielausgang = await SpielePartie(partie);
            
            if (spielausgang != Spielstand.Spielabbruch)
            {
                MessageBox.Show(Ausgabe(spielausgang));
            }
        }

        private async Task<Spielstand> SpielePartie(Partie partie)
        {
            try
            {
                return await Task.Run(() => partie.SpielePartie());
            }
            catch(OperationCanceledException)
            {
                return Spielstand.Spielabbruch;
            }
        }

        private string Ausgabe(Spielstand spielausgang)
        {
            return spielausgang switch
            {
                Spielstand.RotIstSieger => "Rot hat gewonnen!",
                Spielstand.GelbIstSieger => "Gelb hat gewonnen!",
                Spielstand.Unentschieden => "Unentschieden!",
                _ => throw new ArgumentException($"Unerwarteter Spielausgang: {spielausgang}.")
            };
        }

        private ISpieler ErhalteKI()
        {
            Random zahlengenerator = new Random();
            MissRandom missRandom = new MissRandom(zahlengenerator);
            return new WartenderSpieler(missRandom, 1000);
        }
    }
}
