namespace Domain.Entities
{
    public class Kontrahent
    {
        public int Id { get; set; }

        public string Symbol { get; set; }
        public string Nazwa { get; set; }

        public List<DokumentPrzyjecia> DokumentyPrzyjecia { get; set; } = new List<DokumentPrzyjecia>();
    }
}
