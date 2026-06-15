using System.ComponentModel.DataAnnotations;

namespace Backend_Test.Dtos
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Range(0.0, 1_000_000.0)]
        public decimal Price { get; set; }
    }

    public record ProductResponse(int Id, string Name, string Type, decimal Price);
}
