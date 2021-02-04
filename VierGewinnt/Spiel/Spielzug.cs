namespace VierGewinnt.Spiel
{
    public readonly struct Spielzug
    {
        public Farbe Farbe { get; }
        public int Spalte { get; }

        public Spielzug(Farbe farbe, int spalte)
        {
            Farbe = farbe;
            Spalte = spalte;
        }

        public override string ToString()
        {
            return $"(Farbe={Farbe}, Spalte={Spalte})";
        }
    }
}
