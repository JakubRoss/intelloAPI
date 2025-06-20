namespace Domain.Entities
{
    public class DokumentPrzyjecia
    {
        public int Id { get; set; }

        public DateTime Data { get; set; }
        public string Symbol { get; set; }

        public int KontrahentId { get; set; }
        public Kontrahent Kontrahent { get; set; }

        public List<PozycjaDokumentu> Pozycje { get; set; } = new List<PozycjaDokumentu>();
    }
}
