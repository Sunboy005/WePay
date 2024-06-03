using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wepay.Models.DTOs;
using wepay.Service.Interface;

namespace wepay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CurrencyController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("changebasecurrency")]
        public async Task<IActionResult> ChangeBaseCurrency(string currencyIdFrom, string currencyIdTo)
        {
            var result = await _serviceManager.CurrencyService.ChangeBaseCurrency(currencyIdFrom,currencyIdTo);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();

        }

        [HttpDelete("delete-currency")]
        public async Task<IActionResult> DeleteCurrency([FromBody] CurrencyDeletionDto currencyDeletionDto)
        {
            var result = await _serviceManager.CurrencyService.DeleteCurrency(currencyDeletionDto);
            if (result == false)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
