using Microsoft.VisualStudio.TestTools.UnitTesting;
using VierGewinnt.Spiel;

namespace VierGewinntTests
{
    [TestClass]
    public class StellungsanalyseTests
    {
        [TestMethod]
        public void SpielstandVonLeererStellungIstOffen()
        {
            Spielstellung stellung = new Spielstellung();
            Spielstand spielstand = Stellungsanalyse.ErhalteSpielstand(stellung);
            Assert.AreEqual(Spielstand.Offen, spielstand);
        }

        /*
         *      0 1 2 3 4 5 6   
         *   0  - - - - - - -
         *   1  - - - - - - -
         *   2  - - R - - - -
         *   3  - - R - - - -
         *   4  - - R - - - -
         *   5  - G R G G - -
         *   
         */

        [TestMethod]
        public void RotKannVertikalGewinnen()
        {
            Farbe[,] brett = ErhalteLeeresSpielbrett();
            brett[5, 1] = Farbe.Gelb;
            brett[5, 2] = Farbe.Rot;
            brett[5, 3] = Farbe.Gelb;
            brett[5, 4] = Farbe.Gelb;
            brett[4, 2] = Farbe.Rot;
            brett[3, 2] = Farbe.Rot;
            brett[2, 2] = Farbe.Rot;

            Spielstellung stellung = new Spielstellung(brett, new Spielzug(Farbe.Rot, 2));
            Assert.AreEqual(Spielstand.RotIstSieger, Stellungsanalyse.ErhalteSpielstand(stellung));
        }

        /*
         *      0 1 2 3 4 5 6   
         *   0  - - - - - - -
         *   1  - - - - - - -
         *   2  - - - - - - -
         *   3  - - - - - - -
         *   4  G G G G - - -
         *   5  G R R R - R R
         *   
         */

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void GelbKannHorizontalGewinnen(int spalteVonLetztemZug)
        {
            Farbe[,] brett = ErhalteLeeresSpielbrett();
            brett[5, 0] = Farbe.Gelb;
            brett[5, 1] = Farbe.Rot;
            brett[5, 2] = Farbe.Rot;
            brett[5, 3] = Farbe.Rot;
            brett[5, 5] = Farbe.Rot;
            brett[5, 6] = Farbe.Rot;
            brett[4, 0] = Farbe.Gelb;
            brett[4, 1] = Farbe.Gelb;
            brett[4, 2] = Farbe.Gelb;
            brett[4, 3] = Farbe.Gelb;

            Spielstellung stellung = new Spielstellung(brett, new Spielzug(Farbe.Gelb, spalteVonLetztemZug));
            Assert.AreEqual(Spielstand.GelbIstSieger, Stellungsanalyse.ErhalteSpielstand(stellung));
        }

        /*
         *      0 1 2 3 4 5 6   
         *   0  - - - - - - -
         *   1  - - - - - - -
         *   2  - - - R - - -
         *   3  - - R G - - -
         *   4  - R G G - - -
         *   5  R G G R R - -
         *   
         */

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void RotKannDiagonalGewinnen(int spalteVonLetztemZug)
        {
            Farbe[,] brett = ErhalteLeeresSpielbrett();
            brett[5, 0] = Farbe.Rot;
            brett[5, 1] = Farbe.Gelb;
            brett[5, 2] = Farbe.Gelb;
            brett[5, 3] = Farbe.Rot;
            brett[5, 4] = Farbe.Rot;
            brett[4, 1] = Farbe.Rot;
            brett[4, 2] = Farbe.Gelb;
            brett[4, 3] = Farbe.Gelb;
            brett[3, 2] = Farbe.Rot;
            brett[3, 3] = Farbe.Gelb;
            brett[2, 3] = Farbe.Rot;

            Spielstellung stellung = new Spielstellung(brett, new Spielzug(Farbe.Rot, spalteVonLetztemZug));
            Assert.AreEqual(Spielstand.RotIstSieger, Stellungsanalyse.ErhalteSpielstand(stellung));
        }

        /*
         *      0 1 2 3 4 5 6   
         *   0  R G R G R G R
         *   1  R G R G R G R
         *   2  G R G R G R G
         *   3  G R G R G R G
         *   4  R G R G R G R
         *   5  R G R G R G R
         *   
         */

        [TestMethod]
        public void KannUnentschiedenSpielen()
        {
            Farbe[,] brett = ErhalteLeeresSpielbrett();

            F�lleZeileAbwechselnd(0, Farbe.Rot);
            F�lleZeileAbwechselnd(1, Farbe.Rot);
            F�lleZeileAbwechselnd(2, Farbe.Gelb);
            F�lleZeileAbwechselnd(3, Farbe.Gelb);
            F�lleZeileAbwechselnd(4, Farbe.Rot);
            F�lleZeileAbwechselnd(5, Farbe.Rot);

            void F�lleZeileAbwechselnd(int zeile, Farbe ersteFarbe)
            {
                Farbe n�chsteFarbe = ersteFarbe;

                for(int j = 0; j<Spielstellung.AnzahlSpalten; j++)
                {
                    brett[zeile, j] = n�chsteFarbe;
                    n�chsteFarbe = n�chsteFarbe == Farbe.Rot ? Farbe.Gelb : Farbe.Rot;
                }
            }

            Spielstellung stellung = new Spielstellung(brett, new Spielzug(Farbe.Rot, 6));
            Assert.AreEqual(Spielstand.Unentschieden, Stellungsanalyse.ErhalteSpielstand(stellung));
        }

        private Farbe[,] ErhalteLeeresSpielbrett()
        {
            return new Farbe[Spielstellung.AnzahlZeilen, Spielstellung.AnzahlSpalten];
        }
    }
}
