using System;
using System.Collections.Generic;
using System.Linq;

namespace VierGewinnt.Spiel
{
    public class Stellungsanalyse
    {
        public static Spielstand ErhalteSpielstand(Spielstellung stellung)
        {
            return new Stellungsanalyse(stellung).ErhalteSpielstand();
        }

        private readonly Spielstellung stellung;

        private Stellungsanalyse(Spielstellung stellung)
        {
            this.stellung = stellung;
        }

        private Spielstand ErhalteSpielstand()
        {
            if (stellung.LetzterSpielzug == null) return Spielstand.Offen;
            Spielzug letzterSpielzug = stellung.LetzterSpielzug.Value;

            int letzteSpalte = letzterSpielzug.Spalte;
            int letzteZeile = LetzteZeile(letzterSpielzug);

            if (VierHintereinander(SteineInZeile(letzteZeile), letzterSpielzug.Farbe))
            {
                return SiegerfarbeZuSpielstand(letzterSpielzug.Farbe);
            }

            if (VierHintereinander(SteineInSpalte(letzteSpalte), letzterSpielzug.Farbe))
            {
                return SiegerfarbeZuSpielstand(letzterSpielzug.Farbe);
            }

            if (VierHintereinander(SteineInDiagonaleNachRechtsUnten(letzteZeile, letzteSpalte), letzterSpielzug.Farbe))
            {
                return SiegerfarbeZuSpielstand(letzterSpielzug.Farbe);
            }

            if (VierHintereinander(SteineInDiagonaleNachRechtsOben(letzteZeile, letzteSpalte), letzterSpielzug.Farbe))
            {
                return SiegerfarbeZuSpielstand(letzterSpielzug.Farbe);
            }

            if (ObersteZeileIstVoll()) return Spielstand.Unentschieden;
            else return Spielstand.Offen;

            Spielstand SiegerfarbeZuSpielstand(Farbe farbe)
            {
                return farbe switch
                {
                    Farbe.Rot => Spielstand.RotIstSieger,
                    Farbe.Gelb => Spielstand.GelbIstSieger,
                    _ => throw new ArgumentException($"Siegerfarbe {farbe} kann nicht zu einem Spielstand konvertiert werden.")
                };
            }
        }

        private bool ObersteZeileIstVoll()
        {
            return SteineInZeile(0).All(s => s == Farbe.Rot || s == Farbe.Gelb);
            // return !SteineInZeile(0).Any(s => s == Farbe.Keine);
        }

        private int LetzteZeile(Spielzug letzterSpielzug)
        {
            for (int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                if (stellung.SpielsteinFarbe(i, letzterSpielzug.Spalte) != Farbe.Keine)
                {
                    return i;
                }
            }

            throw new Exception("Kann die Zeile des letzten Spielzugs nicht berechnen!");
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

                if(j == Spielstellung.AnzahlSpalten)
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

        private bool VierHintereinander(IEnumerable<Farbe> spielsteine, Farbe farbe)
        {
            int anzahlGleicherSteineHintereinander = 0;

            foreach (Farbe spielstein in spielsteine)
            {
                if (spielstein == farbe)
                {
                    anzahlGleicherSteineHintereinander++;
                    if (anzahlGleicherSteineHintereinander == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    anzahlGleicherSteineHintereinander = 0;
                }
            }

            return false;
        }
    }
}
