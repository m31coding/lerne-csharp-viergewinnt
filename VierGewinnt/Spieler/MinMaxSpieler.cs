using System;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    public class MinMaxSpieler : ISpieler
    {
        private readonly IHeuristik heuristik;
        private readonly int berechneBisTiefe;

        public MinMaxSpieler(IHeuristik heuristik, int berechnetBisTiefe)
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
