using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CreateKontrahentDto
    {
        [Required]
        [StringLength(50)]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [StringLength(150)]
        public string Nazwa { get; set; } = string.Empty;
    }
}
