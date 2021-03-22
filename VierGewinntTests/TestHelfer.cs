using VierGewinnt.Spiel;

namespace VierGewinntTests
{
    public static class TestHelfer
    {
        public static Farbe[,] ErhalteLeeresSpielbrett()
        {
            return new Farbe[Spielstellung.AnzahlZeilen, Spielstellung.AnzahlSpalten];
        }
    }
}
