namespace VierGewinnt.Spiel
{
    public class Verbindungen
    {
        public Verbindungen(
            int anzahlZweierverbindungen,
            int anzahlDreierverbindungen,
            int anzahlViererverbindungen)
        {
            AnzahlZweierverbindungen = anzahlZweierverbindungen;
            AnzahlDreierverbindungen = anzahlDreierverbindungen;
            AnzahlViererverbindungen = anzahlViererverbindungen;
        }

        public Verbindungen() 
            : this(0, 0, 0)
        {

        }

        public int AnzahlZweierverbindungen { get; set; } = 0;
        public int AnzahlDreierverbindungen { get; set; } = 0;
        public int AnzahlViererverbindungen { get; set; } = 0;
    }
}
