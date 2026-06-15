using System.ComponentModel.DataAnnotations;

namespace Backend_Test.Dtos
{
    public class PersonRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int YearOfBirth { get; set; }
    }

    public record PersonResponse(int Id, string FirstName, string LastName, int YearOfBirth);
}
