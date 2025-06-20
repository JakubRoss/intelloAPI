namespace Domain.Entities
{
    public class Towar
    {
        public int Id { get; set; }
        public string NazwaTowaru { get; set; }
        public string JednostkaMiary { get; set; }

        public ICollection<PozycjaDokumentu> Pozycje { get; set; } = new List<PozycjaDokumentu>();
    }

}
