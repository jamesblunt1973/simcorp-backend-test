using Backend_Test.Common;
using Backend_Test.Dtos;
using Backend_Test.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Backend_Test.Services
{
    public interface IPurchaseService
    {
        IReadOnlyCollection<PurchaseResponse> GetAll();
        Result<PurchaseResponse> GetById(int id);
        Result<IReadOnlyCollection<PurchaseResponse>> GetByCustomerId(int customerId);
        Result<PurchaseResponse> Add(PurchaseRequest request);
        Result Delete(int id);
        Result DeleteFromCustomer(int customerId);
        Result<CsvReport> GetReport(int customerId);
    }

    public sealed class PurchaseService : IPurchaseService
    {
		// using da-DK, Same as the example report
		private static readonly CultureInfo ReportCulture = CultureInfo.GetCultureInfo("da-DK");

		private readonly ILogger<PurchaseService> _logger;

        public PurchaseService(ILogger<PurchaseService> logger)
        {
            _logger = logger;
        }

        public IReadOnlyCollection<PurchaseResponse> GetAll() =>
            Data.Purchases.Values.Select(Map).ToList();

        public Result<PurchaseResponse> GetById(int id) =>
            Data.Purchases.TryGetValue(id, out var purchase)
                ? Result<PurchaseResponse>.Success(Map(purchase))
                : Result<PurchaseResponse>.NotFound($"Purchase {id} was not found.");

        public Result<IReadOnlyCollection<PurchaseResponse>> GetByCustomerId(int customerId)
        {
            var purchases = Data.Purchases.Values
                .Where(p => p.CustomerId == customerId)
                .Select(Map)
                .ToList();

            if (purchases.Count == 0)
            {
                return Result<IReadOnlyCollection<PurchaseResponse>>.NotFound(
                    $"No purchases found for customer {customerId}.");
            }

            return Result<IReadOnlyCollection<PurchaseResponse>>.Success(purchases);
        }

        public Result<PurchaseResponse> Add(PurchaseRequest request)
        {
            if (!Data.Persons.ContainsKey(request.CustomerId))
            {
                return Result<PurchaseResponse>.Invalid($"Customer {request.CustomerId} does not exist.");
            }

            var unknownProducts = request.ProductIds
                .Where(productId => !Data.Products.ContainsKey(productId))
                .ToList();

            if (unknownProducts.Count > 0)
            {
                return Result<PurchaseResponse>.Invalid(
                    $"Unknown product id(s): {string.Join(", ", unknownProducts)}.");
            }

            var purchase = new Purchase
            {
                Id = Data.NextPurchaseId(),
                CustomerId = request.CustomerId,
                ProductIds = [.. request.ProductIds]
			};

            Data.Purchases[purchase.Id] = purchase;
            _logger.LogInformation("Created purchase {PurchaseId} for customer {CustomerId}.",
                purchase.Id, purchase.CustomerId);
            return Result<PurchaseResponse>.Success(Map(purchase));
        }

        public Result Delete(int id)
        {
            if (!Data.Purchases.TryRemove(id, out _))
            {
                return Result.NotFound($"Purchase {id} was not found.");
            }

            return Result.Success();
        }

        public Result DeleteFromCustomer(int customerId)
        {
            var ids = Data.Purchases.Values
                .Where(p => p.CustomerId == customerId)
                .Select(p => p.Id)
                .ToList();

            if (ids.Count == 0)
            {
                return Result.NotFound($"No purchases found for customer {customerId}.");
            }

            foreach (var id in ids)
            {
                Data.Purchases.TryRemove(id, out _);
            }

            return Result.Success();
        }

        public Result<CsvReport> GetReport(int customerId)
        {
            var customerPurchases = Data.Purchases.Values
                .Where(p => p.CustomerId == customerId)
                .ToList();

            if (customerPurchases.Count == 0)
            {
                return Result<CsvReport>.NotFound($"No purchases found for customer {customerId}.");
            }

            if (!Data.Persons.TryGetValue(customerId, out var customer))
            {
                return Result<CsvReport>.NotFound($"Customer {customerId} does not exist.");
            }

            var builder = new StringBuilder();
            builder.AppendLine($"CustomerName:;{customer.FirstName} {customer.LastName}");
            builder.AppendLine("ProductId;Count;ProductName;Price");

            var productCounts = customerPurchases
                .SelectMany(purchase => purchase.ProductIds)
                .GroupBy(productId => productId)
                .OrderBy(group => group.Key);

            foreach (var group in productCounts)
            {
                if (!Data.Products.TryGetValue(group.Key, out var product))
                {
                    continue;
                }

                var price = product.Price.ToString("0.00", ReportCulture);
                builder.AppendLine($"{product.Id};{group.Count()};{product.Name};{price}");
            }

            var content = Encoding.UTF8.GetBytes(builder.ToString());
            var report = new CsvReport(content, $"purchase-report-customer-{customerId}.csv");
			return Result<CsvReport>.Success(report);
        }

        private static PurchaseResponse Map(Purchase purchase) =>
            new(purchase.Id, purchase.CustomerId, purchase.ProductIds.AsReadOnly());
    }
}
