using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class UpdateTowarDto
    {
        [Required]
        [StringLength(100)]
        public string Nazwa { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string JednostkaMiary { get; set; } = string.Empty;
    }
}
