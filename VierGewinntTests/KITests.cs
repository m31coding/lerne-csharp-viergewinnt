using Microsoft.VisualStudio.TestTools.UnitTesting;
using VierGewinnt.Spiel;
using VierGewinnt.Spieler;

namespace VierGewinntTests
{
    [TestClass]
    public class KITests
    {
        /*
         *      0 1 2 3 4 5 6   
         *   0  - - - - - - -
         *   1  - - - - - - -
         *   2  - - - G - - -
         *   3  - - - R G - -
         *   4  - - R R R G -
         *   5  - - R R G G -
         *   
         */

        [TestMethod]
        public void MinMaxKIMöchteDiagonalGewinnen()
        {
            ISpieler ki = KIs.ErstelleEinfacheKI(2);

            Farbe[,] brett = TestHelfer.ErhalteLeeresSpielbrett();
            brett[5, 2] = Farbe.Rot;
            brett[5, 3] = Farbe.Rot;
            brett[5, 4] = Farbe.Gelb;
            brett[5, 5] = Farbe.Gelb;
            brett[4, 2] = Farbe.Rot;
            brett[4, 3] = Farbe.Rot;
            brett[4, 4] = Farbe.Rot;
            brett[4, 5] = Farbe.Gelb;
            brett[3, 3] = Farbe.Rot;
            brett[3, 4] = Farbe.Gelb;
            brett[2, 3] = Farbe.Gelb;

            Spielstellung stellung = new Spielstellung(brett, Farbe.Gelb);

            Spielzug spielzug = ki.BerechneNächstenSpielzug(stellung);
            Assert.AreEqual(Farbe.Gelb, spielzug.Farbe);
            Assert.AreEqual(6, spielzug.Spalte);
        }
    }
}
