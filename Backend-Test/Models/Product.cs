namespace Backend_Test.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Type { get; set; }

        public decimal Price { get; set; }
    }
}
