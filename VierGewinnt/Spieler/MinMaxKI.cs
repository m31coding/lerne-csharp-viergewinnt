using System;
using System.Collections.Generic;
using System.Linq;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler.Heuristiken;

namespace VierGewinnt.Spieler
{
    public class MinMaxKI : ISpieler
    {
        private readonly IHeuristik heuristik;
        private readonly int maximaleTiefe;
        private readonly Random random;

        public MinMaxKI(IHeuristik heuristik, int maximaleTiefe, Random random)
        {
            this.heuristik = heuristik;
            this.maximaleTiefe = maximaleTiefe;
            this.random = random;
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            BewerteterSpielzug bewerteterSpielzug = ErhalteDenBestenZug(stellung, 1);
            return bewerteterSpielzug.Zug;
        }

        private BewerteterSpielzug ErhalteDenBestenZug(Spielstellung stellung, int aktuelleTiefe)
        {
            List<BewerteterSpielzug> bewerteteZüge = ErhalteBewerteteSpielzüge(stellung, aktuelleTiefe);

            if (stellung.SpielerAmZug == Farbe.Rot)
            {
                return ErhalteSpielzugMitHöchsterBewertung(bewerteteZüge);
            }
            else // Gelb ist am Zug
            {
                return ErhalteSpielzugMitNiedrigsterBewertung(bewerteteZüge);
            }
        }

        private List<BewerteterSpielzug> ErhalteBewerteteSpielzüge(Spielstellung stellung, int aktuelleTiefe)
        {
            List<Spielzug> züge = stellung.MöglicheZüge().ToList();
            List<BewerteterSpielzug> bewerteteZüge =
                züge.Select(z => ErhalteBewertetenSpielzug(z, stellung.Kopie(), aktuelleTiefe)).ToList();
            return bewerteteZüge;
        }

        private BewerteterSpielzug ErhalteSpielzugMitHöchsterBewertung(List<BewerteterSpielzug> züge)
        {
            return züge.Aggregate((z1, z2) => ErhalteSpielzugMitHöchsterBewertung(z1, z2));
        }

        private BewerteterSpielzug ErhalteSpielzugMitNiedrigsterBewertung(List<BewerteterSpielzug> züge)
        {
            return züge.Aggregate((z1, z2) => ErhalteSpielzugMitNiedrigsterBewertung(z1, z2));
        }

        private BewerteterSpielzug ErhalteSpielzugMitHöchsterBewertung(BewerteterSpielzug zug1, BewerteterSpielzug zug2)
        {
            if (zug1.Wertung == zug2.Wertung)
            {
                return ErhalteZufälligenSpielzug(zug1, zug2);
            }

            return zug1.Wertung > zug2.Wertung ? zug1 : zug2;
        }

        private BewerteterSpielzug ErhalteSpielzugMitNiedrigsterBewertung(BewerteterSpielzug zug1, BewerteterSpielzug zug2)
        {
            if (zug1.Wertung == zug2.Wertung)
            {
                return ErhalteZufälligenSpielzug(zug1, zug2);
            }

            return zug1.Wertung < zug2.Wertung ? zug1 : zug2;
        }

        private BewerteterSpielzug ErhalteZufälligenSpielzug(BewerteterSpielzug zug1, BewerteterSpielzug zug2)
        {
            return random.Next(2) == 0 ? zug1 : zug2;
        }

        private BewerteterSpielzug ErhalteBewertetenSpielzug(Spielzug spielzug, Spielstellung stellung, int aktuelleTiefe)
        {
            return new BewerteterSpielzug(spielzug, BewerteSpielzugFürRot(spielzug, stellung, aktuelleTiefe));
        }

        private double BewerteSpielzugFürRot(Spielzug spielzug, Spielstellung stellung, int aktuelleTiefe)
        {
            stellung.FühreSpielzugAus(spielzug);
            Stellungsanalyse analyse = Stellungsanalyse.ErstelleAnalyse(stellung);

            if (analyse.Spielstand == Spielstand.Offen && aktuelleTiefe < maximaleTiefe)
            {
                return ErhalteDenBestenZug(stellung, aktuelleTiefe + 1).Wertung;
            }
            else
            {
                return heuristik.BewerteSpielstellungFürRot(stellung, analyse);
            }
        }
    }
}
