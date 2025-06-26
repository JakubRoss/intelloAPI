using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdatePozycjaDokumentuDto
    {
        [Required]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Ilość musi być większa niż 0.")]
        public decimal Ilosc { get; set; }
    }
}
