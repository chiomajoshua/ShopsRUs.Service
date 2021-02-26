using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSwag.Annotations;
using ShopsRUs.Service.Features.Customer.Models;
using ShopsRUs.Service.Features.Customer.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ShopsRUs.Service.Features.Customer
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(IUserService userService, ILogger<CustomerController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost(Name = nameof(Create))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> Create(CreateUserRequest createAccountRequest)
        {
            _logger.LogInformation($"User Request: {JsonConvert.SerializeObject(createAccountRequest)}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userData = await _userService.FindUserByEmailAsync(createAccountRequest.Email);

            if (userData != null)
                return Conflict();

            var response = await _userService.CreateUserAsync(createAccountRequest);
            return response != null ? (IActionResult)Created("create", response) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet(Name = nameof(GetAllCustomers))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var userData = await _userService.GetAllCustomers();
            return userData != null ? (IActionResult)Ok(userData) : StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpGet(Name = nameof(GetCustomerById))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [OpenApiTag("ShopsRUs API")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            _logger.LogInformation($"User Id Request: {id}");
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)) return StatusCode(StatusCodes.Status400BadRequest);
            var userData = await _userService.GetCustomerById(id);
            return userData != null ? (IActionResult)Ok(userData) : StatusCode(StatusCodes.Status404NotFound);
        }
    }
}