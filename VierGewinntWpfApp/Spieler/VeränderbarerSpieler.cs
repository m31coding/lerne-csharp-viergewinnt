using System;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;

namespace VierGewinntWpfApp.Spieler
{
    public class VeränderbarerSpieler : ISpieler
    {
        private ISpieler spieler;
        private object locker;

        public VeränderbarerSpieler(ISpieler spieler)
        {
            this.spieler = spieler;
            locker = new object();
        }

        public ISpieler Spieler
        {
            get
            {
                lock (locker)
                {
                    return spieler;
                }
            }
            set
            {
                lock (locker)
                {
                    spieler = value;
                }
            }
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            return Spieler.BerechneNächstenSpielzug(stellung);
        }
    }
}
