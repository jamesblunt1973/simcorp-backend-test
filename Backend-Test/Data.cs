using Backend_Test.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Backend_Test
{
    /// <summary>
    /// NOT PART OF TEST. MERELY TO SUPPLY DATA
    /// </summary>
    public static class Data
    {
        public static readonly ConcurrentDictionary<int, Person> Persons =
            new(SeedPersons.ToDictionary(p => p.Id));

        public static readonly ConcurrentDictionary<int, Product> Products =
            new(SeedProducts.ToDictionary(p => p.Id));

        public static readonly ConcurrentDictionary<int, Purchase> Purchases =
            new(SeedPurchases.ToDictionary(p => p.Id));

        private static int _personId = 10;
        private static int _productId = 10;
        private static int _purchaseId = 39;

        public static int NextPersonId() => Interlocked.Increment(ref _personId);
        public static int NextProductId() => Interlocked.Increment(ref _productId);
        public static int NextPurchaseId() => Interlocked.Increment(ref _purchaseId);

        private static List<Person> SeedPersons =>
        [
            new Person { Id = 1, FirstName = "John", LastName = "Doe", YearOfBirth = 1980 },
            new Person { Id = 2, FirstName = "Jane", LastName = "Doe", YearOfBirth = 1985 },
            new Person { Id = 3, FirstName = "Bob", LastName = "Smith", YearOfBirth = 1990 },
            new Person { Id = 4, FirstName = "Alice", LastName = "Johnson", YearOfBirth = 1995 },
            new Person { Id = 5, FirstName = "Mike", LastName = "Brown", YearOfBirth = 1982 },
            new Person { Id = 6, FirstName = "Samantha", LastName = "Davis", YearOfBirth = 1987 },
            new Person { Id = 7, FirstName = "David", LastName = "Wilson", YearOfBirth = 1992 },
            new Person { Id = 8, FirstName = "Emily", LastName = "Taylor", YearOfBirth = 1997 },
            new Person { Id = 9, FirstName = "Chris", LastName = "Anderson", YearOfBirth = 1984 },
            new Person { Id = 10, FirstName = "Jessica", LastName = "Thomas", YearOfBirth = 1989 }
        ];

        private static List<Product> SeedProducts =>
        [
            new Product { Id = 1, Name = "Pipe Wrench", Type = "Plumbing", Price = 19.99m },
            new Product { Id = 2, Name = "Electric Drill", Type = "Electric", Price = 49.99m },
            new Product { Id = 3, Name = "Garden Hose", Type = "Gardening", Price = 4.99m },
            new Product { Id = 4, Name = "Toilet Plunger", Type = "Plumbing", Price = 1.49m },
            new Product { Id = 5, Name = "Electric Screwdriver", Type = "Electric", Price = 29.99m },
            new Product { Id = 6, Name = "Garden Shovel", Type = "Gardening", Price = 12.99m },
            new Product { Id = 7, Name = "Faucet", Type = "Plumbing", Price = 24.99m },
            new Product { Id = 8, Name = "Electric Saw", Type = "Electric", Price = 89.99m },
            new Product { Id = 9, Name = "Garden Gloves", Type = "Gardening", Price = 3.99m },
            new Product { Id = 10, Name = "Pipe Cutter", Type = "Plumbing", Price = 14.99m }
        ];

        private static List<Purchase> SeedPurchases =>
        [
            new Purchase { Id = 1, CustomerId = 1, ProductIds = [1] },
            new Purchase { Id = 2, CustomerId = 1, ProductIds = [2] },
            new Purchase { Id = 3, CustomerId = 1, ProductIds = [3] },
            new Purchase { Id = 4, CustomerId = 2, ProductIds = [4] },
            new Purchase { Id = 5, CustomerId = 2, ProductIds = [5] },
            new Purchase { Id = 6, CustomerId = 3, ProductIds = [6] },
            new Purchase { Id = 7, CustomerId = 7, ProductIds = [7] },
            new Purchase { Id = 8, CustomerId = 7, ProductIds = [8] },
            new Purchase { Id = 9, CustomerId = 4, ProductIds = [9] },
            new Purchase { Id = 10, CustomerId = 4, ProductIds = [10] },
            new Purchase { Id = 11, CustomerId = 4, ProductIds = [4] },
            new Purchase { Id = 12, CustomerId = 4, ProductIds = [8] },
            new Purchase { Id = 13, CustomerId = 8, ProductIds = [8] },
            new Purchase { Id = 14, CustomerId = 8, ProductIds = [2] },
            new Purchase { Id = 15, CustomerId = 5, ProductIds = [1] },
            new Purchase { Id = 16, CustomerId = 5, ProductIds = [6] },
            new Purchase { Id = 17, CustomerId = 8, ProductIds = [5] },
            new Purchase { Id = 18, CustomerId = 1, ProductIds = [4] },
            new Purchase { Id = 19, CustomerId = 2, ProductIds = [6] },
            new Purchase { Id = 20, CustomerId = 3, ProductIds = [10] },
            new Purchase { Id = 21, CustomerId = 4, ProductIds = [3] },
            new Purchase { Id = 22, CustomerId = 5, ProductIds = [1] },
            new Purchase { Id = 23, CustomerId = 1, ProductIds = [6] },
            new Purchase { Id = 24, CustomerId = 2, ProductIds = [10] },
            new Purchase { Id = 25, CustomerId = 3, ProductIds = [7] },
            new Purchase { Id = 26, CustomerId = 4, ProductIds = [1] },
            new Purchase { Id = 27, CustomerId = 5, ProductIds = [6] },
            new Purchase { Id = 28, CustomerId = 1, ProductIds = [10] },
            new Purchase { Id = 29, CustomerId = 2, ProductIds = [7] },
            new Purchase { Id = 30, CustomerId = 3, ProductIds = [1] },
            new Purchase { Id = 31, CustomerId = 4, ProductIds = [6] },
            new Purchase { Id = 32, CustomerId = 5, ProductIds = [10] },
            new Purchase { Id = 33, CustomerId = 1, ProductIds = [7] },
            new Purchase { Id = 34, CustomerId = 2, ProductIds = [1] },
            new Purchase { Id = 35, CustomerId = 3, ProductIds = [6] },
            new Purchase { Id = 36, CustomerId = 4, ProductIds = [10] },
            new Purchase { Id = 37, CustomerId = 6, ProductIds = [1] },
            new Purchase { Id = 38, CustomerId = 6, ProductIds = [4] },
            new Purchase { Id = 39, CustomerId = 6, ProductIds = [7] }
        ];
    }
}
