namespace VierGewinnt.Spiel
{
    public readonly struct Spielzug
    {
        public Spielzug(Farbe farbe, int spalte)
        {
            Farbe = farbe;
            Spalte = spalte;
        }

        public Farbe Farbe { get; }
        public int Spalte { get; }

        public override string ToString()
        {
            return $"(Farbe={Farbe}, Spalte={Spalte})";
        }
    }
}
