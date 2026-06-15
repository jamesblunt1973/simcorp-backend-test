using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend_Test.Dtos
{
    public class PurchaseRequest
    {
        public int CustomerId { get; set; }

        [Required]
        [MinLength(1)]
        public List<int> ProductIds { get; set; } = new();
    }

    public record PurchaseResponse(int Id, int CustomerId, IReadOnlyList<int> ProductIds);

    public record CsvReport(byte[] Content, string FileName);
}
