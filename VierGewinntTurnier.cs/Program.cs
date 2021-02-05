using System;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinnt.Spieler.Heuristiken;

namespace VierGewinntTurnier.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpieler ki = new MinMaxKI(new EinfacheHeuristik(), 2);

            Spielstellung stellung = new Spielstellung();
            stellung.FühreSpielzugAus(new Spielzug(Farbe.Rot, 2));
            stellung.FühreSpielzugAus(new Spielzug(Farbe.Gelb, 0));
            stellung.FühreSpielzugAus(new Spielzug(Farbe.Rot, 2));
            stellung.FühreSpielzugAus(new Spielzug(Farbe.Gelb, 0));
            stellung.FühreSpielzugAus(new Spielzug(Farbe.Rot, 2));
            //stellung.FühreSpielzugAus(new Spielzug(Farbe.Gelb, 0));

            Spielzug spielzug = ki.BerechneNächstenSpielzug(stellung);
        }
    }
}
