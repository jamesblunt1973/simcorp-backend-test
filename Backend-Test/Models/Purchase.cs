using System.Collections.Generic;

namespace Backend_Test.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public required List<int> ProductIds { get; set; }
    }
}
