using Backend_Test.Dtos;
using Backend_Test.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController(IPersonService personService) : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyCollection<PersonResponse>> GetAll() =>
            Ok(personService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<PersonResponse> GetById(int id) =>
            FromResult(personService.GetById(id));

        [HttpPost]
        public ActionResult<PersonResponse> Add(PersonRequest request)
        {
            var result = personService.Add(request);
            if (!result.IsSuccess)
            {
                return FromResult(result);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public ActionResult<PersonResponse> Update(int id, PersonRequest request) =>
            FromResult(personService.Update(id, request));

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) =>
            FromResult(personService.Delete(id));
    }
}
