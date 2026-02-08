using System.ComponentModel.DataAnnotations;

namespace RESTFull_API.DTO
{
    public sealed class CreateRollDto
    {
        [Required]
        [Range(0.0001, double.MaxValue)]
        public decimal Lenght { get; set; }

        [Required]
        [Range(0.0001, double.MaxValue)]
        public decimal Weight { get; set; }
    }
}
