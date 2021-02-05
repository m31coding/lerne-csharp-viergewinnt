using System;
using System.Collections.Generic;
using VierGewinnt.Spieler.Heuristiken;

namespace VierGewinnt.Spieler
{
    public static class KIs
    {
        public static List<SpielerMitName> ErhalteKISpieler()
        {
            List<SpielerMitName> spieler = new List<SpielerMitName>();

            spieler.Add(new SpielerMitName(new MisterBoring(), "Mister Boring"));
            spieler.Add(new SpielerMitName(new MissRandom(new Random()), "Miss Random"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 4), "MinMax Tiefe 4"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 4), "MinMax Tiefe 8"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new MeineHeuristik(), 8), "Meine KI Tiefe 8"));
            return spieler;
        }
    }
}
