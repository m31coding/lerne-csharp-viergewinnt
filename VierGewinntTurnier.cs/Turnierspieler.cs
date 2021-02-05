using VierGewinnt.Spieler;

namespace VierGewinntTurnier.cs
{
    public class Turnierspieler
    {
        private readonly SpielerMitName spieler;

        public Turnierspieler(SpielerMitName spieler)
        {
            this.spieler = spieler;
        }

        public ISpieler Spieler => spieler.Spieler;
        public string Name => spieler.Name;
        public int Siege { get; set; }
        public int Niederlagen { get; set; }
        public int Unentschieden { get; set; }
        public int Punkte { get; set; }
        public int Platzierung { get; set; }

        public void VerzeichneSieg()
        {
            Siege++;
            Punkte += 3;
        }

        public void VerzeichneUnentschieden()
        {
            Unentschieden++;
            Punkte += 1;
        }

        public void VerzeichenNiederlage()
        {
            Niederlagen++;
        }
    }
}
