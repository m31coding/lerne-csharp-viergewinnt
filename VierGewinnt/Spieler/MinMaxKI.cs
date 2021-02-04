using System;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    public class MinMaxKI : ISpieler
    {
        private readonly IHeuristik heuristik;
        private readonly int berechneBisTiefe;

        public MinMaxKI(IHeuristik heuristik, int berechnetBisTiefe)
        {
            this.heuristik = heuristik;
            this.berechneBisTiefe = berechnetBisTiefe;
        }

        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            throw new NotImplementedException();
        }
    }
}
