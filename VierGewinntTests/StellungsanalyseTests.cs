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
            Spielstand spielstand = Stellungsanalyse.ErstelleAnalyse(stellung).Spielstand;
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

            Spielstellung stellung = new Spielstellung(brett, Farbe.Gelb);
            Stellungsanalyse analyse = Stellungsanalyse.ErstelleAnalyse(stellung);
            Verbindungen verbindungenRotErwartet = new Verbindungen(0, 0, 1);
            Verbindungen verbindungenGelbErwartet = new Verbindungen(1, 0, 0);
            Assert.IsTrue(SindGleich(verbindungenRotErwartet, analyse.VerbindungenRot));
            Assert.IsTrue(SindGleich(verbindungenGelbErwartet, analyse.VerbindungenGelb));
            Assert.AreEqual(Spielstand.RotIstSieger, analyse.Spielstand);
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
        public void GelbKannHorizontalGewinnen()
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

            Spielstellung stellung = new Spielstellung(brett, Farbe.Rot);
            Stellungsanalyse analyse = Stellungsanalyse.ErstelleAnalyse(stellung);
            Verbindungen verbindungenRotErwartet = new Verbindungen(1, 1, 0);
            Verbindungen verbindungenGelbErwartet = new Verbindungen(2, 0, 1);
            Assert.IsTrue(SindGleich(verbindungenRotErwartet, analyse.VerbindungenRot));
            Assert.IsTrue(SindGleich(verbindungenGelbErwartet, analyse.VerbindungenGelb));
            Assert.AreEqual(Spielstand.GelbIstSieger, analyse.Spielstand);
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
        public void RotKannDiagonalGewinnen()
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

            Spielstellung stellung = new Spielstellung(brett, Farbe.Gelb);
            Stellungsanalyse analyse = Stellungsanalyse.ErstelleAnalyse(stellung);
            Verbindungen verbindungenRotErwartet = new Verbindungen(1, 0, 1);
            Verbindungen verbindungenGelbErwartet = new Verbindungen(5, 1, 0);
            Assert.IsTrue(SindGleich(verbindungenRotErwartet, analyse.VerbindungenRot));
            Assert.IsTrue(SindGleich(verbindungenGelbErwartet, analyse.VerbindungenGelb));
            Assert.AreEqual(Spielstand.RotIstSieger, analyse.Spielstand);
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

            Spielstellung stellung = new Spielstellung(brett, Farbe.Gelb);
            Stellungsanalyse analyse = Stellungsanalyse.ErstelleAnalyse(stellung);
            Assert.AreEqual(Spielstand.Unentschieden, analyse.Spielstand);
        }

        private Farbe[,] ErhalteLeeresSpielbrett()
        {
            return new Farbe[Spielstellung.AnzahlZeilen, Spielstellung.AnzahlSpalten];
        }

        private bool SindGleich(Verbindungen verbindungen1, Verbindungen verbindungen2)
        {
            return verbindungen1.AnzahlZweierverbindungen == verbindungen2.AnzahlZweierverbindungen &&
                   verbindungen1.AnzahlDreierverbindungen == verbindungen2.AnzahlDreierverbindungen &&
                   verbindungen1.AnzahlViererverbindungen == verbindungen2.AnzahlViererverbindungen;
        }
    }
}
