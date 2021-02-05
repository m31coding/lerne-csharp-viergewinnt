using System;
using System.Collections.Generic;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinnt.Spieler.Heuristiken;

namespace VierGewinntTurnier.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SpielerMitName> turnierSpieler = KIs.ErhalteSchnelleKISpieler();
            Turnier turnier = new Turnier(2, turnierSpieler);
            turnier.TrageTurnierAus();
            turnier.GibBerichtAus();
        }
    }
}
