using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler.Heuristiken
{
    public interface IHeuristik
    {
        double BewerteSpielstellungFürRot(Spielstellung stellung, Stellungsanalyse analyse);
    }
}
