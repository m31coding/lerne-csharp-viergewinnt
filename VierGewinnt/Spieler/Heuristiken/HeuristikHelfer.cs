using System;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler.Heuristiken
{
    public static class HeuristikHelfer
    {
        public static double BewerteAusgangFürRot(Spielstand spielstand)
        {
            if (spielstand == Spielstand.RotIstSieger)
            {
                return double.MaxValue;
            }
            else if (spielstand == Spielstand.GelbIstSieger)
            {
                return -double.MaxValue;
            }
            else if (spielstand == Spielstand.Unentschieden)
            {
                return 0;
            }
            else
            {
                string fehler = $"Unerwarteter Spielstand: {spielstand}.";
                throw new ArgumentException(fehler);
            }
        }
    }
}
