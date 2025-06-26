using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreatePozycjaDokumentuDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera.")]
        [Required(ErrorMessage = "Id jest wymagany.")]
        public int TowarId { get; set; }

        [Required]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Ilość musi być większa niż 0.")]
        public decimal Ilosc { get; set; }
    }
}
