using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdateDokumentPrzyjeciaDto
    {
        [Required]
        [StringLength(50)]
        public string Symbol { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wartość musi być większa od zera.")]
        [Required(ErrorMessage = "Id jest wymagany.")]
        public int KontrahentId { get; set; }
    }
}
