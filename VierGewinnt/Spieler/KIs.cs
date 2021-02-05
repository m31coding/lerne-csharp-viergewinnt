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
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 2), "Einfache KI Tiefe 2"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 4), "Einfache KI Tiefe 4"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 6), "Einfache KI Tiefe 6"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new MeineHeuristik(), 2), "Meine KI Tiefe 2"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new MeineHeuristik(), 4), "Meine KI Tiefe 4"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new MeineHeuristik(), 6), "Meine KI Tiefe 6"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new LooserHeuristik(new EinfacheHeuristik()), 2), "Looser"));

            return spieler;
        }

        public static List<SpielerMitName> ErhalteSchnelleKISpieler()
        {
            List<SpielerMitName> spieler = new List<SpielerMitName>();

            spieler.Add(new SpielerMitName(new MisterBoring(), "Mister Boring"));
            spieler.Add(new SpielerMitName(new MissRandom(new Random()), "Miss Random"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 2), "Einfache KI Tiefe 2"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new EinfacheHeuristik(), 3), "Einfache KI Tiefe 3"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new MeineHeuristik(), 2), "Meine KI Tiefe 2"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new MeineHeuristik(), 3), "Meine KI Tiefe 3"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new LooserHeuristik(new EinfacheHeuristik()), 2), "Looser"));

            return spieler;
        }
    }
}
