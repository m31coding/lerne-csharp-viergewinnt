using System.Collections.Generic;
using VierGewinnt.Spieler;

namespace VierGewinntTurnier
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SpielerMitName> turnierSpieler = KIs.ErstelleSchnelleKISpieler();
            Turnier turnier = new Turnier(20, turnierSpieler);
            turnier.TrageTurnierAus();
            turnier.GibBerichtAus();
        }
    }
}
