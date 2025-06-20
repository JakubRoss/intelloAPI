namespace Domain.Entities
{ 
    public class PozycjaDokumentu
    {
        public int Id { get; set; }

        public int DokumentPrzyjeciaId { get; set; }
        public DokumentPrzyjecia DokumentPrzyjecia { get; set; }

        public int TowarId { get; set; }
        public Towar Towar { get; set; }

        public decimal Ilosc { get; set; }
    }
}
