namespace Application.DTOs
{
    public class DokumentPrzyjeciaDto
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Symbol { get; set; }
        public KontrahentDto Kontrahent { get; set; }
        public List<PozycjaDokumentuDto> Pozycje { get; set; } = new List<PozycjaDokumentuDto>();
    }
}
