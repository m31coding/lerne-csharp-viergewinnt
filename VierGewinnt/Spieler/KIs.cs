using System;
using System.Collections.Generic;

namespace VierGewinnt.Spieler
{
    public static class KIs
    {
        public static List<SpielerMitName> ErhalteKISpieler()
        {
            List<SpielerMitName> spieler = new List<SpielerMitName>();

            spieler.Add(new SpielerMitName(new MisterBoring(), "Mister Boring"));
            spieler.Add(new SpielerMitName(new MissRandom(new Random()), "Miss Random"));

            return spieler;
        }
    }
}
