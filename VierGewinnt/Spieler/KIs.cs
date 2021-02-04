using System;
using System.Collections.Generic;

namespace VierGewinnt.Spieler
{
    public static class KIs
    {
        public static IReadOnlyCollection<ISpieler> ErhalteKISpieler()
        {
            List<ISpieler> spieler = new List<ISpieler>
            {
                new MisterBoring(),
                new MissRandom(new Random())
            };
            return spieler;
        }
    }
}
