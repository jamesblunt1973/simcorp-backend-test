using Backend_Test.Common;
using Backend_Test.Dtos;
using Backend_Test.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Test.Services
{
    public interface IProductService
    {
        IReadOnlyCollection<ProductResponse> GetAll();
        Result<ProductResponse> GetById(int id);
        Result<ProductResponse> Add(ProductRequest request);
        Result<ProductResponse> Update(int id, ProductRequest request);
        Result Delete(int id);
    }

    public sealed class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        public IReadOnlyCollection<ProductResponse> GetAll() =>
            Data.Products.Values.Select(Map).ToList();

        public Result<ProductResponse> GetById(int id) =>
            Data.Products.TryGetValue(id, out var product)
                ? Result<ProductResponse>.Success(Map(product))
                : Result<ProductResponse>.NotFound($"Product {id} was not found.");

        public Result<ProductResponse> Add(ProductRequest request)
        {
            var product = new Product
            {
                Id = Data.NextProductId(),
                Name = request.Name,
                Type = request.Type,
                Price = request.Price
            };

            Data.Products[product.Id] = product;
            _logger.LogInformation("Created product {ProductId}.", product.Id);
            return Result<ProductResponse>.Success(Map(product));
        }

        public Result<ProductResponse> Update(int id, ProductRequest request)
        {
            if (!Data.Products.ContainsKey(id))
            {
                return Result<ProductResponse>.NotFound($"Product {id} was not found.");
            }

            var product = new Product
            {
                Id = id,
                Name = request.Name,
                Type = request.Type,
                Price = request.Price
            };

            Data.Products[id] = product;
            return Result<ProductResponse>.Success(Map(product));
        }

        public Result Delete(int id)
        {
            if (!Data.Products.TryRemove(id, out _))
            {
                _logger.LogWarning("Attempted to delete non-existent product {ProductId}.", id);
                return Result.NotFound($"Product {id} was not found.");
            }

            return Result.Success();
        }

        private static ProductResponse Map(Product product) =>
            new(product.Id, product.Name, product.Type, product.Price);
    }
}
