using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler.Heuristiken
{
    public class LooserHeuristik : IHeuristik
    {
        private readonly IHeuristik heuristik;

        public LooserHeuristik(IHeuristik heuristik)
        {
            this.heuristik = heuristik;
        }

        public double BewerteSpielstellungFürRot(Spielstellung stellung, Stellungsanalyse analyse)
        {
            return -heuristik.BewerteSpielstellungFürRot(stellung, analyse);
        }
    }
}
