using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    public interface ISpieler
    {
        Spielzug BerechneNächstenSpielzug(Spielstellung stellung);
    }
}
