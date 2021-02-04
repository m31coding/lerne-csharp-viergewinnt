using System.Collections.Generic;
using VierGewinnt.Spiel;

namespace VierGewinntTests
{
    public static class SpielstellungsErweiterungen
    {
        public static void FühreSpielzügeAus(this Spielstellung stellung, List<Spielzug> spielzüge)
        {
            foreach(Spielzug spielzug in spielzüge)
            {
                stellung.FühreSpielzugAus(spielzug);
            }
        }
    }
}
