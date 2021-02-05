using System.Collections.Generic;
using VierGewinnt.Spieler;

namespace VierGewinntTurnier.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SpielerMitName> turnierSpieler = KIs.ErhalteSchnelleKISpieler();
            Turnier turnier = new Turnier(10, turnierSpieler);
            turnier.TrageTurnierAus();
            turnier.GibBerichtAus();
        }
    }
}
