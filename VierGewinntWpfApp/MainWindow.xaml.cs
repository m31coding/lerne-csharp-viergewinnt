using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VierGewinnt;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinntWpfApp.Spieler;

namespace VierGewinntWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GuiSpieler? aktuellerGuiSpieler;
        private VeränderbarerSpieler kiSpieler;

        public MainWindow()
        {
            InitializeComponent();
            kiSpieler = new VeränderbarerSpieler(null!);
        }

        private void WindowContentRendered(object sender, EventArgs e)
        {
            ErstelleKIs();
        }

        private void ErstelleKIs()
        {
            int zuvorAusgewählt = kiListe.SelectedIndex;
            kiListe.ItemsSource = KIs.ErstelleKISpieler();
            kiListe.SelectedIndex = zuvorAusgewählt == -1 ? 0 : zuvorAusgewählt;
        }

        private void KiAusgewählt(object sender, SelectionChangedEventArgs e)
        {
            SpielerMitName spielerMitName = (SpielerMitName)kiListe.SelectedItem;
            
            if(spielerMitName == null) 
            {
                return;
            }

            kiSpieler.Spieler = spielerMitName.Spieler;
        }

        private async void NeuesSpielButtonClicked(object sender, RoutedEventArgs e)
        {
            if (aktuellerGuiSpieler != null)
            {
                aktuellerGuiSpieler.Abbruch();
            }

            ErstelleKIs();
            Farbe farbeGuiSpieler = StartspielerAbfrage();
            await NeuesSpiel(farbeGuiSpieler);
        }

        private Farbe StartspielerAbfrage()
        {
            string text = "Möchtest du Rot sein? (Rot beginnt)";
            string überschrift = "Startspieler";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult abfrageErgebnis = MessageBox.Show(text, überschrift, button, icon);
            return abfrageErgebnis == MessageBoxResult.Yes ? Farbe.Rot : Farbe.Gelb;
        }

        private async Task NeuesSpiel(Farbe farbeGuiSpieler)
        {
            Canvas canvas = new Canvas()
            {
                Height = Spielstellung.AnzahlZeilen,
                Width = Spielstellung.AnzahlSpalten
            };

            Viewbox.Child = canvas;

            GuiVisualisierer visualisierer = GuiVisualisierer.ErstelleVisualisierer(canvas, Dispatcher, true);
            aktuellerGuiSpieler = new GuiSpieler();
            visualisierer.AufSpalteGeklickt += spalte => aktuellerGuiSpieler.WähleSpalte(spalte);

            ISpieler spieler1 = aktuellerGuiSpieler;
            ISpieler spieler2 = kiSpieler;

            if (farbeGuiSpieler == Farbe.Gelb)
            {
                spieler1 = spieler2;
                spieler2 = aktuellerGuiSpieler;
            }

            Partie partie = new Partie(spieler1, spieler2, visualisierer);
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
            catch (OperationCanceledException)
            {
                return Spielstand.Spielabbruch;
            }
        }

        private string Ausgabe(Spielstand spielausgang)
        {
            if (spielausgang == Spielstand.RotIstSieger)
            {
                return "Rot hat gewonnen!";
            }
            else if (spielausgang == Spielstand.GelbIstSieger)
            {
                return "Gelb hat gewonnen!";
            }
            else if (spielausgang == Spielstand.Unentschieden)
            {
                return "Unentschieden!";
            }
            else
            {
                throw new ArgumentException($"Unerwarteter Spielausgang: {spielausgang}.");
            }
        }
    }
}
