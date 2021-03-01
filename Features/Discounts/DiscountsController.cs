using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSwag.Annotations;
using ShopsRUs.Service.Features.Discounts.Models;
using ShopsRUs.Service.Features.Discounts.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Discounts
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly ILogger<DiscountsController> _logger;
        public DiscountsController(IDiscountService discountService, ILogger<DiscountsController> logger)
        {
            _discountService = discountService;
            _logger = logger;
        }

        [HttpPost(Name = nameof(AddDiscountType))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> AddDiscountType(CreateDiscountTypeRequest createDiscountTypeRequest)
        {
            _logger.LogInformation($"User Request: {JsonConvert.SerializeObject(createDiscountTypeRequest)}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _discountService.IsDiscountExists(createDiscountTypeRequest.DiscountType)) return Conflict();

            var response = await _discountService.CreateDiscountAsync(createDiscountTypeRequest);
            return response != null ? (IActionResult)Created("create", response) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost(Name = nameof(GetAllDiscounts))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var userData = await _discountService.GetAllDiscounts();
            return userData != null ? (IActionResult)Ok(userData) : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPost(Name = nameof(GetDiscountPercentageByType))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> GetDiscountPercentageByType(string type)
        {
            _logger.LogInformation($"User Id Request: {type}");
            if (string.IsNullOrEmpty(type) || string.IsNullOrWhiteSpace(type)) return StatusCode(StatusCodes.Status400BadRequest);
            var discountPercentage = await _discountService.GetDiscountPercentageByType(type);
            return discountPercentage > 0.0 ? (IActionResult)Ok(discountPercentage) : StatusCode(StatusCodes.Status404NotFound);
        }
    }
}
