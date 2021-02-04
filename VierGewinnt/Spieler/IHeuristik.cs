using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    public interface IHeuristik
    {
        float BewerteSpielStellungFürRot(Spielstellung stellung);
    }
}
