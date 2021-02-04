using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    // Wählt immer einen zufälligen Zug aus
    public class MissRandom : ISpieler
    {
        private readonly Random zahlengenerator;

        public MissRandom(Random zahlengenerator)
        {
            this.zahlengenerator = zahlengenerator;
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            List<Spielzug> möglicheZüge = stellung.MöglicheZüge().ToList();
            int zufälligeZahl = zahlengenerator.Next(0, möglicheZüge.Count);
            return möglicheZüge[zufälligeZahl];
        }
    }
}
