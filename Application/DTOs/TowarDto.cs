using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class TowarDto
    {
        public int Id { get; set; }   
        public string NazwaTowaru { get; set; }
        public string JednostkaMiary { get; set; } 
    }
}
