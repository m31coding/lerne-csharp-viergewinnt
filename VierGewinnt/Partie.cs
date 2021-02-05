using System;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinnt.Visualisierer;

namespace VierGewinnt
{
    public class Partie
    {
        private readonly ISpieler spielerRot;
        private readonly ISpieler spielerGelb;
        private readonly IVisualisierer visualisierer;

        public Partie(ISpieler spielerRot, ISpieler spielerGelb, IVisualisierer visualisierer)
        {
            this.spielerRot = spielerRot;
            this.spielerGelb = spielerGelb;
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
                return spielerRot.BerechneNächstenSpielzug(stellung);
            }
            else if (stellung.SpielerAmZug == Farbe.Gelb)
            {
                return spielerGelb.BerechneNächstenSpielzug(stellung);
            }
            else
            {
                throw new ArgumentException("Kein Spieler ist am Zug");
            }
        }
    }
}
