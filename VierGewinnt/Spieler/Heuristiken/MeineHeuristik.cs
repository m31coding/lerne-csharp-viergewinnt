using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler.Heuristiken
{
    class MeineHeuristik : IHeuristik
    {
        public double BewerteSpielstellungFürRot(Spielstellung stellung, Stellungsanalyse analyse)
        {
            if (analyse.Spielstand != Spielstand.Offen)
            {
                return HeuristikHelfer.BewerteAusgangFürRot(analyse.Spielstand);
            }

            return 0; // todo
        }
    }
}
