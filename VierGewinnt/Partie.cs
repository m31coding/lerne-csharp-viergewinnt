using System;
using System.Threading.Tasks;
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

            visualisierer.Visualisiere(new Spielstellung(stellung)); 

            while(spielstand == Spielstand.Offen)
            {
                Spielzug nächsterZug = ErhalteNächstenSpielzug(new Spielstellung(stellung));
                stellung.FühreSpielzugAus(nächsterZug);
                spielstand = Stellungsanalyse.ErhalteSpielstand(stellung);
                visualisierer.Visualisiere(new Spielstellung(stellung));
            }

            return spielstand;
        }

        private Spielzug ErhalteNächstenSpielzug(Spielstellung stellung)
        {
            return stellung.SpielerAmZug switch
            {
                Farbe.Rot => spieler1.BerechneNächstenSpielzug(stellung),
                Farbe.Gelb => spieler2.BerechneNächstenSpielzug(stellung),
                _ => throw new ArgumentException("Kein Spieler ist am Zug")
            };
        }
    }
}
