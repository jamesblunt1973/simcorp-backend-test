using Backend_Test.Common;
using Backend_Test.Dtos;
using Backend_Test.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchasesController(IPurchaseService purchaseService) : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyCollection<PurchaseResponse>> GetAll() =>
            Ok(purchaseService.GetAll());

        [HttpGet("{id}")]
        public ActionResult<PurchaseResponse> GetById(int id) =>
            FromResult(purchaseService.GetById(id));

        [HttpGet("customer/{customerId}")]
        public ActionResult<IReadOnlyCollection<PurchaseResponse>> GetByCustomerId(int customerId) =>
            FromResult(purchaseService.GetByCustomerId(customerId));

        [HttpGet("customer/{customerId}/report")]
        public ActionResult GetPurchaseReportById(int customerId)
        {
            var result = purchaseService.GetReport(customerId);

            return result.Status switch
            {
                ResultStatus.Success => File(result.Value!.Content, "text/csv", result.Value.FileName),
                ResultStatus.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }

        [HttpPost]
        public ActionResult<PurchaseResponse> Add(PurchaseRequest request)
        {
            var result = purchaseService.Add(request);
            if (!result.IsSuccess)
            {
                return FromResult(result);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) =>
            FromResult(purchaseService.Delete(id));

        [HttpDelete("customer/{customerId}")]
        public ActionResult DeleteFromCustomer(int customerId) =>
            FromResult(purchaseService.DeleteFromCustomer(customerId));
    }
}
