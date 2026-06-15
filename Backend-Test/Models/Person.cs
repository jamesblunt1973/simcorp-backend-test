namespace Backend_Test.Models
{
    public class Person
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int YearOfBirth { get; set; }
    }
}
