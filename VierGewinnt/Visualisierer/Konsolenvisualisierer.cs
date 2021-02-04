using System;
using System.Linq;
using System.Text;
using VierGewinnt.Spiel;

namespace VierGewinnt.Visualisierer
{
    public class Konsolenvisualisierer : IVisualisierer
    {
        private readonly bool löscheKonsolenfenster;

        public Konsolenvisualisierer(bool löscheKonsolenfenster)
        {
            this.löscheKonsolenfenster = löscheKonsolenfenster;
        }

        public void Visualisiere(Spielstellung stellung)
        {
            if (löscheKonsolenfenster) Console.Clear();
            Console.WriteLine(AlsZeichenfolge(stellung));
        }

        private string AlsZeichenfolge(Spielstellung stellung)
        {
            StringBuilder zeichenfolge = new StringBuilder();

            zeichenfolge.AppendLine($"Spieler am Zug: {ZeichenFürFarbe(stellung.SpielerAmZug)}.");
            zeichenfolge.AppendLine();

            for(int i = 0; i < Spielstellung.AnzahlZeilen; i++)
            {
                zeichenfolge.Append('|');

                for (int j = 0; j < Spielstellung.AnzahlSpalten; j++)
                {
                    zeichenfolge.Append(ZeichenFürFarbe(stellung.SpielsteinFarbe(i, j)));
                    zeichenfolge.Append('|');
                }

                zeichenfolge.AppendLine();
                zeichenfolge.AppendLine(Enumerable.Repeat('-', Spielstellung.AnzahlSpalten).ToString());
            }

            zeichenfolge.AppendLine();
            return zeichenfolge.ToString();
        }

        private char ZeichenFürFarbe(Farbe farbe)
        {
            return farbe switch
            {
                Farbe.Rot => 'x',
                Farbe.Gelb => 'o',
                Farbe.Keine => ' ',
                _ => throw new ArgumentException($"Unbekannte Farbe: {farbe}.")
            };
        }
    }
}
