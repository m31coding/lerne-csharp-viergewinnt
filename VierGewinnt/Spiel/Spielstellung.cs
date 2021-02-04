using System;
using System.Collections.Generic;

namespace VierGewinnt.Spiel
{
    public class Spielstellung
    {
        public const int AnzahlZeilen = 6;
        public const int AnzahlSpalten = 7;

        private readonly Farbe[,] spielbrett;

        public Farbe SpielerAmZug
        {
            get
            {
                if (LetzterSpielzug == null) return Farbe.Rot; // rot beginnt

                return LetzterSpielzug.Value.Farbe switch
                {
                    Farbe.Rot => Farbe.Gelb,
                    Farbe.Gelb => Farbe.Rot,
                    Farbe.Keine => Farbe.Keine,
                    _ => throw new ArgumentException($"Unbekannte Farbe: {LetzterSpielzug.Value.Farbe}.")
                };
            }
        }

        public Spielzug? LetzterSpielzug { get; private set; }

        public Spielstellung(Farbe[,] spielbrett, Spielzug? letzerSpielzug)
        {
            if(spielbrett.GetLength(0) != AnzahlZeilen || spielbrett.GetLength(1) != AnzahlSpalten)
            {
                throw new ArgumentException($"Das Spielfeld muss eine Größe von 6 x 7 haben!");
            }

            this.spielbrett = spielbrett;
            LetzterSpielzug = letzerSpielzug;
        }

        public Spielstellung()
        {
            spielbrett = new Farbe[AnzahlZeilen, AnzahlSpalten];
            LetzterSpielzug = null;
        }

        public Spielstellung(Spielstellung spielstellung)
        {
            spielbrett = new Farbe[AnzahlZeilen, AnzahlSpalten];
            Array.Copy(spielstellung.spielbrett, spielbrett, spielbrett.Length);
            LetzterSpielzug = spielstellung.LetzterSpielzug;
        }

        public Farbe SpielsteinFarbe(int zeile, int spalte)
        {
            return spielbrett[zeile, spalte];
        }

        public IEnumerable<Spielzug> MöglicheZüge()
        {
            for (int spalte = 0; spalte < AnzahlSpalten; spalte++)
            {
                Spielzug spielzug = new Spielzug(SpielerAmZug, spalte);
                if (SpielzugIstGültig(spielzug)) yield return spielzug;
            }
        }

        private bool SpielzugIstGültig(Spielzug spielzug)
        {
            return spielzug.Farbe == SpielerAmZug &&
                spielbrett[0, spielzug.Spalte] == Farbe.Keine;
        }

        public void FühreSpielzugAus(Spielzug spielzug)
        {
            if (!SpielzugIstGültig(spielzug))
            {
                throw new ArgumentException($"Der Spielzug {spielzug} ist ungültig!");
            }

            for (int zeile = AnzahlZeilen - 1; zeile >= 0; zeile--)
            {
                if (FeldIstFrei(zeile, spielzug.Spalte))
                {
                    SetzeStein(zeile, spielzug.Spalte, spielzug.Farbe);
                    break;
                }
            }

            LetzterSpielzug = spielzug;
        }

        private bool FeldIstFrei(int zeile, int spalte)
        {
            return spielbrett[zeile, spalte] == Farbe.Keine;
        }

        private void SetzeStein(int zeile, int spalte, Farbe farbe)
        {
            spielbrett[zeile, spalte] = farbe;
        }
    }
}
