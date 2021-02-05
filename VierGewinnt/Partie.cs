using System;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinnt.Visualisierer;

namespace VierGewinnt
{
    public class Partie
    {
        private readonly ISpieler spieler1;
        private readonly ISpieler spieler2;
        private readonly IVisualisierer visualisierer;

        public Partie(ISpieler spieler1, ISpieler spieler2, IVisualisierer visualisierer)
        {
            this.spieler1 = spieler1;
            this.spieler2 = spieler2;
            this.visualisierer = visualisierer;
        }

        public Spielstand SpielePartie()
        {
            Spielstellung stellung = new Spielstellung();
            Spielstand spielstand = Spielstand.Offen;

            visualisierer.Visualisiere(stellung.Kopie());

            while (spielstand == Spielstand.Offen)
            {
                Spielzug nächsterZug = ErhalteNächstenSpielzug(stellung.Kopie());
                stellung.FühreSpielzugAus(nächsterZug);
                spielstand = Stellungsanalyse.ErstelleAnalyse(stellung).Spielstand;
                visualisierer.Visualisiere(stellung.Kopie());
            }

            return spielstand;
        }

        private Spielzug ErhalteNächstenSpielzug(Spielstellung stellung)
        {
            if (stellung.SpielerAmZug == Farbe.Rot)
            {
                return spieler1.BerechneNächstenSpielzug(stellung);
            }
            else if (stellung.SpielerAmZug == Farbe.Gelb)
            {
                return spieler2.BerechneNächstenSpielzug(stellung);
            }
            else
            {
                throw new ArgumentException("Kein Spieler ist am Zug");
            }
        }
    }
}
