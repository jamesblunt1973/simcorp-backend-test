using Backend_Test.Dtos;
using Backend_Test.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyCollection<ProductResponse>> GetAll() =>
            Ok(productService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<ProductResponse> GetById(int id) =>
            FromResult(productService.GetById(id));

        [HttpPost]
        public ActionResult<ProductResponse> Add(ProductRequest request)
        {
            var result = productService.Add(request);
            if (!result.IsSuccess)
            {
                return FromResult(result);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public ActionResult<ProductResponse> Update(int id, ProductRequest request) =>
            FromResult(productService.Update(id, request));

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) =>
            FromResult(productService.Delete(id));
    }
}
