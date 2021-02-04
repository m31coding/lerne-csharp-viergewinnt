using System;
using System.Collections.Generic;
using System.Linq;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    public class KonsolenspielerV2 : ISpieler
    {
        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            List<Spielzug> möglicheZüge = stellung.MöglicheZüge().ToList();
            return LeseNächstenSpielzugAusKonsoleneingabe(möglicheZüge);
        }

        private Spielzug LeseNächstenSpielzugAusKonsoleneingabe(List<Spielzug> möglicheZüge)
        {
            string eingabe = FrageEingabeAb(möglicheZüge);

            if (!int.TryParse(eingabe, out int gewählteSpalte))
            {
                Console.WriteLine($"Ungültige Eingabe: {eingabe}. Gib eine Spalte ein.");
                return LeseNächstenSpielzugAusKonsoleneingabe(möglicheZüge);
            }

            Spielzug? gewählterSpielzug = möglicheZüge.FirstOrDefault(z => z.Spalte == gewählteSpalte);

            if (gewählterSpielzug == null)
            {
                Console.WriteLine($"Spalte {eingabe} ist nicht möglich.");
                return LeseNächstenSpielzugAusKonsoleneingabe(möglicheZüge);
            }

            return gewählterSpielzug.Value;
        }

        private string FrageEingabeAb(List<Spielzug> möglicheZüge)
        {
            IEnumerable<int> möglicheSpalten = möglicheZüge.Select(z => z.Spalte);
            Console.WriteLine($"Mögliche spalten: {string.Join(", ", möglicheSpalten)}");
            Console.Write("Eingabe: ");
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
