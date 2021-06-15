using System;
using System.Collections.Generic;

namespace VierGewinnt.Spiel
{
    public class Spielstellung
    {
        private readonly Farbe[,] spielbrett;

        public Spielstellung(Farbe[,] spielbrett, Farbe spielerAmZug)
        {
            if(spielbrett.GetLength(0) != AnzahlZeilen || spielbrett.GetLength(1) != AnzahlSpalten)
            {
                string fehler = $"Das Spielfeld muss eine Größe von {AnzahlZeilen} x {AnzahlSpalten} haben!";
                throw new ArgumentException(fehler);
            }

            this.spielbrett = spielbrett;
            SpielerAmZug = spielerAmZug;
        }

        public Spielstellung()
        {
            spielbrett = new Farbe[AnzahlZeilen, AnzahlSpalten];
            SpielerAmZug = Farbe.Rot;
        }

        private Spielstellung(Spielstellung spielstellung)
        {
            spielbrett = new Farbe[AnzahlZeilen, AnzahlSpalten];
            Array.Copy(spielstellung.spielbrett, spielbrett, spielbrett.Length);
            SpielerAmZug = spielstellung.SpielerAmZug;
        }

        public static int AnzahlZeilen => 6;
        public static int AnzahlSpalten => 7;
        public Farbe SpielerAmZug { get; private set; }

        public Spielstellung Kopie()
        {
            return new Spielstellung(this);
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
                if (SpielzugIstGültig(spielzug))
                {
                    yield return spielzug;
                }
            }
        }

        private bool SpielzugIstGültig(Spielzug spielzug)
        {
            return spielzug.Farbe == SpielerAmZug && FeldIstFrei(0, spielzug.Spalte);
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

            WechsleSpielerAmZug();
        }

        private bool FeldIstFrei(int zeile, int spalte)
        {
            return spielbrett[zeile, spalte] == Farbe.Keine;
        }

        private void SetzeStein(int zeile, int spalte, Farbe farbe)
        {
            spielbrett[zeile, spalte] = farbe;
        }

        private void WechsleSpielerAmZug()
        {
            SpielerAmZug = SpielerAmZug == Farbe.Rot ? Farbe.Gelb : Farbe.Rot;
        }
    }
}
