namespace Application.DTOs
{
    public class PozycjaDokumentuDto
    {
        public int Id { get; set; }
        public int TowarId { get; set; }
        public int DokumentPrzyjeciaId { get; set; }
        public string NazwaTowaru { get; set; } = string.Empty;
        public string JednostkaMiary { get; set; } = string.Empty;
        public decimal Ilosc { get; set; }
    }
}