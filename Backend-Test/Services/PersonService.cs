using Backend_Test.Common;
using Backend_Test.Dtos;
using Backend_Test.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Test.Services
{
    public interface IPersonService
    {
        IReadOnlyCollection<PersonResponse> GetAll();
        Result<PersonResponse> GetById(int id);
        Result<PersonResponse> Add(PersonRequest request);
        Result<PersonResponse> Update(int id, PersonRequest request);
        Result Delete(int id);
    }

    public sealed class PersonService : IPersonService
    {
        private readonly TimeProvider _timeProvider;
        private readonly ILogger<PersonService> _logger;

        public PersonService(TimeProvider timeProvider, ILogger<PersonService> logger)
        {
            _timeProvider = timeProvider;
            _logger = logger;
        }

        public IReadOnlyCollection<PersonResponse> GetAll() =>
            Data.Persons.Values.Select(Map).ToList();

        public Result<PersonResponse> GetById(int id) =>
            Data.Persons.TryGetValue(id, out var person)
                ? Result<PersonResponse>.Success(Map(person))
                : Result<PersonResponse>.NotFound($"Person {id} was not found.");

        public Result<PersonResponse> Add(PersonRequest request)
        {
            if (request.YearOfBirth > _timeProvider.GetUtcNow().Year)
            {
                return Result<PersonResponse>.Invalid("Year of birth cannot be in the future.");
            }

            var person = new Person
            {
                Id = Data.NextPersonId(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                YearOfBirth = request.YearOfBirth
            };

            Data.Persons[person.Id] = person;
            _logger.LogInformation("Created person {PersonId}.", person.Id);
            return Result<PersonResponse>.Success(Map(person));
        }

        public Result<PersonResponse> Update(int id, PersonRequest request)
        {
            if (!Data.Persons.ContainsKey(id))
            {
                return Result<PersonResponse>.NotFound($"Person {id} was not found.");
            }

            if (request.YearOfBirth > _timeProvider.GetUtcNow().Year)
            {
                return Result<PersonResponse>.Invalid("Year of birth cannot be in the future.");
            }

            var person = new Person
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                YearOfBirth = request.YearOfBirth
            };

            Data.Persons[id] = person;
            return Result<PersonResponse>.Success(Map(person));
        }

        public Result Delete(int id)
        {
            if (!Data.Persons.TryRemove(id, out _))
            {
                _logger.LogWarning("Attempted to delete non-existent person {PersonId}.", id);
                return Result.NotFound($"Person {id} was not found.");
            }

            return Result.Success();
        }

        private static PersonResponse Map(Person person) =>
            new(person.Id, person.FirstName, person.LastName, person.YearOfBirth);
    }
}
