using System;
using System.Collections.Generic;
using VierGewinnt.Spieler.Heuristiken;

namespace VierGewinnt.Spieler
{
    public static class KIs
    {
        public static List<SpielerMitName> ErstelleKISpieler()
        {
            List<SpielerMitName> spieler = new List<SpielerMitName>();

            spieler.Add(new SpielerMitName(new MisterBoring(), "Mister Boring"));
            spieler.Add(new SpielerMitName(new MissRandom(new Random()), "Miss Random"));
            spieler.Add(new SpielerMitName(ErstelleEinfacheKI(2), "Einfache KI Tiefe 2"));
            spieler.Add(new SpielerMitName(ErstelleEinfacheKI(4), "Einfache KI Tiefe 4"));
            spieler.Add(new SpielerMitName(ErstelleEinfacheKI(6), "Einfache KI Tiefe 6"));
            spieler.Add(new SpielerMitName(ErstelleEinfacheKI(7), "Einfache KI Tiefe 7"));
            spieler.Add(new SpielerMitName(ErstelleMeineKI(2), "Meine KI Tiefe 2"));
            spieler.Add(new SpielerMitName(ErstelleMeineKI(4), "Meine KI Tiefe 4"));
            spieler.Add(new SpielerMitName(ErstelleMeineKI(6), "Meine KI Tiefe 6"));
            spieler.Add(new SpielerMitName(ErstelleMeineKI(7), "Meine KI Tiefe 7"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new LooserHeuristik(new EinfacheHeuristik()), 2, new Random()), "Looser"));

            return spieler;
        }

        public static List<SpielerMitName> ErstelleSchnelleKISpieler()
        {
            List<SpielerMitName> spieler = new List<SpielerMitName>();

            spieler.Add(new SpielerMitName(new MisterBoring(), "Mister Boring"));
            spieler.Add(new SpielerMitName(new MissRandom(new Random()), "Miss Random"));
            spieler.Add(new SpielerMitName(ErstelleEinfacheKI(2), "Einfache KI Tiefe 2"));
            spieler.Add(new SpielerMitName(ErstelleEinfacheKI(3), "Einfache KI Tiefe 3"));
            spieler.Add(new SpielerMitName(ErstelleMeineKI(2), "Meine KI Tiefe 2"));
            spieler.Add(new SpielerMitName(ErstelleMeineKI(3), "Meine KI Tiefe 3"));
            spieler.Add(new SpielerMitName(new MinMaxKI(new LooserHeuristik(new EinfacheHeuristik()), 2, new Random()), "Looser"));

            return spieler;
        }

        public static ISpieler ErstelleEinfacheKI(int tiefe)
        {
            return ErstelleEinfacheKI(tiefe, new Random());
        }

        public static ISpieler ErstelleEinfacheKI(int tiefe, Random random)
        {
            return new MinMaxKI(new EinfacheHeuristik(), tiefe, random);
        }

        public static ISpieler ErstelleMeineKI(int tiefe)
        {
            return new MinMaxKI(new MeineHeuristik(), tiefe, new Random());
        }

        public static ISpieler ErstelleMeineKI(int tiefe, Random random)
        {
            return new MinMaxKI(new MeineHeuristik(), tiefe, random);
        }
    }
}
