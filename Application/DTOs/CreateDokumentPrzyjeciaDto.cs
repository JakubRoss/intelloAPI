using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateDokumentPrzyjeciaDto
    {
        [Required]
        [StringLength(50)]
        public string Symbol { get; set; }
        [Required]
        public int KontrahentId { get; set; }
        [Required]
        [Range(0.0001, double.MaxValue, ErrorMessage = "Ilość musi być większa niż 0.")]
        public int Ilosc { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera.")]
        [Required(ErrorMessage = "Id jest wymagany.")]
        public int TowarId { get; set; } 
    }
}
