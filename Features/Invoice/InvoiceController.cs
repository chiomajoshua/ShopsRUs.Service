using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSwag.Annotations;
using ShopsRUs.Service.Features.Invoice.Models;
using ShopsRUs.Service.Features.Invoice.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Invoice
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }

        [HttpPost(Name = nameof(CalculateBill))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> CalculateBill(InvoiceRequest invoiceRequest)
        {
            _logger.LogInformation($"User Request: {JsonConvert.SerializeObject(invoiceRequest)}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _invoiceService.CalculateBill(invoiceRequest);
            return response > 0.0 ? (IActionResult)Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
