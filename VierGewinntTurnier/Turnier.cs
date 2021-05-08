using System;
using System.Collections.Generic;
using System.Linq;
using VierGewinnt;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;
using VierGewinnt.Visualisierer;

namespace VierGewinntTurnier
{
    public class Turnier
    {
        private readonly int anzahlRunden;
        private readonly List<Turnierspieler> turnierspieler;
        private int anzahlPartien = 0;
        private int aktuellePartie = 1;

        public Turnier(int anzahlRunden, List<SpielerMitName> spieler)
        {
            this.anzahlRunden = anzahlRunden;
            this.turnierspieler = spieler.Select(s => new Turnierspieler(s)).ToList();
        }


        public void TrageTurnierAus()
        {
            anzahlPartien = GaußscheSummenformel(turnierspieler.Count-1) * anzahlRunden;

            for (int i = 0; i < anzahlRunden; i++)
            {
                TrageRundeAus(i);
            }

            BerechnePlatzierungen();
        }

        // https://de.wikipedia.org/wiki/Gau%C3%9Fsche_Summenformel
        private static int GaußscheSummenformel(int n)
        {
            return (n * n + n) / 2;
        }

        private void TrageRundeAus(int runde)
        {
            for (int i = 0; i < turnierspieler.Count; i++)
            {
                for (int j = i + 1; j < turnierspieler.Count; j++)
                {
                    if (GeradeRunde(runde))
                    {
                        SpielePartie(turnierspieler[i], turnierspieler[j]);
                    }
                    else
                    {
                        SpielePartie(turnierspieler[j], turnierspieler[i]);
                    }

                    aktuellePartie++;
                }
            }
        }

        private static bool GeradeRunde(int runde)
        {
            return runde % 2 == 0;
        }

        private void SpielePartie(Turnierspieler spielerRot, Turnierspieler spielerGelb)
        {
            Console.Write($"Partie {aktuellePartie}/{anzahlPartien}: {spielerRot.Name} vs {spielerGelb.Name} ... ");
            Partie partie = new Partie(spielerRot.Spieler, spielerGelb.Spieler, new LeererVisualisierer());
            Spielstand ausgang = partie.SpielePartie();

            if (ausgang == Spielstand.RotIstSieger)
            {
                Console.WriteLine($"{spielerRot.Name} gewinnt!");
                spielerRot.VerzeichneSieg();
                spielerGelb.VerzeichenNiederlage();
            }
            else if (ausgang == Spielstand.GelbIstSieger)
            {
                Console.WriteLine($"{spielerGelb.Name} gewinnt!");
                spielerRot.VerzeichenNiederlage();
                spielerGelb.VerzeichneSieg();
            }
            else if (ausgang == Spielstand.Unentschieden)
            {
                Console.WriteLine("Unentschieden!");
                spielerRot.VerzeichneUnentschieden();
                spielerGelb.VerzeichneUnentschieden();
            }
            else
            {
                string fehler = $"Unerwarteter Spielausgange: {ausgang}.";
                throw new ArgumentException(fehler);
            }
        }

        private void BerechnePlatzierungen()
        {
            turnierspieler.Sort((s1, s2) => s2.Punkte.CompareTo(s1.Punkte));

            int platzierung = 1;

            foreach (Turnierspieler spieler in turnierspieler)
            {
                spieler.Platzierung = platzierung;
                platzierung++;
            }
        }

        public void GibBerichtAus()
        {
            Console.WriteLine();
            Console.WriteLine("Platzierungen: ");
            Console.WriteLine();

            foreach(Turnierspieler spieler in turnierspieler)
            {
                GibZeileAus(spieler);
            }
        }

        private void GibZeileAus(Turnierspieler spieler)
        {
            Console.WriteLine($"{spieler.Platzierung}.: {spieler.Name}, {spieler.Punkte} Punkte, " +
                $"{spieler.Siege} Siege, {spieler.Unentschieden} Unentschieden, {spieler.Niederlagen} Niederlagen)");
        }
    }
}
