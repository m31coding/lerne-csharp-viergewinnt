namespace VierGewinnt.Spieler
{
    public class SpielerMitName
    {
        public SpielerMitName(ISpieler spieler, string name)
        {
            Spieler = spieler;
            Name = name;
        }

        public ISpieler Spieler { get; }
        public string Name { get; }
    }
}
