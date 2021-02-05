﻿using System;
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

        public MinMaxKI(IHeuristik heuristik, int maximaleTiefe)
        {
            this.heuristik = heuristik;
            this.maximaleTiefe = maximaleTiefe;
            random = new Random();
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            BewerteterSpielzug bewerteterSpielzug = ErhalteDenBestenZug(stellung, 1);
            return bewerteterSpielzug.Zug;
        }

        private BewerteterSpielzug ErhalteDenBestenZug(Spielstellung stellung, int tiefe)
        {
            List<Spielzug> züge = stellung.MöglicheZüge().ToList();
            List<BewerteterSpielzug> bewerteteZüge =
                züge.Select(z => ErhalteBewertetenSpielzug(z, stellung.Kopie(), tiefe)).ToList();

            if (stellung.SpielerAmZug == Farbe.Rot)
            {
                return ErhalteSpielzugMitHöchsterBewertung(bewerteteZüge);
            }
            else // Gelb ist am Zug
            {
                return ErhalteSpielzugMitNiedrigsterBewertung(bewerteteZüge);
            }
        }

        private BewerteterSpielzug ErhalteSpielzugMitHöchsterBewertung(List<BewerteterSpielzug> züge)
        {
            return züge.Aggregate((z1, z2) => ErhalteZug(z1, z2, true));
        }

        private BewerteterSpielzug ErhalteSpielzugMitNiedrigsterBewertung(List<BewerteterSpielzug> züge)
        {
            return züge.Aggregate((z1, z2) => ErhalteZug(z1, z2, false));
        }

        private BewerteterSpielzug ErhalteZug(BewerteterSpielzug zug1, BewerteterSpielzug zug2, bool höchsteBewertung)
        {
            if(zug1.Wertung == zug2.Wertung)
            {
                return random.Next(2) == 0 ? zug1 : zug2;
            }

            if(höchsteBewertung)
            {
                return zug1.Wertung > zug2.Wertung ? zug1 : zug2;
            }
            else
            {
                return zug1.Wertung < zug2.Wertung ? zug1 : zug2;
            }
        }

        private BewerteterSpielzug ErhalteBewertetenSpielzug(Spielzug spielzug, Spielstellung stellung, int tiefe)
        {
            return new BewerteterSpielzug(spielzug, BewerteSpielzugFürRot(spielzug, stellung, tiefe));
        }

        private double BewerteSpielzugFürRot(Spielzug spielzug, Spielstellung stellung, int tiefe)
        {
            stellung.FühreSpielzugAus(spielzug);
            Stellungsanalyse analyse = Stellungsanalyse.ErstelleAnalyse(stellung);
           
            if (analyse.Spielstand == Spielstand.Offen && tiefe < maximaleTiefe)
            {
                return ErhalteDenBestenZug(stellung, tiefe + 1).Wertung;
            }
            else
            {
                return heuristik.BewerteSpielstellungFürRot(stellung, analyse);
            }
        }
    }
}
