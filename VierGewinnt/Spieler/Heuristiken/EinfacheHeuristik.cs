using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler.Heuristiken
{
    public class EinfacheHeuristik : IHeuristik
    {
        public EinfacheHeuristik()
        {

        }

        public double BewerteSpielstellungFürRot(Spielstellung stellung, Stellungsanalyse analyse)
        {
            if(analyse.Spielstand != Spielstand.Offen)
            {
                return HeuristikHelfer.BewerteAusgangFürRot(analyse.Spielstand);
            }

            double wertung = 0;

            wertung += analyse.AnzahlRoteSteine;
            wertung += analyse.VerbindungenRot.AnzahlZweierverbindungen;
            wertung += analyse.VerbindungenRot.AnzahlDreierverbindungen;

            wertung -= analyse.AnzahlGelbeSteine;
            wertung -= analyse.VerbindungenGelb.AnzahlZweierverbindungen;
            wertung -= analyse.VerbindungenGelb.AnzahlDreierverbindungen;

            return wertung;
        }
    }
}
