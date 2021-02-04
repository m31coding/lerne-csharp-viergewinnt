using System.Threading.Tasks;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
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
