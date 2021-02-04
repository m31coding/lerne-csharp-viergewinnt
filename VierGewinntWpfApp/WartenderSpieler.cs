using System.Threading.Tasks;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;

namespace VierGewinntWpfApp
{
    public class WartenderSpieler : ISpieler
    {
        private readonly ISpieler spieler;
        private readonly int millisekunden;

        public WartenderSpieler(ISpieler spieler, int millisekunden)
        {
            this.spieler = spieler;
            this.millisekunden = millisekunden;
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            Task.Delay(millisekunden).Wait();
            return spieler.BerechneNächstenSpielzug(stellung);
        }
    }
}
