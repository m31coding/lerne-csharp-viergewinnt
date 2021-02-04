using System;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    public class Konsolenspieler : ISpieler
    {
        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            Console.WriteLine("Wähle eine Zahl zwischen eins und sechs.");
            int eingabe = int.Parse(Console.ReadKey().KeyChar.ToString());
            return new Spielzug(stellung.SpielerAmZug, eingabe);
        }
    }
}
