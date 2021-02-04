using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VierGewinnt.Spiel;

namespace VierGewinnt.Spieler
{
    // Wählt immer das nächstmögliche freie Feld aus
    public class MisterBoring : ISpieler
    {
        public Spielzug BerechneNächstenSpielzug(Spielstellung stellung)
        {
            return stellung.MöglicheZüge().First();
        }
    }
}
