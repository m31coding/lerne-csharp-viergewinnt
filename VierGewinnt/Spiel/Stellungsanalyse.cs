using System;
using System.Collections.Generic;
using System.Linq;

namespace VierGewinnt.Spiel
{
    public class Stellungsanalyse
    {
        public static Stellungsanalyse ErstelleAnalyse(Spielstellung stellung)
        {
            Stellungsanalyse analyse = new Stellungsanalyse(stellung);
            analyse.ErstelleAnalyse();
            return analyse;
        }

        private readonly Spielstellung stellung;

        private Stellungsanalyse(Spielstellung stellung)
        {
            this.stellung = stellung;
            VerbindungenRot = new Verbindungen();
            VerbindungenGelb = new Verbindungen();
            Spielstand = Spielstand.Offen;
        }

        public Verbindungen VerbindungenRot { get; }
        public Verbindungen VerbindungenGelb { get; }
        public Spielstand Spielstand { get; private set; }

        private void ErstelleAnalyse()
        {
            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                ZähleVerbindungen(SteineInZeile(i));
            }

            for (int j = 0; j < Spielstellung.AnzahlSpalten; j++)
            {
                ZähleVerbindungen(SteineInSpalte(j));
            }

            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                ZähleVerbindungen(SteineInDiagonaleNachRechtsUnten(i, 0));
            }

            for (int j = 1; j < Spielstellung.AnzahlSpalten; j++)
            {
                ZähleVerbindungen(SteineInDiagonaleNachRechtsUnten(0, j));
            }

            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                ZähleVerbindungen(SteineInDiagonaleNachRechtsOben(i, 0));
            }

            for (int j = 1; j < Spielstellung.AnzahlSpalten; j++)
            {
                ZähleVerbindungen(SteineInDiagonaleNachRechtsOben(Spielstellung.AnzahlZeilen-1, j));
            }

            Spielstand = ErhalteSpielstand();
        }

        private Spielstand ErhalteSpielstand()
        {
            if (VerbindungenRot.AnzahlViererverbindungen >= 1)
            {
                return Spielstand.RotIstSieger;
            }
            else if (VerbindungenGelb.AnzahlViererverbindungen >= 1)
            {
                return Spielstand.GelbIstSieger;
            }
            else if(ObersteZeileIstVoll())
            {
                return Spielstand.Unentschieden;
            }
            else
            {
                return Spielstand.Offen;
            }
        }

        private bool ObersteZeileIstVoll()
        {
            return SteineInZeile(0).All(s => s == Farbe.Rot || s == Farbe.Gelb);
        }

        private IEnumerable<Farbe> SteineInZeile(int zeile)
        {
            for (int j = 0; j < Spielstellung.AnzahlSpalten; j++)
            {
                yield return stellung.SpielsteinFarbe(zeile, j);
            }
        }

        private IEnumerable<Farbe> SteineInSpalte(int spalte)
        {
            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                yield return stellung.SpielsteinFarbe(i, spalte);
            }
        }

        private IEnumerable<Farbe> SteineInDiagonaleNachRechtsUnten(int zeile, int spalte)
        {
            int differenz = zeile - spalte;
            int startZeile = Math.Max(differenz, 0);
            int startSpalte = Math.Max(-differenz, 0);
            int j = startSpalte;

            for (int i = startZeile; i < Spielstellung.AnzahlZeilen; i++)
            {
                yield return stellung.SpielsteinFarbe(i, j);
                j++;

                if (j == Spielstellung.AnzahlSpalten)
                {
                    break;
                }
            }
        }

        private IEnumerable<Farbe> SteineInDiagonaleNachRechtsOben(int zeile, int spalte)
        {
            int summe = spalte + zeile;
            int startZeile = Math.Min(summe, Spielstellung.AnzahlZeilen - 1);
            int startSpalte = Math.Max(0, summe - (Spielstellung.AnzahlZeilen - 1));
            int j = startSpalte;

            for (int i = startZeile; i >= 0; i--)
            {
                yield return stellung.SpielsteinFarbe(i, j);
                j++;

                if (j == Spielstellung.AnzahlSpalten)
                {
                    break;
                }
            }
        }

        private void ZähleVerbindungen(IEnumerable<Farbe> steine)
        {
            int anzahlGleicheSteine = 0;
            Farbe letzterStein = Farbe.Keine;

            foreach(Farbe stein in steine)
            {
                if(stein == letzterStein)
                {
                    anzahlGleicheSteine++;
                }
                else
                {
                    WerteAus(anzahlGleicheSteine, letzterStein);
                    anzahlGleicheSteine = 1;
                    letzterStein = stein;
                }
            }

            WerteAus(anzahlGleicheSteine, letzterStein);
        }

        private void WerteAus(int anzahlGleicheSteine, Farbe farbe)
        {
            if(anzahlGleicheSteine <= 1 || farbe == Farbe.Keine)
            {
                return;
            }

            Verbindungen verbindungen = 
                farbe == Farbe.Rot ? VerbindungenRot : VerbindungenGelb;

            if (anzahlGleicheSteine == 2)
            {
                verbindungen.AnzahlZweierverbindungen++;
            }
            else if (anzahlGleicheSteine == 3)
            {
                verbindungen.AnzahlDreierverbindungen++;
            }
            else if (anzahlGleicheSteine == 4)
            {
                verbindungen.AnzahlViererverbindungen++;
            }
        }
    }
}
